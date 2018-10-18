using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{
    public class CustomDataGridViewColumnDescriptorBuilder<T>
        where T: class, IModel
    {

        private CustomDataGridView<T> table;

        private DataGridViewColumnType type;

        private string header;

        private int width;

        private int dividerWidth;

        private DataGridViewCellStyle cellStyle;

        private CustomDataGridViewColumnDescriptor<T>.CellInitializerMethodDelegate initializerMethod;

        private CustomDataGridViewColumnDescriptor<T>.CellUpdaterMethodDelegate updaterMethod;

        private CustomDataGridViewColumnDescriptor<T>.CellContentClickHandlerMethodDelegate contentClickHandlerMethod;

        private CustomDataGridViewColumnDescriptor<T>.CellEndEditHandlerMethodDelegate endEditHandlerMethod;

        private List<string> changeEvents = new List<string>();

        private bool textEditable;

        private string buttonText;

        private Image buttonImage;

        private Padding buttonImagePadding;

        public CustomDataGridViewColumnDescriptorBuilder()
        { }

        public CustomDataGridViewColumnDescriptorBuilder(DataGridViewColumnType type)
        {
            this.type = type;
        }

        public CustomDataGridViewColumnDescriptorBuilder(CustomDataGridView<T> table)
        {
            this.table = table;
        }

        public CustomDataGridViewColumnDescriptor<T> Build()
        {
            return new CustomDataGridViewColumnDescriptor<T>(
                type,
                header,
                width,
                dividerWidth,
                cellStyle,
                initializerMethod,
                updaterMethod,
                contentClickHandlerMethod,
                endEditHandlerMethod,
                changeEvents.ToArray(),
                textEditable,
                buttonText,
                buttonImage,
                buttonImagePadding);
        }

        public CustomDataGridViewColumnDescriptor<T> BuildAndAdd(CustomDataGridView<T> table) {
            var columnDescriptor = Build();
            table.AddColumn(columnDescriptor);
            return columnDescriptor;
        }

        public CustomDataGridViewColumnDescriptor<T> BuildAndAdd()
        {
            if (table == null)
                throw new Exception();
            return BuildAndAdd(table);
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> Type(DataGridViewColumnType type)
        {
            this.type = type;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> Header(string header)
        {
            this.header = header;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> Width(int width)
        {
            this.width = width;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> DividerWidth(int dividerWidth)
        {
            this.dividerWidth = dividerWidth;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> CellStyle(DataGridViewCellStyle cellStyle)
        {
            this.cellStyle = cellStyle;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> InitializerMethod(CustomDataGridViewColumnDescriptor<T>.CellInitializerMethodDelegate initializerMethod)
        {
            this.initializerMethod = initializerMethod;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> UpdaterMethod(CustomDataGridViewColumnDescriptor<T>.CellUpdaterMethodDelegate updaterMethod)
        {
            this.updaterMethod = updaterMethod;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> CellContentClickHandlerMethod(CustomDataGridViewColumnDescriptor<T>.CellContentClickHandlerMethodDelegate contentClickHandlerMethod)
        {
            this.contentClickHandlerMethod = contentClickHandlerMethod;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> CellEndEditHandlerMethod(CustomDataGridViewColumnDescriptor<T>.CellEndEditHandlerMethodDelegate endEditHandlerMethod)
        {
            this.endEditHandlerMethod = endEditHandlerMethod;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> AddChangeEvent(string eventName)
        {
            changeEvents.Add(eventName);
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> TextEditable(bool textEditable)
        {
            this.textEditable = textEditable;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> ButtonText(string buttonText)
        {
            this.buttonText = buttonText;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> ButtonImage(Image buttonImage)
        {
            this.buttonImage = buttonImage;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> ButtonImagePadding(Padding buttonImagePadding)
        {
            this.buttonImagePadding = buttonImagePadding;
            return this;
        }

    }
}
