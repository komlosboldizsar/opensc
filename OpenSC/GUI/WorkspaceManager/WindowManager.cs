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
                window.Resize += persistableWindowSizePositionChangeHandler;
                window.Move += persistableWindowSizePositionChangeHandler;
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
                window.Resize -= persistableWindowSizePositionChangeHandler;
                window.Move -= persistableWindowSizePositionChangeHandler;
                persistWindows();
            }

        }

        private void persistWindows()
        {
            if(!restoringPersistedWindows)
                WindowPersister.SaveWindows(persistableChildWindows);
        }

        private void restorePersistedWindows()
        {
            restoringPersistedWindows = true;
            List<IPersistableWindow> windows = WindowPersister.LoadWindows();
            if(windows != null)
                foreach (IPersistableWindow window in windows)
                    window.RestoreWindow();
            restoringPersistedWindows = false;
            persistWindows();
        }

        private void persistableWindowSizePositionChangeHandler(object sender, EventArgs e)
        {
            persistWindows();
        }

    }
}
