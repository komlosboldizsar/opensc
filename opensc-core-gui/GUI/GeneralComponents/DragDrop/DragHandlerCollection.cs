using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.DragDrop
{

    public class DragHandlerCollection<TDragHandler, TDragSourceEventArgs>
        where TDragHandler : DragHandler<TDragSourceEventArgs>
        where TDragSourceEventArgs : DragSourceEventArgs
    {

        private List<TDragHandler> registeredHandlers = new();
        public void AddHandler(TDragHandler handler)
        {
            if (!registeredHandlers.Contains(handler))
                registeredHandlers.Add(handler);
        }

        public void GetDragData(TDragSourceEventArgs eventArgs, out DragDropEffects allowedEffects, out object draggedObject)
        {
            foreach (TDragHandler handler in registeredHandlers)
            {
                allowedEffects = handler.GetAllowedEffects(eventArgs);
                if (allowedEffects != DragDropEffects.None)
                {
                    draggedObject = handler.GetDraggedObject(eventArgs);
                    return;
                }
            }
            allowedEffects = DragDropEffects.None;
            draggedObject = null;
        }

    }

}
