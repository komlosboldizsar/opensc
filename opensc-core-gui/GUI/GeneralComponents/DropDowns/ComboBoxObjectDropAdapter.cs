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

    public static class ComboBoxObjectDropAdapter
    {

        private class Handlers
        {

            public static Handlers Instance { get; } = new();
            private Handlers() => ObjectDropAdapter<ComboBox>.Handle(receiverCanHandle, receiverDragResponder, receiverValueSetter);
            public void _() { }

            private static bool receiverCanHandle(ComboBox receiverParent, DragEventArgs eventArgs, object tag) => true;

            private static bool receiverDragResponder(ComboBox receiver, DragEventArgs eventArgs, object tag) => true;

            private static void receiverValueSetter(ComboBox receiver, IEnumerable<object> objects, DragEventArgs eventArgs, object tag)
                => receiver.SelectWithParentsHelp(objects.First());

        }

        public static ObjectDropAdapter<ComboBox>.IDropSettingManager ReceiveSystemObjectDrop(this ComboBox comboBox)
        {
            Handlers.Instance._();
            return ObjectDropAdapter<ComboBox>.ReceiveObjectDrop(comboBox);
        }  

    }

}
