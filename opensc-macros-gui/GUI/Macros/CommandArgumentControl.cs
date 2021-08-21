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

        private Control valueField;

        private void CommandArgumentControl_Load(object sender, EventArgs e)
        {

            if (last)
                borderPanel.Visible = false;

            if (argument == null)
                return;

            argumentIndexLabel.Text = string.Format("(arg{0})", argIndex);
            argumentNameLabel.Text = argument.Name;
            argumentDescriptionLabel.Text = argument.Description;

            if (argument.ObjectType == typeof(string))
            {
                table.Controls.Remove(valueComboBox);
                TextBox valueTextBox = new TextBox();
                table.Controls.Add(valueTextBox, 1, 2);
                valueTextBox.Dock = DockStyle.Top;
                valueField = valueTextBox;
            }
            else if (isNumericalType(argument.ObjectType))
            {

                table.Controls.Remove(valueComboBox);

                NumericUpDown valueNumericField = new NumericUpDown();
                valueNumericField.Minimum = 0;
                valueNumericField.Maximum = 99999;
                bool isInt = (argument.ObjectType == typeof(int));
                valueNumericField.DecimalPlaces = isInt ? 0 : 2;
                valueNumericField.Increment = isInt ? 1 : (decimal)0.05;

                table.Controls.Add(valueNumericField, 1, 2);
                valueNumericField.Dock = DockStyle.Top;
                valueField = valueNumericField;

            }
            else
            {
                valueComboBox.CreateAdapterAsDataSource<object>(argument.GetPossibilities(new object[] { }), item => argument.GetStringForPossibility(item), true, "-");
                valueField = valueComboBox;
            }

        }

        private static bool isNumericalType(Type type)
            => ((type == typeof(int)) || (type == typeof(decimal)) || (type == typeof(float)) || (type == typeof(double)));

        public object[] PreviousArgumentValues
        {
            set
            {
                if ((argument.ObjectType == typeof(string)) || isNumericalType(argument.ObjectType))
                    return;
                valueComboBox.CreateAdapterAsDataSource<object>(argument.GetPossibilities(value), item => argument.GetStringForPossibility(item), true, "-");
            }
        }

        public object ArgumentValue
        {
            get
            {
                TextBox valueTextBox = valueField as TextBox;
                if (valueTextBox != null)
                    return valueTextBox.Text;
                NumericUpDown valueNumericField = valueField as NumericUpDown;
                if (valueNumericField != null)
                    return valueNumericField.Value;
                return valueComboBox.SelectedValue;
            }
        }

        public delegate void ArgumentValueChangedDelegate(CommandArgumentControl control, IMacroCommandArgument argument, object newValue);
        public event ArgumentValueChangedDelegate ArgumentValueChanged;

        private void valueComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArgumentValueChanged?.Invoke(this, argument, valueComboBox.SelectedValue);
        }

    }
}
