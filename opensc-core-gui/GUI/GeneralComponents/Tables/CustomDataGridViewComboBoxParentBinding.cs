using OpenSC.GUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{

    public static class CustomDataGridViewComboBoxParentBinding
    {

        private static ParentBinder<DataGridViewComboBoxColumn, SelectEventArgs> BinderBase { get; } = new(selectIfCanMethod, canSelectMethod);

        private static bool selectIfCanMethod(DataGridViewComboBoxColumn column, object value, SelectEventArgs eventArgs)
            => (eventArgs.Row.Cells[column.Index] as DataGridViewComboBoxCell)?.SelectIfContainsValue(value) == true;

        private static bool canSelectMethod(DataGridViewComboBoxColumn column, object value, SelectEventArgs eventArgs)
            => (eventArgs.Row.Cells[column.Index] as DataGridViewComboBoxCell)?.ContainsValue(value) == true;

        public static void BindParent(this DataGridViewComboBoxColumn child, DataGridViewComboBoxColumn parent, Func<object, object> parentsValueSelector)
            => BinderBase.BindParent(child, parent, parentsValueSelector);

        public static void BindParent<T>(this CustomDataGridViewColumnDescriptorBuilder<T> childBuilder, CustomDataGridViewColumnDescriptorBuilder<T> parentBuilder, Func<object, object> parentsValueSelector)
        {
            BindParentColumnDescriptorExtensionCommonBase<T> commonBase = new(parentsValueSelector);
            childBuilder.AddExtension(new BindParentColumnDescriptorExtension<T>(BindParentColumnDescriptorExtensionMode.Child, commonBase));
            parentBuilder.AddExtension(new BindParentColumnDescriptorExtension<T>(BindParentColumnDescriptorExtensionMode.Parent, commonBase));
        }

        private class BindParentColumnDescriptorExtensionCommonBase<T>
        {

            private Func<object, object> parentsValueSelector;
            private DataGridViewComboBoxColumn childColumn;
            private DataGridViewComboBoxColumn parentColumn;

            public BindParentColumnDescriptorExtensionCommonBase(Func<object, object> parentsValueSelector)
            {
                this.parentsValueSelector = parentsValueSelector;
            }

            public void ColumnReady(DataGridViewComboBoxColumn column, BindParentColumnDescriptorExtensionMode mode)
            {
                switch (mode)
                {
                    case BindParentColumnDescriptorExtensionMode.Child:
                        childColumn = column;
                        break;
                    case BindParentColumnDescriptorExtensionMode.Parent:
                        parentColumn = column;
                        break;
                }
                columnSet();
            }

            private bool bound = false;

            private void columnSet()
            {
                if (bound)
                    return;
                if ((childColumn == null) || (parentColumn == null))
                    return;
                childColumn.BindParent(parentColumn, parentsValueSelector);
                bound = true;
            }

        }

        private class BindParentColumnDescriptorExtension<T> : CustomDataGridViewColumnDescriptorExtension<T>
        {

            public BindParentColumnDescriptorExtensionMode Mode { get; init; }
            public BindParentColumnDescriptorExtensionCommonBase<T> CommonBase { get; init; }

            public BindParentColumnDescriptorExtension(BindParentColumnDescriptorExtensionMode mode, BindParentColumnDescriptorExtensionCommonBase<T> commonBase)
            {
                Mode = mode;
                CommonBase = commonBase;
            }

            public override void ColumnReady(CustomDataGridView<T> table, DataGridViewColumn column)
            {
                if (column is DataGridViewComboBoxColumn comboBoxColumn)
                    CommonBase.ColumnReady(comboBoxColumn, Mode);
            }

            public override void AddedToBuilder(CustomDataGridViewColumnDescriptorBuilder<T> builder)
            {
                DataGridViewComboBoxColumn comboBoxColumn = builder.ReadyDescriptor?.ReadyColumn as DataGridViewComboBoxColumn;
                if (comboBoxColumn != null)
                    CommonBase.ColumnReady(comboBoxColumn, Mode);
            }

        }

        private enum BindParentColumnDescriptorExtensionMode
        {
            Child,
            Parent
        }

        public static bool SelectWithParentsHelp(this DataGridViewComboBoxColumn column, DataGridViewRow row, object value)
            => BinderBase.SelectWithParentsHelp(column, value, new SelectEventArgs() { 
                Row = row
            });

        public static bool SelectWithParentsHelp(this DataGridViewComboBoxCell cell, object value)
            => BinderBase.SelectWithParentsHelp(cell.OwningColumn as DataGridViewComboBoxColumn, value, new SelectEventArgs()
            {
                Row = cell.OwningRow
            });

        private class SelectEventArgs : ParentBinderSelectEventArgs<DataGridViewComboBoxColumn>
        {
            public DataGridViewRow Row { get; init; }
        }

    }

}
