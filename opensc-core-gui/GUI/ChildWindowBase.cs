using OpenSC.GUI.WorkspaceManager;
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
    public partial class ChildWindowBase : Form, IPersistableWindow
    {

        public ChildWindowBase()
        {
            InitializeComponent();
        }

        public void ShowAsChild()
        {
            MdiParent = MainForm.Instance;
            Show();
        }

        private void ChildWindowBase_Load(object sender, EventArgs e)
        {
            if(!DesignMode)
                WorkspaceManager.WindowManager.Instance.NotifyChildWindowOpened(this);
        }

        private void ChildWindowBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!DesignMode)
                WorkspaceManager.WindowManager.Instance.NotifyChildWindowClosed(this);
        }

        #region Persistence
        public Point Position => new Point(Left, Top);

        private Point? restoredPosition = null;
        private Size? restoredSize = null;

        bool restoring = false;

        public void RestoreData(Point position, Size size, Dictionary<string, object> keyValuePairs)
        {
            restoring = true;
            restoredPosition = position;
            restoredSize = size;
            restoreBeforeOpen(keyValuePairs);
            restoring = false;
        }

        public void RestoreWindow()
        {

            if (restoredSize.HasValue)
                Size = restoredSize.Value;

            ShowAsChild();

            if (restoredPosition.HasValue)
            {
                Left = restoredPosition.Value.X;
                Top = restoredPosition.Value.Y;
            }

        }

        public virtual Dictionary<string, object> GetKeyValuePairs()
        {
            return new Dictionary<string, object>();
        }

        protected virtual void restoreBeforeOpen(Dictionary<string, object> keyValuePairs)
        { }

        protected void RequestRepersist()
        {
            if(!restoring)
                WindowManager.Instance.ChildWindowRequestsRepersist();
        }
        #endregion

    }
}
