using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenSC.Model.Routers;

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
                Output.SourceNameChanged += sourceNameChangedHandler;
                Output.CrosspointChanged += crosspointChangedHandler;
                Output.NameChanged += nameChangedHandler;
            }
        }

        private void crosspointChangedHandler(RouterOutput output, RouterInput newInput)
            => crosspoint = newInput;

        private void sourceNameChangedHandler(IRouterInputSource inputSource, string newName)
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

            if(Output.Crosspoint == null)
            {
                label.Text = "?";
                return;
            }
            label.Text = string.Format("{0}\r\n({1})",
                Output.Crosspoint.Name,
                ((Output.SourceName != null) ? Output.SourceName : "-"));

        }

    }

}
