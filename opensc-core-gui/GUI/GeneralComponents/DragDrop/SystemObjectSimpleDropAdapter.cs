using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.DragDrop
{

    public class SystemObjectSimpleDropAdapter<TReceiver> : SystemObjectCompositeDropAdapter<TReceiver, SystemObjectSimpleDropAdapter<TReceiver>.NullChild>
        where TReceiver : Control
    {

        public SystemObjectSimpleDropAdapter(SimpleReceiverDragDesponderDelegate receiverDragDesponder, SimpleReceiverValueSetterDelegate receiverValueSetter)
            : base(nullChildSelector,
                  (rp, rc, ea, t) => receiverDragDesponder(rp, ea, t),
                  (rp, rc, so, ea, t) => receiverValueSetter(rp, so, ea, t),
                  nullChild)
        { }

        public delegate bool SimpleReceiverDragDesponderDelegate(TReceiver receiver, DragEventArgs eventArgs, object tag);
        public delegate void SimpleReceiverValueSetterDelegate(TReceiver receiver, ISystemObject systemObject, DragEventArgs eventArgs, object tag);

        private static NullChild nullChildSelector(TReceiver receiver, DragEventArgs eventArgs) => null;

        private static NullChild nullChild = new NullChild();

        public class NullChild { }

    }

}
