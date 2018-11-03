using OpenSC.Model;
using OpenSC.Model.General;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{
    public class CustomDataGridView<T>: DataGridView
    {

        private IObservableList<T> boundCollection;

        public IObservableList<T> BoundCollection
        {
            get { return boundCollection; }
            set
            {
                if (boundCollection != null)
                    boundCollection.ItemsChanged -= itemsChangedHandler;
                boundCollection = value;
                loadItems();
                if (boundCollection != null)
                    boundCollection.ItemsChanged += itemsChangedHandler;
            }
        }

        private List<CustomDataGridViewColumnDescriptor<T>> columnDescriptors = new List<CustomDataGridViewColumnDescriptor<T>>();

        public IReadOnlyList<CustomDataGridViewColumnDescriptor<T>> ColumnDescriptors => columnDescriptors;

        public CustomDataGridView()
        {
            init();
        }

        public CustomDataGridView(IObservableList<T> boundCollection)
        {
            BoundCollection = boundCollection;
            init();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (boundCollection != null)
                    boundCollection.ItemsChanged -= itemsChangedHandler;
                boundCollection = null;
            }
        }

        private void init()
        {
            CellContentClick += cellContentClickHandler;
            CellEndEdit += cellEndEditHandler;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = false;
        }

        private void cellEndEditHandler(object sender, DataGridViewCellEventArgs e)
        {
            CustomDataGridViewRow<T> row = Rows[e.RowIndex] as CustomDataGridViewRow<T>;
            row?.HandleEndEdit(e);
        }

        private void cellContentClickHandler(object sender, DataGridViewCellEventArgs e)
        {
            CustomDataGridViewRow<T> row = Rows[e.RowIndex] as CustomDataGridViewRow<T>;
            row?.HandleContentClick(e);
        }

        private void itemsChangedHandler()
        {
            loadItems();
        }

        private void loadItems()
        {
            Rows.Clear();
            if (boundCollection == null)
                return;
            foreach (T item in boundCollection)
            {
                var row = new CustomDataGridViewRow<T>(this, item);
                Rows.Add(row);
            }
        }

        public void AddColumn(CustomDataGridViewColumnDescriptor<T> columnDescriptor)
        {
            columnDescriptors.Add(columnDescriptor);
            DataGridViewColumn column = getColumnByType(columnDescriptor.Type);
            column.HeaderText = columnDescriptor.Header;
            column.Width = columnDescriptor.Width + columnDescriptor.DividerWidth;
            column.DividerWidth = columnDescriptor.DividerWidth;
            if(columnDescriptor.CellStyle != null)
                column.DefaultCellStyle = columnDescriptor.CellStyle;
            Columns.Add(column);
        }

        public void ColumnChangeReady()
        {
            loadItems();
        }

        private static DataGridViewColumn getColumnByType(DataGridViewColumnType type)
        {
            switch (type)
            {
                case DataGridViewColumnType.TextBox:
                    return new DataGridViewTextBoxColumn();
                case DataGridViewColumnType.CheckBox:
                    return new DataGridViewCheckBoxColumn();
                case DataGridViewColumnType.Image:
                    return new DataGridViewImageColumn();
                case DataGridViewColumnType.Button:
                    return new DataGridViewButtonColumn();
                case DataGridViewColumnType.ComboBox:
                    return new DataGridViewComboBoxColumn();
                case DataGridViewColumnType.Link:
                    return new DataGridViewLinkColumn();
                case DataGridViewColumnType.DisableButton:
                    return new DataGridViewButtonColumn();
                case DataGridViewColumnType.ImageButton:
                    return new DataGridViewButtonColumn();
            }
            return null;
        }

    }
}
