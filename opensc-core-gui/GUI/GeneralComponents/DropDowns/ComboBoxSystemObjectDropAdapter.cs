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

        private static SystemObjectSimpleDropAdapter<ComboBox> baseInstance;
        private static SystemObjectSimpleDropAdapter<ComboBox> BaseInstance
        {
            get
            {
                if (baseInstance == null)
                    baseInstance = new SystemObjectSimpleDropAdapter<ComboBox>(receiverDragResponder, receiverValueSetter);
                return baseInstance;
            }
        }

        private static void receiverValueSetter(ComboBox receiver, ISystemObject systemObject, DragEventArgs eventArgs, object tag)
        {
            if (receiver.ContainsValue(systemObject))
                receiver.SelectByValue(systemObject);
        }

        private static bool receiverDragResponder(ComboBox receiver, DragEventArgs eventArgs, object tag) => true;

        public static void ReceiveSystemObjectDrop(this ComboBox comboBox)
            => BaseInstance.ReceiveSystemObjectDrop(comboBox);

        public static void FilterSystemObjectDropByType<TSystemObject>(this ComboBox comboBox)
            => BaseInstance.FilterSystemObjectDropByType<TSystemObject>(comboBox);

    }

}
