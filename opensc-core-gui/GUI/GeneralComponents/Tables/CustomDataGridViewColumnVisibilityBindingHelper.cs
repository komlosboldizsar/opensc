using OpenSC.Model;
using OpenSC.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{

    public static class CustomDataGridViewColumnVisibilityBindingHelper
    {

        public static void BindVisibilityToSetting<T>(this CustomDataGridViewColumnDescriptorBuilder<T> customDataGridViewColumnDescriptorBuilder, Setting<bool> setting)
            => customDataGridViewColumnDescriptorBuilder.AddExtension(new CustomDataGridViewColumnVisibilityBindingDescriptorExtension<T>(setting));

        private class CustomDataGridViewColumnVisibilityBindingDescriptorExtension<T> : CustomDataGridViewColumnDescriptorExtension<T>
        {

            private Setting<bool> setting;
            DataGridViewColumn column;
            public CustomDataGridViewColumnVisibilityBindingDescriptorExtension(Setting<bool> setting)
            {
                this.setting = setting;
                setting.ValueChanged += settingValueChanged;
            }

            public override void CellReady(CustomDataGridView<T> table, DataGridViewCell cell) { }
            public override void ColumnReady(CustomDataGridView<T> table, DataGridViewColumn column)
            {
                this.column = column;
                column.Visible = setting.Value;
            }

            private void settingValueChanged(ISetting setting, object oldValue, object newValue)
            {
                if (column != null)
                    column.Visible = (bool)newValue;
            }

        }

    }

}
