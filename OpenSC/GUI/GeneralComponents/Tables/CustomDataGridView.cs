using OpenSC.Model;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{
    public class CustomDataGridView<T>: DataGridView
        where T: class, IModel
    {

        private DatabaseBase<T> boundDatabase;

        public DatabaseBase<T> BoundDatabase
        {
            get { return boundDatabase; }
            set
            {
                if (boundDatabase != null)
                    boundDatabase.ChangedItems -= itemsChangedHandler;
                boundDatabase = value;
                loadItems();
                if (boundDatabase != null)
                    boundDatabase.ChangedItems += itemsChangedHandler;
            }
        }

        private List<CustomDataGridViewColumnDescriptor<T>> columnDescriptors = new List<CustomDataGridViewColumnDescriptor<T>>();

        public IReadOnlyList<CustomDataGridViewColumnDescriptor<T>> ColumnDescriptors => columnDescriptors;

        public CustomDataGridView()
        {
            init();
        }

        public CustomDataGridView(DatabaseBase<T> boundDatabase)
        {
            BoundDatabase = boundDatabase;
            init();
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
            try
            {
                CustomDataGridViewRow<T> row = Rows[e.RowIndex] as CustomDataGridViewRow<T>;
                row.HandleEndEdit(e);
            }
            catch { }
        }

        private void cellContentClickHandler(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CustomDataGridViewRow<T> row = Rows[e.RowIndex] as CustomDataGridViewRow<T>;
                row.HandleContentClick(e);
            }
            catch { }
        }

        private void itemsChangedHandler(DatabaseBase<T> database)
        {
            loadItems();
        }

        private void loadItems()
        {
            Rows.Clear();
            if (boundDatabase == null)
                return;
            foreach (T item in boundDatabase.ItemsAsList)
            {
                var row = new CustomDataGridViewRow<T>(this, item);
                Rows.Add(row);
            }
        }

        public void AddColumn(CustomDataGridViewColumnDescriptor<T> columnDescriptor)
        {
            columnDescriptors.Add(columnDescriptor);
            DataGridViewColumn column = getColumnByType(columnDescriptor.Type);
            column.Width = columnDescriptor.Width + columnDescriptor.DividerWidth;
            column.DividerWidth = columnDescriptor.DividerWidth;
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
