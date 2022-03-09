using OpenSC.Model;
using System;
using System.Collections.Generic;
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

            private static SystemObjectDragHandler<T> instance;

            internal static SystemObjectDragHandler<T> Instance
            {
                get
                {
                    instance ??= new();
                    return instance;
                }
            }

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
                => new SystemObjectReference(eventArgs.Row.Item as ISystemObject);

        }

    }
}
