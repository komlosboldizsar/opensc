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

    /*public static class CustomDataGridViewSystemObjectDropAdapter
    {

        private static SystemObjectSimpleDropAdapter<DataGridView> BaseInstance = new SystemObjectSimpleDropAdapter<DataGridView>(receiverDragResponder, receiverValueSetter);

        private static bool receiverDragResponder(DataGridView receiverParent, DragEventArgs eventArgs, object tag)
            => onEmptyArea(receiverParent, eventArgs);

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

        public delegate void ObjectReceiverMethod(DataGridView table, IEnumerable<ISystemObject> systemObjects);

        public static void ReceiveSystemObjectDrop(this DataGridView dataGridView, ObjectReceiverMethod objectReceiverMethod, bool enableMulti = false)
            => BaseInstance.ReceiveSystemObjectDrop(dataGridView, null, objectReceiverMethod, enableMulti);

        public static void FilterSystemObjectDropByType<TSystemObject>(this DataGridView dataGridView)
            => BaseInstance.FilterSystemObjectDropByType<TSystemObject>(dataGridView);

    }*/

}
