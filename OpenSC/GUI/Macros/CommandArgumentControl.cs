using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenSC.Model.Macros;
using OpenSC.GUI.GeneralComponents.DropDowns;

namespace OpenSC.GUI.Macros
{
    public partial class CommandArgumentControl : UserControl
    {

        public CommandArgumentControl()
        {
            InitializeComponent();
        }

        public CommandArgumentControl(IMacroCommandArgument argument, int argIndex, bool last)
        {
            InitializeComponent();
            this.argument = argument;
            this.argIndex = argIndex;
            this.last = last;
        }

        private IMacroCommandArgument argument;

        private int argIndex;

        private bool last;

        private static readonly Type[] TYPES_TEXTBOX = new Type[]
        {
            typeof(string)
        };

        private static readonly Type[] TYPES_NUMERICFIELD = new Type[]
        {
            typeof(int)
        };

        private void CommandArgumentControl_Load(object sender, EventArgs e)
        {
            if (last)
                borderPanel.Visible = false;
            if (argument == null)
                return;
            argumentIndexLabel.Text = string.Format("(arg{0})", argIndex);
            argumentNameLabel.Text = argument.Name;
            argumentDescriptionLabel.Text = argument.Description;
            valueComboBox.CreateAdapterAsDataSource<object>(new List<object>(), o => o.ToString(), true, "-");
        }

        public object ArgumentValue
            => valueComboBox.SelectedValue;

    }
}
