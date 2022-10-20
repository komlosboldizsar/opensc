using OpenSC.Logger;
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

    public partial class SplashScreen : Form, ILogReceiver
    {

        public SplashScreen()
        {
            this.FormClosing += SplashScreen_FormClosing;
            this.Disposed += SplashScreen_Disposed;
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
            => LogDispatcher.Subscribe(this, LogMessageType.Info);

        private void SplashScreen_FormClosing(object sender, FormClosingEventArgs e)
            => LogDispatcher.Unsubscribe(this);

        private void SplashScreen_Disposed(object sender, EventArgs e)
            => LogDispatcher.Unsubscribe(this);

        public void ReceiveLogMessage(LogMessageType messageType, DateTime timestamp, string tag, string message)
            => Status = message;

        public string Status
        {
            get => statusLabel.Text;
            set
            {
                if (InvokeRequired)
                {
                    if (IsDisposed)
                        return;
                    Invoke(new Action(() => Status = value));
                    return;
                }
                statusLabel.Text = value;
                Application.DoEvents();
            }
        }

    }

}
