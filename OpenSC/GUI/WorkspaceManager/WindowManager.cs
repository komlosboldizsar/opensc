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

        public event ChildWindowOpenedDelegate ChildWindowOpened;
        public event ChildWindowClosedDelegate ChildWindowClosed;
        public event ChildWindowListChangedDelegate ChildWindowListChanged;

        internal void NotifyChildWindowOpened(ChildWindowBase window)
        {
            if (!childWindows.Contains(window))
                childWindows.Add(window);
            ChildWindowOpened?.Invoke(window);
            ChildWindowListChanged?.Invoke();
        }

        internal void NotifyChildWindowClosed(ChildWindowBase window)
        {
            childWindows.Remove(window);
            ChildWindowClosed?.Invoke(window);
            ChildWindowListChanged?.Invoke();
        }

    }
}
