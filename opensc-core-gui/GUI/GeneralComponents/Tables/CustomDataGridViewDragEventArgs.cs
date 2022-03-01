using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{
    public class CustomDataGridViewDragEventArgs<TRowItem> : OpenSC.GUI.GeneralComponents.DragDrop.DragEventArgs
    {
        public int RowIndex { get; init; }
        public DataGridViewColumn Column { get; init; }
        public int ColumnIndex { get; init; }
        public CustomDataGridViewRow<TRowItem> Row { get; init; }
    }
}
