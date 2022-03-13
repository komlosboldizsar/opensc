﻿using OpenSC.GUI.GeneralComponents.DragDrop;
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

        private static SystemObjectCompositeDropAdapter<DataGridView, DataGridViewColumn> BaseInstance = new SystemObjectCompositeDropAdapter<DataGridView, DataGridViewColumn>(receiverChildSelector, receiverDragResponder, receiverValueSetter, new DataGridViewColumn());

        private static DataGridViewColumn receiverChildSelector(DataGridView receiverParent, DragEventArgs eventArgs)
        {
            DataGridView.HitTestInfo hitTestInfo = hitTest(receiverParent, eventArgs);
            if ((hitTestInfo.ColumnIndex >= 0) && (hitTestInfo.ColumnIndex < receiverParent.ColumnCount))
                return receiverParent.Columns[hitTestInfo.ColumnIndex];
            return null;
        }

        private static bool receiverDragResponder(DataGridView receiverParent, DataGridViewColumn receiverChild, DragEventArgs eventArgs, object tag)
        {
            DataGridView.HitTestInfo hitTestInfo = hitTest(receiverParent, eventArgs);
            if (hitTestInfo.RowIndex < 0)
                return false;
            return true;
        }

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

        public static void ReceiveSystemObjectDrop(this DataGridViewColumn dataGridViewColumn)
            => BaseInstance.ReceiveSystemObjectDrop(dataGridViewColumn.DataGridView, dataGridViewColumn);

        public static void FilterSystemObjectDropByType<TSystemObject>(this DataGridViewColumn dataGridViewColumn)
            => BaseInstance.FilterSystemObjectDropByType<TSystemObject>(dataGridViewColumn.DataGridView, dataGridViewColumn);

        public static void ReceiveSystemObjectDrop<T>(this CustomDataGridViewColumnDescriptorBuilder<T> dataGridViewColumnDescriptorBuilder)
            => dataGridViewColumnDescriptorBuilder.AddExtension(new ReceiveSystemObjectDropDescriptorExtension<T>());

        public static void FilterSystemObjectDropByType<T, TSystemObject>(this CustomDataGridViewColumnDescriptorBuilder<T> dataGridViewColumnDescriptorBuilder)
            => dataGridViewColumnDescriptorBuilder.AddExtension(new FilterSystemObjectDropByTypeDescriptorExtension<T, TSystemObject>());

        private class ReceiveSystemObjectDropDescriptorExtension<T> : CustomDataGridViewColumnDescriptorExtension<T>
        {
            public override void ColumnReady(CustomDataGridView<T> table, DataGridViewColumn column)
                => column.ReceiveSystemObjectDrop();
        }

        private class FilterSystemObjectDropByTypeDescriptorExtension<T, TSystemObject> : CustomDataGridViewColumnDescriptorExtension<T>
        {
            public override void ColumnReady(CustomDataGridView<T> table, DataGridViewColumn column)
                => column.FilterSystemObjectDropByType<TSystemObject>();
        }

    }

}
