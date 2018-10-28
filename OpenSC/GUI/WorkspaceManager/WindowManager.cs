using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.WorkspaceManager
{

    public delegate void ChildWindowOpenedDelegate(ChildWindowBase window);
    public delegate void ChildWindowClosedDelegate(ChildWindowBase window);
    public delegate void ChildWindowListChangedDelegate();

    public class WindowManager
    {

        public static WindowManager Instance { get; } = new WindowManager();

        private List<ChildWindowBase> childWindows = new List<ChildWindowBase>();
        private List<IPersistableWindow> persistableChildWindows = new List<IPersistableWindow>();
        int focusedWindowIndex = -1;

        public event ChildWindowOpenedDelegate ChildWindowOpened;
        public event ChildWindowClosedDelegate ChildWindowClosed;
        public event ChildWindowListChangedDelegate ChildWindowListChanged;

        private bool restoringPersistedWindows = false;
        private bool mainFormClosing = false;

        public void Init()
        {
            MainForm.Instance.Load += mainFormOpenedHandler; ;
            MainForm.Instance.FormClosing += mainFormClosingHandler;
        }

        private void mainFormOpenedHandler(object sender, EventArgs e)
        {
            restorePersistedWindows();
        }

        private void mainFormClosingHandler(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            mainFormClosing = true;
        }

        internal void NotifyChildWindowOpened(ChildWindowBase window)
        {

            if (!childWindows.Contains(window))
                childWindows.Add(window);
            ChildWindowOpened?.Invoke(window);
            ChildWindowListChanged?.Invoke();

            if (window is IPersistableWindow)
            {
                persistableChildWindows.Add(window);
                persistWindows();
                window.ResizeEnd += persistableWindowSizePositionChangeHandler;
                window.GotFocus += persistableWindowGotFocusHandler;
            }

        }

        internal void NotifyChildWindowClosed(ChildWindowBase window)
        {

            childWindows.Remove(window);
            ChildWindowClosed?.Invoke(window);
            ChildWindowListChanged?.Invoke();

            if (mainFormClosing)
                return;

            if (window is IPersistableWindow)
            {
                persistableChildWindows.Remove(window);
                window.ResizeEnd -= persistableWindowSizePositionChangeHandler;
                window.GotFocus -= persistableWindowGotFocusHandler;
                persistWindows();
            }

        }

        public void ChildWindowRequestsRepersist()
        {
            persistWindows();
        }

        private void persistWindows()
        {
            if(!restoringPersistedWindows)
                WindowPersister.SaveWindows(new WindowPersister.Workspace(){
                    Windows = persistableChildWindows,
                    FocusedWindowIndex = focusedWindowIndex
                });
        }

        private void restorePersistedWindows()
        {
            restoringPersistedWindows = true;
            WindowPersister.Workspace workspace = WindowPersister.LoadWindows();
            if (workspace != null) {
                foreach (IPersistableWindow window in workspace.Windows)
                    window.RestoreWindow();
                if ((workspace.FocusedWindowIndex != null) && (workspace.FocusedWindowIndex < persistableChildWindows.Count))
                    persistableChildWindows[(int)workspace.FocusedWindowIndex].Focus();
            }
            restoringPersistedWindows = false;
            persistWindows();
        }

        private void persistableWindowSizePositionChangeHandler(object sender, EventArgs e)
        {
            persistWindows();
        }

        private void persistableWindowGotFocusHandler(object sender, EventArgs e)
        {
            focusedWindowIndex = persistableChildWindows.FindIndex(w => w.Focused);
            persistWindows();
        }

    }
}
