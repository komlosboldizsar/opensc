using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{
    public static class CustomDataGridViewSystemObjectDragAdapter
    {

        public static void AllowSystemObjectDrag<T>(this CustomDataGridViewColumnDescriptorBuilder<T> customDataGridViewColumnDescriptorBuilder)
            => customDataGridViewColumnDescriptorBuilder.AddExtension(new AllowSystemObjectDragDescriptorExtension<T>());

        private class AllowSystemObjectDragDescriptorExtension<T> : CustomDataGridViewColumnDescriptorExtension<T>
        {
            public override void ColumnReady(CustomDataGridView<T> table, DataGridViewColumn column)
            {
                SystemObjectDragHandler<T> dragHandler = SystemObjectDragHandler<T>.Instance;
                table.DragHandlers.AddHandler(dragHandler);
                dragHandler.AddTableColumn(table, column);
            }
        }

        private class SystemObjectDragHandler<T> : CustomDataGridViewDragHandler<T>
        {

            internal static SystemObjectDragHandler<T> Instance { get; } = new();

            private Dictionary<CustomDataGridView<T>, List<DataGridViewColumn>> handledTablesColumns = new();

            public void AddTableColumn(CustomDataGridView<T> table, DataGridViewColumn column)
            {
                if (!handledTablesColumns.TryGetValue(table, out List<DataGridViewColumn> handledColumns))
                {
                    handledColumns = new();
                    handledTablesColumns.Add(table, handledColumns);
                }
                if (!handledColumns.Contains(column))
                    handledColumns.Add(column);
            }

            public override DragDropEffects GetAllowedEffects(CustomDataGridViewDragSourceEventArgs<T> eventArgs)
            {
                if (!handledTablesColumns.TryGetValue(eventArgs.Table, out List<DataGridViewColumn> handledColumns))
                    return DragDropEffects.None;
                if (!handledColumns.Contains(eventArgs.Column))
                    return DragDropEffects.None;
                return DragDropEffects.Link;
            }

            public override object GetDraggedObject(CustomDataGridViewDragSourceEventArgs<T> eventArgs)
            {
                DataGridViewSelectedCellCollection selectedCells = eventArgs.Table.SelectedCells;
                if ((selectedCells.Count > 1) && selectedCells.Contains(eventArgs.Cell))
                {
                    return selectedCells.Cast<DataGridViewCell>()
                        .Where(c => (c.ColumnIndex == eventArgs.ColumnIndex))
                        .Distinct(CellRowIndexEqualityComparer.Instance)
                        .OrderBy(c => c.RowIndex)
                        .Select(c => new SystemObjectReference(((CustomDataGridViewRow<T>)c.OwningRow).Item as ISystemObject))
                        .ToArray();
                }
                return new SystemObjectReference(eventArgs.Row.Item as ISystemObject);
            }

            private class CellRowIndexEqualityComparer : IEqualityComparer<DataGridViewCell>
            {
                public static CellRowIndexEqualityComparer Instance { get; } = new();
                public bool Equals(DataGridViewCell x, DataGridViewCell y) => (x.RowIndex == y.RowIndex);
                public int GetHashCode([DisallowNull] DataGridViewCell obj) => obj.GetHashCode();
            }

        }

    }
}
