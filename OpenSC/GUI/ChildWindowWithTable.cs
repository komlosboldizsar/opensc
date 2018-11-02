using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model;
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
    public partial class ChildWindowWithTable : ChildWindowWithTitleTwoParts
    {

        public ChildWindowWithTable()
        {
            InitializeComponent();
        }

        public CustomDataGridView<T> CreateTable<T>()
            where T: class
        {
            var customTable = new CustomDataGridView<T>();
            bottomPanel.Controls.Clear();
            bottomPanel.Controls.Add(customTable);
            customTable.Dock = DockStyle.Fill;
            this._table = customTable;
            return customTable;
        }

        protected static CustomDataGridViewColumnDescriptorBuilder<T> GetColumnDescriptorBuilder<T>()
            where T: class
        {
            return new CustomDataGridViewColumnDescriptorBuilder<T>();
        }

        protected CustomDataGridViewColumnDescriptorBuilder<T> GetColumnDescriptorBuilderForTable<T>()
            where T : class
        {
            return new CustomDataGridViewColumnDescriptorBuilder<T>((CustomDataGridView<T>)_table);
        }

        protected const int DEFAULT_DIVIDER_WIDTH = 3;

        protected readonly Padding DEFAULT_IMAGE_BUTTON_PADDING = new Padding(2);

        protected DataGridViewCellStyle BOLD_TEXT_CELL_STYLE
        {
            get
            {
                DataGridViewCellStyle boldTextCellStyle = _table.DefaultCellStyle.Clone();
                boldTextCellStyle.Font = new Font(_table.DefaultCellStyle.Font, FontStyle.Bold);
                return boldTextCellStyle;
            }
        }

        protected DataGridViewCellStyle TWO_PIXELS_PADDING_CELL_STYLE
        {
            get
            {
                DataGridViewCellStyle twoPixelsPaddingCellStyle = _table.DefaultCellStyle.Clone();
                twoPixelsPaddingCellStyle.Padding = new Padding(2);
                return twoPixelsPaddingCellStyle;
            }
        }

    }
}
