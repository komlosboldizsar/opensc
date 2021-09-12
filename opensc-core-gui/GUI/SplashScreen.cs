﻿using OpenSC.Logger;
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
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            LogDispatcher.Subscribe(this, LogMessageType.Info);
        }

        public void ReceiveLogMessage(LogMessageType messageType, DateTime timestamp, string tag, string message)
        {
            Status = message;
        }

        public string Status
        {
            get => statusLabel.Text;
            set
            {
                statusLabel.Text = value;
                Application.DoEvents();
            }
        }

    }

}
