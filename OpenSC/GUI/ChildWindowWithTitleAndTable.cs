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

        protected const int DEFAULT_DIVIDER_WIDTH = 3;

        protected readonly Padding DEFAULT_IMAGE_BUTTON_PADDING = new Padding(2);

        protected DataGridViewCellStyle BOLD_TEXT_CELL_STYLE
        {
            get
            {
                DataGridViewCellStyle boldTextCellStyle = table.DefaultCellStyle.Clone();
                boldTextCellStyle.Font = new Font(table.DefaultCellStyle.Font, FontStyle.Bold);
                return boldTextCellStyle;
            }
        }

        protected DataGridViewCellStyle TWO_PIXELS_PADDING_CELL_STYLE {
            get
            {
                DataGridViewCellStyle twoPixelsPaddingCellStyle = table.DefaultCellStyle.Clone();
                twoPixelsPaddingCellStyle.Padding = new Padding(2);
                return twoPixelsPaddingCellStyle;
            }
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
