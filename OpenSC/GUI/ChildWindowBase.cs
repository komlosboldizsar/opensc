﻿using OpenSC.GUI.WorkspaceManager;
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

        public void RestoreData(Dictionary<string, object> keyValuePairs)
        {
            restoreBeforeOpen(keyValuePairs);
        }

        public void RestoreWindow()
        {
            ShowAsChild();
        }

        public virtual Dictionary<string, object> GetKeyValuePairs()
        {
            return new Dictionary<string, object>();
        }

        protected virtual void restoreBeforeOpen(Dictionary<string, object> keyValuePairs)
        { }
        #endregion

    }
}
