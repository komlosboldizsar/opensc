using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.GeneralComponents.Tables
{
    public class CustomDataGridViewColumnDescriptorBuilder<T>
        where T: class, IModel
    {

        private DataGridViewColumnType type;

        private string header;

        private CustomDataGridViewColumnDescriptor<T>.CellInitializerMethodDelegate initializerMethod;

        private CustomDataGridViewColumnDescriptor<T>.CellUpdaterMethodDelegate updaterMethod;

        private CustomDataGridViewColumnDescriptor<T>.CellContentClickHandlerMethodDelegate contentClickHandlerMethod;

        private CustomDataGridViewColumnDescriptor<T>.CellEndEditHandlerMethodDelegate endEditHandlerMethod;

        private List<string> changeEvents = new List<string>();

        public CustomDataGridViewColumnDescriptorBuilder()
        { }

        public CustomDataGridViewColumnDescriptorBuilder(DataGridViewColumnType type)
        {
            this.type = type;
        }

        public CustomDataGridViewColumnDescriptor<T> Build()
        {
            return new CustomDataGridViewColumnDescriptor<T>(
                type,
                header,
                initializerMethod,
                updaterMethod,
                contentClickHandlerMethod,
                endEditHandlerMethod,
                changeEvents.ToArray());
        }

        public CustomDataGridViewColumnDescriptor<T> BuildAndAdd(CustomDataGridView<T> table) {
            var columnDescriptor = Build();
            table.AddColumn(columnDescriptor);
            return columnDescriptor;
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

    }
}
