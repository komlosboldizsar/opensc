using OpenSC.GUI.GeneralComponents.DragDrop;
using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.DropDowns
{

    public static class ComboBoxSystemObjectDropAdapter
    {

        private static SystemObjectSimpleDropAdapter<ComboBox> BaseInstance { get; } = new SystemObjectSimpleDropAdapter<ComboBox>(receiverDragResponder, receiverValueSetter);

        private static void receiverValueSetter(ComboBox receiver, IEnumerable<ISystemObject> systemObjects, DragEventArgs eventArgs, object tag)
            => receiver.SelectWithParentsHelp(systemObjects.First());

        private static bool receiverDragResponder(ComboBox receiver, DragEventArgs eventArgs, object tag) => true;

        public static void ReceiveSystemObjectDrop(this ComboBox comboBox)
            => BaseInstance.ReceiveSystemObjectDrop(comboBox);

        public static void FilterSystemObjectDropByType<TSystemObject>(this ComboBox comboBox)
            => BaseInstance.FilterSystemObjectDropByType<TSystemObject>(comboBox);        

    }

}
