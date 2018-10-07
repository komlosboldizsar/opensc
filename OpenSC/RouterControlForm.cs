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
    public partial class RouterControlForm : ChildWindowWithTitle
    {

        public RouterInputOutputControl activeInput;
        public List<RouterInputOutputControl> activeOutputs = new List<RouterInputOutputControl>();

        public RouterControlForm()
        {
            InitializeComponent();
        }

        private void RouterControlForm_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 7; i++) {
                var ctrl = new RouterInputOutputControl();
                ctrl.LabelText = string.Format("INPUT {0}", i);
                ctrl.Clicked += inputClicked;
                ctrl.Parent = inputsContainer;
            }
        }

        private void inputClicked(object sender, EventArgs e)
        {
            if(activeInput != null)
                activeInput.IsActive = false;
            var senderCtrl = (RouterInputOutputControl)sender;
            if (senderCtrl != activeInput)
            {
                activeInput = senderCtrl;
                activeInput.IsActive = true;
            }
            else
            {
                activeInput = null;
            }
        }

    }
}
