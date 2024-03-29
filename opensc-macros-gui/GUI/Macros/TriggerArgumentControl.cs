﻿using System;
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
    public partial class TriggerArgumentControl : UserControl
    {

        public TriggerArgumentControl()
        {
            InitializeComponent();
        }

        public TriggerArgumentControl(IMacroTriggerArgument argument, int argIndex, bool last)
        {
            InitializeComponent();
            this.argument = argument;
            this.argIndex = argIndex;
            this.last = last;
        }

        private IMacroTriggerArgument argument;

        private int argIndex;

        private bool last;

        private Control valueField;

        private void TriggerArgumentControl_Load(object sender, EventArgs e)
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
            else if (isBoolType(argument.ObjectType))
            {
                table.Controls.Remove(valueComboBox);
                CheckBox valueCheckBox = new CheckBox();
                table.Controls.Add(valueCheckBox, 1, 2);
                valueCheckBox.Dock = DockStyle.Left;
                valueField = valueCheckBox;
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

        private static bool isBoolType(Type type)
           => (type == typeof(bool));

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
                CheckBox valueCheckBox = valueField as CheckBox;
                if (valueCheckBox != null)
                    return valueCheckBox.Checked;
                NumericUpDown valueNumericField = valueField as NumericUpDown;
                if (valueNumericField != null)
                {
                    if (argument.ObjectType == typeof(int))
                        return (int)valueNumericField.Value;
                    if (argument.ObjectType == typeof(float))
                        return (float)valueNumericField.Value;
                    if (argument.ObjectType == typeof(double))
                        return (double)valueNumericField.Value;
                    return valueNumericField.Value;
                }
                return valueComboBox.SelectedValue;
            }
            set
            {
                TextBox valueTextBox = valueField as TextBox;
                if (valueTextBox != null)
                {
                    valueTextBox.Text = value?.ToString() ?? "";
                    return;
                }
                CheckBox valueCheckBox = valueField as CheckBox;
                if (valueCheckBox != null)
                {
                    valueCheckBox.Checked = (bool)value;
                    return;
                }
                NumericUpDown valueNumericField = valueField as NumericUpDown;
                if (valueNumericField != null)
                {
                    if (!decimal.TryParse((value?.ToString() ?? ""), out decimal valueDecimal))
                        valueDecimal = 0;
                    valueNumericField.Value = valueDecimal;
                    return;
                }
                valueComboBox.SelectByValue(value);
            }
        }

        public delegate void ArgumentValueChangedDelegate(TriggerArgumentControl control, IMacroTriggerArgument argument, object newValue);
        public event ArgumentValueChangedDelegate ArgumentValueChanged;

        private void valueComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArgumentValueChanged?.Invoke(this, argument, valueComboBox.SelectedValue);
        }

    }
}
