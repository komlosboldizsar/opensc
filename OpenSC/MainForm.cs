using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var w = new ChildWindowBase();
            w.MdiParent = this;
            w.Show();
        }

        private void clockUpdateTimer_Tick(object sender, EventArgs e)
        {
            statusStripClock.Text = DateTime.Now.ToString("yyyy. MM. dd. HH:mm:ss");
        }
    }
}
