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
        public virtual void CellReady(CustomDataGridView<T> table, DataGridViewCell cell) { }
        public virtual void ColumnReady(CustomDataGridView<T> table, DataGridViewColumn column) { }
        public virtual void AddedToBuilder(CustomDataGridViewColumnDescriptorBuilder<T> builder) { }
    }

}
