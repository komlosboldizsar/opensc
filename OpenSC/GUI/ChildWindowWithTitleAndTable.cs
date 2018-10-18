using OpenSC.GUI.GeneralComponents.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI
{
    public partial class ChildWindowWithTitleAndTable<T> : ChildWindowWithTitle
    {
        public ChildWindowWithTitleAndTable()
        {
            InitializeComponent();
        }

        protected static CustomDataGridViewColumnDescriptorBuilder<T> GetColumnDescriptorBuilder()
        {
            return new CustomDataGridViewColumnDescriptorBuilder<T>();
        }

        protected CustomDataGridViewColumnDescriptorBuilder<T> GetColumnDescriptorBuilderForTable()
        {
            return new CustomDataGridViewColumnDescriptorBuilder<T>(table);
        }

    }
}
