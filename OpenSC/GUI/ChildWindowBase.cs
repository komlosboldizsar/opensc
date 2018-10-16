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
            WorkspaceManager.WindowManager.Instance.NotifyChildWindowOpened(this);
        }

        private void ChildWindowBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            WorkspaceManager.WindowManager.Instance.NotifyChildWindowClosed(this);
        }

        public void Restore(Dictionary<string, object> keyValuePairs)
        {
            restoreBeforeOpen(keyValuePairs);
            ShowAsChild();
        }

        public virtual Dictionary<string, object> GetKeyValuePairs()
        {
            return new Dictionary<string, object>();
        }

        protected virtual void restoreBeforeOpen(Dictionary<string, object> keyValuePairs)
        { }

    }
}
