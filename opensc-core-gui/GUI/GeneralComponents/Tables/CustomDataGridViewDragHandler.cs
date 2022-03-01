using OpenSC.GUI.GeneralComponents.DragDrop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{
    public abstract class CustomDataGridViewDragHandler<TRowItem> : DragHandler<CustomDataGridViewDragEventArgs<TRowItem>>
    { }
}
