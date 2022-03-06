using OpenSC.GUI.GeneralComponents.DragDrop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.GeneralComponents.Tables
{
    public class CustomDataGridViewDragHandlerCollection<TRowItem>
        : DragHandlerCollection<CustomDataGridViewDragHandler<TRowItem>, CustomDataGridViewDragSourceEventArgs<TRowItem>>
    { }
}
