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

    public static class CustomDataGridViewSystemObjectDropAdapter
    {

        private class Handlers
        {

            public static Handlers Instance { get; } = new();
            private Handlers() => SystemObjectDropAdapter<DataGridView>.Handle(receiverCanHandle, receiverDragResponder, receiverValueSetter);
            public void _() { }

            private static bool receiverCanHandle(DataGridView receiverParent, DragEventArgs eventArgs, object tag)
                => onEmptyArea(receiverParent, eventArgs);

            private static bool receiverDragResponder(DataGridView receiverParent, DragEventArgs eventArgs, object tag)
                => true;

            private static void receiverValueSetter(DataGridView receiverParent, IEnumerable<ISystemObject> systemObjects, DragEventArgs eventArgs, object tag)
            {
                if (!onEmptyArea(receiverParent, eventArgs))
                    return;
                ((ObjectReceiverMethod)tag)(receiverParent, systemObjects);
            }

            private static bool onEmptyArea(DataGridView receiverParent, DragEventArgs eventArgs)
            {
                Point clientCoords = receiverParent.PointToClient(new Point(eventArgs.X, eventArgs.Y));
                DataGridView.HitTestInfo hitTestInfo = receiverParent.HitTest(clientCoords.X, clientCoords.Y);
                return ((hitTestInfo.RowIndex == -1) && (hitTestInfo.ColumnIndex == -1)); ;
            }

        }

        public delegate void ObjectReceiverMethod(DataGridView table, IEnumerable<ISystemObject> systemObjects);

        public static SystemObjectDropAdapter<DataGridView>.IDropSettingManager ReceiveSystemObjectDrop(this DataGridView table, ObjectReceiverMethod objectReceiverMethod)
        {
            Handlers.Instance._();
            return SystemObjectDropAdapter<DataGridView>.ReceiveSystemObjectDrop(table, objectReceiverMethod);
        }

    }

}
