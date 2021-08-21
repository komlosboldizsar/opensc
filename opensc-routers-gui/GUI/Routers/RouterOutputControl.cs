using System;
using System.Collections.Generic;
using System.Drawing;
using OpenSC.Model.Routers;
using OpenSC.Model.Signals;

namespace OpenSC.GUI.Routers
{

    public partial class RouterOutputControl : RouterInputOutputControlBase
    {

        public RouterOutput Output { get; private set; }
        private RouterControlForm containerForm;

        private RouterInput _crosspoint;
        private RouterInput crosspoint
        {
            get { return _crosspoint; }
            set
            {
                if (value == _crosspoint)
                    return;
                if (_crosspoint != null)
                    _crosspoint.NameChanged -= crosspointInputNameChangedHandler;
                _crosspoint = value;
                if (_crosspoint != null)
                    _crosspoint.NameChanged += crosspointInputNameChangedHandler;
            }
        }

        public RouterOutputControl()
        {
            InitializeComponent();
        }

        public RouterOutputControl(RouterOutput output, RouterControlForm containerForm)
        {
            InitializeComponent();
            this.Output = output;
            this.containerForm = containerForm;
            button.Text = Output.Name;
            updateLabel();
        }

        private void RouterOutputControl_Load(object sender, EventArgs e)
        {
            if (Output != null)
            {
                Output.RegisteredSourceSignalNameChanged += sourceSignalNameChangedHandler;
                Output.CurrentInputChanged += currentInputChangedHandler;
                Output.NameChanged += nameChangedHandler;
            }
        }

        private void currentInputChangedHandler(RouterOutput output, RouterInput newInput)
            => crosspoint = newInput;

        private void sourceSignalNameChangedHandler(ISignalSource inputSource, string newName, List<object> recursionChain)
            => updateLabel();

        private void crosspointInputNameChangedHandler(RouterInput input, string oldName, string newName)
            => updateLabel();

        private void nameChangedHandler(RouterOutput output, string oldName, string newName)
        {
            button.Text = Output.Name;
        }

        private static readonly Color COLOR_BUTTON_BACK_SELECTED = Color.FromArgb(255, 255, 255, 128);
        private static readonly Color COLOR_BUTTON_BACK_NOTSELECTED = Color.FromArgb(255, 255, 255, 192);

        private bool selected;

        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                button.BackColor = (selected ? COLOR_BUTTON_BACK_SELECTED : COLOR_BUTTON_BACK_NOTSELECTED);
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            containerForm.OutputClicked(this);
        }

        private void updateLabel()
        {

            if (InvokeRequired)
            {
                Invoke(new Action(() => updateLabel()));
                return;
            }

            if(Output.CurrentInput == null)
            {
                label.Text = "?";
                return;
            }
            label.Text = string.Format("{0}\r\n({1})",
                Output.CurrentInput.Name,
                ((Output.RegisteredSourceSignalName != null) ? Output.RegisteredSourceSignalName : "-"));

        }

    }

}
