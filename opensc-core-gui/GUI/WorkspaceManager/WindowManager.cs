using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public delegate void ActiveChildWindowChangedDelegate(Form window);
        public event ActiveChildWindowChangedDelegate ActiveWindowChanged;

        public delegate void ChildWindowsSizePositionChangedDelegate(Form window);
        public event ChildWindowsSizePositionChangedDelegate ChildWindowsSizePositionChanged;

        public delegate void MainFormPositionChangedDelegate(Point position);
        public event MainFormPositionChangedDelegate MainFormPositionChanged;

        public delegate void MainFormSizeChangedDelegate(Size size);
        public event MainFormSizeChangedDelegate MainFormSizeChanged;

        public delegate void MainFormMaximizedChangedDelegate(bool isMaximized);
        public event MainFormMaximizedChangedDelegate MainFormMaximizedChanged;

        private Form activeWindow;

        public Form ActiveWindow
        {
            get { return activeWindow; }
            set
            {
                if (restoringPersistedWindows)
                    return;
                if (activeWindow == value)
                    return;
                activeWindow = value;
                persistWindows();
                ActiveWindowChanged?.Invoke(activeWindow);
            }
        }

        private int? ActiveWindowIndex
        {
            get
            {
                IPersistableWindow activeWindowCasted = activeWindow as IPersistableWindow;
                if (!persistableChildWindows.Contains(activeWindowCasted))
                    return null;
                return persistableChildWindows.FindIndex(window => (window == activeWindowCasted));
            }
            set
            {
                if ((value == null) || (value < 0) || (value >= persistableChildWindows.Count))
                {
                    ActiveWindow = null;
                    return;
                }
                ActiveWindow = persistableChildWindows[(int)value] as Form;
            }
        }

        private Point mainFormPosition;

        public Point MainFormPosition
        {
            get => mainFormPosition;
            set
            {
                if (value == mainFormPosition)
                    return;
                mainFormPosition = value;
                persistWindows();
                MainFormPositionChanged?.Invoke(value);
            }
        }

        private Size mainFormSize;

        public Size MainFormSize
        {
            get => mainFormSize;
            set
            {
                if (value == mainFormSize)
                    return;
                mainFormSize = value;
                persistWindows();
                MainFormSizeChanged?.Invoke(value);
            }
        }

        private bool mainFormMaximized;

        public bool MainFormMaximized
        {
            get => mainFormMaximized;
            set
            {
                if (value == mainFormMaximized)
                    return;
                mainFormMaximized = value;
                persistWindows();
                MainFormMaximizedChanged?.Invoke(value);
            }
        }

        public event ChildWindowOpenedDelegate ChildWindowOpened;
        public event ChildWindowClosedDelegate ChildWindowClosed;
        public event ChildWindowListChangedDelegate ChildWindowListChanged;

        private bool restoringPersistedWindows = false;
        private bool mainFormClosing = false;

        public void Init()
        {
            MainForm.Instance.Load += mainFormOpenedHandler;
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
                    ActiveWindowIndex = ActiveWindowIndex,
                    MainPosition = MainFormPosition,
                    MainSize = MainFormSize,
                    MainMaximized = MainFormMaximized
                });
        }

        private void restorePersistedWindows()
        {
            restoringPersistedWindows = true;
            WindowPersister.Workspace workspace = WindowPersister.LoadWindows();
            if (workspace != null) {
                foreach (IPersistableWindow window in workspace.Windows)
                    window.RestoreWindow();
            }
            if (workspace != null)
            {
                ActiveWindowIndex = workspace.ActiveWindowIndex;
                MainFormSize = workspace.MainSize;
                MainFormPosition = workspace.MainPosition;
                MainFormMaximized = workspace.MainMaximized;
            }
            restoringPersistedWindows = false;
            persistWindows();
        }

        private void persistableWindowSizePositionChangeHandler(object sender, EventArgs e)
        {
            persistWindows();
            ChildWindowsSizePositionChanged?.Invoke(sender as Form);
        }

    }
}
