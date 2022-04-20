using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.DragDrop
{

    public abstract class DragHandler<TDragEventArgs>
        where TDragEventArgs : DragSourceEventArgs
    {
        public abstract DragDropEffects GetAllowedEffects(TDragEventArgs eventArgs);
        public abstract object GetDraggedObject(TDragEventArgs eventArgs);
    }

}
