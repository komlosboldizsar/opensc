using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{

    public abstract class CustomDataGridViewColumnDescriptorExtension<T>
    {
        public abstract void CellReady(CustomDataGridView<T> table, DataGridViewCell cell);
        public abstract void ColumnReady(CustomDataGridView<T> table, DataGridViewColumn column);
    }

}
