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
            var t = new Model.Timers.Timer();
            t.Mode = Model.Timers.TimerMode.Backwards;
            t.Start();
            var w = new Timers.TimerWindow(t);
            w.ShowAsChild();
            var w2 = new Timers.TimerEditWindow(t);
            w2.ShowAsChild();
        }
    }
}
