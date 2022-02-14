using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model;
using OpenSC.Model.General;
using System.Collections.Generic;

namespace OpenSC.GUI
{

    public interface IItemListFormBaseManager
    {
        void InitializeTable();
    }

    public class ItemListFormBaseManager<TItem> : IItemListFormBaseManager
        where TItem : class 
    {

        protected ItemListFormBase form;
        protected IObservableEnumerable<TItem> boundCollection;

        public ItemListFormBaseManager(ItemListFormBase form, IObservableEnumerable<TItem> boundCollection, ColumnDescriptorCreatorDelegate baseColumnCreatorMethod)
        {
            this.form = form;
            this.boundCollection = boundCollection;
            columnDescriptorCreators.Add(baseColumnCreatorMethod);
        }

        public delegate void ColumnDescriptorCreatorDelegate(CustomDataGridView<TItem> table, ColumnDescriptorBuilderGetterDelegate builderGetterMethod);
        public delegate CustomDataGridViewColumnDescriptorBuilder<TItem> ColumnDescriptorBuilderGetterDelegate();
        private CustomDataGridView<TItem> table;
        private List<ColumnDescriptorCreatorDelegate> columnDescriptorCreators = new List<ColumnDescriptorCreatorDelegate>();

        public void InitializeTable()
        {
            table = form.CreateTable<TItem>();
            List<CustomDataGridViewColumnDescriptorBuilder<TItem>> columnDescriptorBuilders = new List<CustomDataGridViewColumnDescriptorBuilder<TItem>>();
            ColumnDescriptorBuilderGetterDelegate builderGetterMethod = () =>
            {
                CustomDataGridViewColumnDescriptorBuilder<TItem> builder = new CustomDataGridViewColumnDescriptorBuilder<TItem>(table);
                columnDescriptorBuilders.Add(builder);
                return builder;
            };
            foreach (ColumnDescriptorCreatorDelegate columnDescriptorCreator in columnDescriptorCreators)
                columnDescriptorCreator(table, builderGetterMethod);
            foreach (CustomDataGridViewColumnDescriptorBuilder<TItem> builder in columnDescriptorBuilders)
                builder.BuildAndAdd(table);
            table.BoundCollection = boundCollection;
        }

    }

}
