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

    public partial class RouterInputControl : RouterInputOutputControlBase
    {

        public RouterInput Input { get; private set; }
        private RouterControlForm containerForm;

        public RouterInputControl()
        {
            InitializeComponent();
        }

        public RouterInputControl(RouterInput input, RouterControlForm containerForm)
        {
            InitializeComponent();
            this.Input = input;
            this.containerForm = containerForm;
            button.Text = input.Name;
            label.Text = ((input.SourceName != null) ? input.SourceName : "-");
        }

        private void RouterInputControl_Load(object sender, EventArgs e)
        {
            if (Input != null)
            {
                Input.SourceNameChanged += sourceNameChangedHandler;
                Input.NameChanged += nameChangedHandler;
            }
        }

        private void sourceNameChangedHandler(RouterInput input, string newName)
        {
            label.Text = ((newName != null) ? newName : "-");
        }

        private void nameChangedHandler(RouterInput output, string oldName, string newName)
        {
            button.Text = newName;
        }

        private static readonly Color COLOR_BUTTON_BACK_SELECTED = Color.FromArgb(255, 128, 128, 255);
        private static readonly Color COLOR_BUTTON_BACK_NOTSELECTED = Color.FromArgb(255, 192, 192, 255);

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
            containerForm.InputClicked(this);
        }

    }

}
