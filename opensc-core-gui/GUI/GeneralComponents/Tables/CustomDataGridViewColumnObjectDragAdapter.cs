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
    public static class CustomDataGridViewColumnObjectDragAdapter
    {

        public delegate object DraggedObjectSelectorDelegate<T>(T item, DataGridViewCell cell);

        public static void AllowObjectDrag<T>(this CustomDataGridViewColumnDescriptorBuilder<T> customDataGridViewColumnDescriptorBuilder, DraggedObjectSelectorDelegate<T> draggedObjectSelector = null)
            => customDataGridViewColumnDescriptorBuilder.AddExtension(new AllowObjectDragDescriptorExtension<T>(draggedObjectSelector));

        private class AllowObjectDragDescriptorExtension<T> : CustomDataGridViewColumnDescriptorExtension<T>
        {

            DraggedObjectSelectorDelegate<T> draggedObjectSelector;

            public AllowObjectDragDescriptorExtension(DraggedObjectSelectorDelegate<T> draggedObjectSelector = null)
            {
                this.draggedObjectSelector = draggedObjectSelector;
            }

            public override void ColumnReady(CustomDataGridView<T> table, DataGridViewColumn column)
            {
                ObjectDragHandler<T> dragHandler = ObjectDragHandler<T>.Instance;
                table.DragHandlers.AddHandler(dragHandler);
                dragHandler.AddTableColumn(table, column, draggedObjectSelector);
            }

        }

        private class ObjectDragHandler<T> : CustomDataGridViewDragHandler<T>
        {

            internal static ObjectDragHandler<T> Instance { get; } = new();

            private Dictionary<CustomDataGridView<T>, Dictionary<DataGridViewColumn, ColumnData>> handledTablesColumns = new();

            public void AddTableColumn(CustomDataGridView<T> table, DataGridViewColumn column, DraggedObjectSelectorDelegate<T> draggedObjectSelector)
            {
                if (!handledTablesColumns.TryGetValue(table, out Dictionary<DataGridViewColumn, ColumnData> handledColumns))
                {
                    handledColumns = new();
                    handledTablesColumns.Add(table, handledColumns);
                }
                if (!handledColumns.ContainsKey(column))
                    handledColumns.Add(column, new(draggedObjectSelector));
            }

            public override DragDropEffects GetAllowedEffects(CustomDataGridViewDragSourceEventArgs<T> eventArgs)
            {
                if (!handledTablesColumns.TryGetValue(eventArgs.Table, out Dictionary<DataGridViewColumn, ColumnData> handledColumns))
                    return DragDropEffects.None;
                if (!handledColumns.ContainsKey(eventArgs.Column))
                    return DragDropEffects.None;
                return DragDropEffects.Link;
            }

            public override object GetDraggedObject(CustomDataGridViewDragSourceEventArgs<T> eventArgs)
            {
                DataGridViewSelectedCellCollection selectedCells = eventArgs.Table.SelectedCells;
                DraggedObjectSelectorDelegate<T> draggedObjectSelector = null;
                if (handledTablesColumns.TryGetValue(eventArgs.Table, out Dictionary<DataGridViewColumn, ColumnData> handledColumns))
                    if (handledColumns.TryGetValue(eventArgs.Column, out ColumnData columnData))
                        draggedObjectSelector = columnData.DraggedObjectSelector;
                if (draggedObjectSelector == null)
                    draggedObjectSelector = defaultDraggedObjectSelector;
                if ((selectedCells.Count > 1) && selectedCells.Contains(eventArgs.Cell))
                {
                    return selectedCells.Cast<DataGridViewCell>()
                        .Where(c => (c.ColumnIndex == eventArgs.ColumnIndex))
                        .Distinct(CellRowIndexEqualityComparer.Instance)
                        .OrderBy(c => c.RowIndex)
                        .Select(c => new ObjectProxy(draggedObjectSelector(((CustomDataGridViewRow<T>)c.OwningRow).Item, c)))
                        .ToArray();
                }
                return new ObjectProxy(draggedObjectSelector(eventArgs.Row.Item, eventArgs.Cell));
            }

            private static object defaultDraggedObjectSelector(T item, DataGridViewCell cell) => item;

            private class CellRowIndexEqualityComparer : IEqualityComparer<DataGridViewCell>
            {
                public static CellRowIndexEqualityComparer Instance { get; } = new();
                public bool Equals(DataGridViewCell x, DataGridViewCell y) => (x.RowIndex == y.RowIndex);
                public int GetHashCode([DisallowNull] DataGridViewCell obj) => obj.GetHashCode();
            }

            private record ColumnData(DraggedObjectSelectorDelegate<T> DraggedObjectSelector);

        }

    }
}
