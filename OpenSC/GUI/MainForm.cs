using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Timers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI
{
    public partial class MainForm : Form
    {

        public static MainForm Instance { get; } = new MainForm();

        public MainForm()
        {
            InitializeComponent();
        }

        private void clockUpdateTimer_Tick(object sender, EventArgs e)
        {
            statusStripClock.Text = DateTime.Now.ToString("yyyy. MM. dd. HH:mm:ss");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            //
            WindowManager.Instance.ChildWindowOpened += childWindowOpenedHandler;
            WindowManager.Instance.ChildWindowClosed += childWindowClosedHandler;
            //

            var t1 = new Model.Timers.Timer();
            t1.ID = 4;
            t1.Mode = Model.Timers.TimerMode.Forwards;
            t1.Start();
            TimerDatabase.Instance.Add(t1);

            var t2 = new Model.Timers.Timer();
            t2.ID = 7;
            t2.Mode = Model.Timers.TimerMode.Backwards;
            t2.Start();
            TimerDatabase.Instance.Add(t2);

            var t3 = new Model.Timers.Timer();
            t3.ID = 8;
            t3.Mode = Model.Timers.TimerMode.Clock;
            t3.Start();
            TimerDatabase.Instance.Add(t3);

            var w = new Timers.TimerWindow(t1);
            w.ShowAsChild();
            var w2 = new Timers.TimerEditWindow(t1);
            w2.ShowAsChild();
            var w3 = new Timers.TimerList();
            w3.ShowAsChild();
        }

        private void childWindowOpenedHandler(ChildWindowBase window)
        {
            ToolStripMenuItem newMenuItem = new ToolStripMenuItem()
            {
                Text = window.Text,
                Tag = window
            };
            newMenuItem.Click += windowsMenuItemClickHandler;
            windowsMenu.DropDownItems.Add(newMenuItem);
        }

        private void childWindowClosedHandler(ChildWindowBase window)
        {
            ToolStripItemCollection items = windowsMenu.DropDownItems;
            int elementCount = items.Count;
            for (int i = elementCount - 1; i >= 0; i--)
            {
                if (items[i].Tag == window)
                    items.RemoveAt(i);
            }
        }

        private void windowsMenuItemClickHandler(object sender, EventArgs e)
        {
            ((sender as ToolStripMenuItem)?.Tag as ChildWindowBase)?.Focus();
        }

    }
}
