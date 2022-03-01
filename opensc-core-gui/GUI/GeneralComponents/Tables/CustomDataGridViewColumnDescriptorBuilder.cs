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
    {

        private CustomDataGridView<T> table;

        private DataGridViewColumnType type;

        private string id;

        private string header;

        private int width;

        private int dividerWidth;

        private DataGridViewCellStyle cellStyle;

        private CustomDataGridViewColumnDescriptor<T>.CellInitializerMethodDelegate initializerMethod;

        private CustomDataGridViewColumnDescriptor<T>.CellUpdaterMethodDelegate updaterMethod;

        private CustomDataGridViewColumnDescriptor<T>.CellDropDownPopulatorMethodDelegate dropDownPopulatorMethod;

        private CustomDataGridViewColumnDescriptor<T>.CellContentClickHandlerMethodDelegate contentClickHandlerMethod;

        private CustomDataGridViewColumnDescriptor<T>.CellDoubleClickHandlerMethodDelegate doubleClickHandlerMethod;

        private CustomDataGridViewColumnDescriptor<T>.CellEndEditHandlerMethodDelegate endEditHandlerMethod;

        private List<string> changeEvents = new List<string>();

        private List<string[]> multilevelChangeEvents = new List<string[]>();

        private CustomDataGridViewColumnDescriptor<T>.ExternalUpdateEventSubscriberMethodDelegate externalUpdateEventSubscriberMethod;

        private bool textEditable;

        private string buttonText;

        private Image buttonImage;

        private Padding buttonImagePadding;

        private bool iconShown;

        private Color iconColor;

        private DataGridViewSmallIconCell.IconTypes iconType;

        private Padding iconPadding;

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
                id,
                header,
                width,
                dividerWidth,
                cellStyle,
                initializerMethod,
                updaterMethod,
                dropDownPopulatorMethod,
                contentClickHandlerMethod,
                doubleClickHandlerMethod,
                endEditHandlerMethod,
                changeEvents.ToArray(),
                multilevelChangeEvents.ToArray(),
                externalUpdateEventSubscriberMethod,
                textEditable,
                buttonText,
                buttonImage,
                buttonImagePadding,
                iconShown,
                iconColor,
                iconType,
                iconPadding);
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

        public CustomDataGridViewColumnDescriptorBuilder<T> ID(string id)
        {
            this.id = id;
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

        public CustomDataGridViewColumnDescriptorBuilder<T> DropDownPopulatorMethod(CustomDataGridViewColumnDescriptor<T>.CellDropDownPopulatorMethodDelegate dropDownPopulatorMethod)
        {
            this.dropDownPopulatorMethod = dropDownPopulatorMethod;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> CellContentClickHandlerMethod(CustomDataGridViewColumnDescriptor<T>.CellContentClickHandlerMethodDelegate contentClickHandlerMethod)
        {
            this.contentClickHandlerMethod = contentClickHandlerMethod;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> CellDoubleClickHandlerMethod(CustomDataGridViewColumnDescriptor<T>.CellDoubleClickHandlerMethodDelegate contentDoubleClickHandlerMethod)
        {
            this.doubleClickHandlerMethod = contentDoubleClickHandlerMethod;
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

        public CustomDataGridViewColumnDescriptorBuilder<T> AddMultilevelChangeEvent(params string[] eventNames)
        {
            multilevelChangeEvents.Add(eventNames);
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> ExternalUpdateEventSubscriberMethod(CustomDataGridViewColumnDescriptor<T>.ExternalUpdateEventSubscriberMethodDelegate externalUpdateEventSubscriberMethod)
        {
            this.externalUpdateEventSubscriberMethod = externalUpdateEventSubscriberMethod;
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

        public CustomDataGridViewColumnDescriptorBuilder<T> IconShown(bool iconShown)
        {
            this.iconShown = iconShown;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> IconColor(Color iconColor)
        {
            this.iconColor = iconColor;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> IconType(DataGridViewSmallIconCell.IconTypes iconType)
        {
            this.iconType = iconType;
            return this;
        }

        public CustomDataGridViewColumnDescriptorBuilder<T> IconPadding(Padding iconPadding)
        {
            this.iconPadding = iconPadding;
            return this;
        }

    }
}
