using OpenSC.GUI.GeneralComponents.DragDrop;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.DropDowns
{

    public static class CustomDataGridViewColumnSystemObjectDropAdapter
    {

        private class Handlers
        {

            public static Handlers Instance { get; } = new();

            private Handlers()
                => SystemObjectDropAdapter<DataGridView>.HandleParted<DataGridViewColumn>(receiverCanHandle, receiverChildSelector, receiverDragResponder, receiverValueSetter, new DataGridViewColumn());
            
            public void _() { }

            private static DataGridViewColumn receiverChildSelector(DataGridView receiverParent, DragEventArgs eventArgs)
            {
                DataGridView.HitTestInfo hitTestInfo = hitTest(receiverParent, eventArgs);
                if ((hitTestInfo.ColumnIndex >= 0) && (hitTestInfo.ColumnIndex < receiverParent.ColumnCount))
                    return receiverParent.Columns[hitTestInfo.ColumnIndex];
                return null;
            }

            private static bool receiverCanHandle(DataGridView receiverParent, DataGridViewColumn receiverChild, DragEventArgs eventArgs, object tag)
            {
                DataGridView.HitTestInfo hitTestInfo = hitTest(receiverParent, eventArgs);
                if (hitTestInfo.RowIndex < 0)
                    return false;
                return true;
            }

            private static bool receiverDragResponder(DataGridView receiverParent, DataGridViewColumn receiverChild, DragEventArgs eventArgs, object tag)
                => true;

            private static void receiverValueSetter(DataGridView receiverParent, DataGridViewColumn receiverChild, IEnumerable<ISystemObject> systemObjects, DragEventArgs eventArgs, object tag)
            {
                DataGridView.HitTestInfo hitTestInfo = hitTest(receiverParent, eventArgs);
                if ((hitTestInfo.ColumnIndex < 0) || (hitTestInfo.ColumnIndex >= receiverParent.ColumnCount))
                    return;
                if ((hitTestInfo.RowIndex < 0) || (hitTestInfo.RowIndex >= receiverParent.RowCount))
                    return;
                DataGridViewCell cell = receiverParent.Rows[hitTestInfo.RowIndex].Cells[hitTestInfo.ColumnIndex];
                if (cell is DataGridViewComboBoxCell comboBoxCell)
                    comboBoxCell.SelectWithParentsHelp(systemObjects.First());
                // Other cell types not supported at the moment
            }

            private static DataGridView.HitTestInfo hitTest(DataGridView receiverParent, DragEventArgs eventArgs)
            {
                Point clientCoords = receiverParent.PointToClient(new Point(eventArgs.X, eventArgs.Y));
                return receiverParent.HitTest(clientCoords.X, clientCoords.Y);
            }

        }

        public static SystemObjectDropAdapter<DataGridView>.IDropSettingManager ReceiveSystemObjectDrop(this DataGridViewColumn dataGridViewColumn)
        {
            Handlers.Instance._();
            return SystemObjectDropAdapter<DataGridView>.ReceiveSystemObjectDrop<DataGridViewColumn>(dataGridViewColumn.DataGridView, dataGridViewColumn);
        }

        public static SystemObjectDropAdapter<DataGridView>.IDropSettingManager ReceiveSystemObjectDrop<T>(this CustomDataGridViewColumnDescriptorBuilder<T> dataGridViewColumnDescriptorBuilder)
        {
            ReceiveSystemObjectDropDescriptorExtension<T> extension = new ReceiveSystemObjectDropDescriptorExtension<T>();
            dataGridViewColumnDescriptorBuilder.AddExtension(extension);
            return extension;
        }

        private class ReceiveSystemObjectDropDescriptorExtension<T> : CustomDataGridViewColumnDescriptorExtension<T>, SystemObjectDropAdapter<DataGridView>.IDropSettingManager
        {

            public override void ColumnReady(CustomDataGridView<T> table, DataGridViewColumn column)
            {
                SystemObjectDropAdapter<DataGridView>.IDropSettingManager dropSettingManager = column.ReceiveSystemObjectDrop();
                dropSettingManager.EnableMulti(enableMulti);
                typeFilters.ForEach(tf => tf.AddTo(dropSettingManager));
            }

            public SystemObjectDropAdapter<DataGridView>.IDropSettingManager FilterByType<TFilter>()
            {
                typeFilters.Add(new TypeFilter<TFilter>());
                return this;
            }

            public SystemObjectDropAdapter<DataGridView>.IDropSettingManager EnableMulti(bool enableMulti = true)
            {
                this.enableMulti = enableMulti;
                return this;
            }

            private bool enableMulti;

            private List<ITypeFilter> typeFilters = new();

            private interface ITypeFilter
            {
                void AddTo(SystemObjectDropAdapter<DataGridView>.IDropSettingManager dropSettingManager);
            }

            private class TypeFilter<TFilter> : ITypeFilter
            {
                public void AddTo(SystemObjectDropAdapter<DataGridView>.IDropSettingManager dropSettingManager)
                    => dropSettingManager.FilterByType<TFilter>();
            }

        }

    }

}
