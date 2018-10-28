using OpenSC.GUI.Menus;
using OpenSC.GUI.Settings;
using OpenSC.GUI.UMDs;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Timers;
using OpenSC.Model.UMDs;
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
            WindowManager.Instance.ChildWindowOpened += childWindowOpenedHandler;
            WindowManager.Instance.ChildWindowClosed += childWindowClosedHandler;
            WindowManager.Instance.ActiveWindowChanged += activeWindowChangedHandler;
            MdiChildActivate += mdiChildActivateHandler;

            menuStrip.DynamicChildrenInsertPosition = 1;
            menuStrip.AssociatedMenuItem = MenuManager.Instance.TopMenu;
        }

        private void mdiChildActivateHandler(object sender, EventArgs e)
        {
            WindowManager.Instance.ActiveWindow = ActiveMdiChild;
        }

        private void activeWindowChangedHandler(Form window)
        {
            if (window != null)
                ActivateMdiChild(window);
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

        private void arrangeWindowsMenuItemClickHandler(object sender, EventArgs e)
        {

            if(sender == tileWindowsHorizontallyMenuItem)
            {
                LayoutMdi(MdiLayout.TileHorizontal);
                return;
            }

            if(sender == tileWindowsVerticallyMenuItem)
            {
                LayoutMdi(MdiLayout.TileVertical);
                return;
            }

            if(sender == cascadeWindowsMenuItem)
            {
                LayoutMdi(MdiLayout.Cascade);
                return;
            }

        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var window = new UmdList();
            window.ShowAsChild();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MasterDatabase.Instance.SaveEverything();
        }

        private void globalSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsWindow.Instance.ShowAsChild();
        }
    }
}
