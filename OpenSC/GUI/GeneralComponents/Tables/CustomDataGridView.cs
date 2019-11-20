using OpenSC.Model;
using OpenSC.Model.General;
using System.Collections.Generic;
using System.Drawing;
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
            saveHeaderProperties();
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

        private bool verticalHeader = false;

        public bool VerticalHeader
        {
            get => verticalHeader;
            set
            {
                verticalHeader = value;
                if (value)
                {
                    setHeaderPropertiesToVertical();
                    CellPainting += cellPaintHandlerOnVerticalHeader;
                }
                else
                {
                    restoreHeaderProperties();
                    CellPainting -= cellPaintHandlerOnVerticalHeader;
                }
                Invalidate();
            }
        }

        DataGridViewColumnHeadersHeightSizeMode _columnHeadersHeightSizeMode;
        int _columnHeadersHeight;
        DataGridViewAutoSizeColumnsMode _autoSizeColumnsMode;

        private void saveHeaderProperties()
        {
            _columnHeadersHeightSizeMode = ColumnHeadersHeightSizeMode;
            _columnHeadersHeight = ColumnHeadersHeight;
            _autoSizeColumnsMode = AutoSizeColumnsMode;
        }

        private void restoreHeaderProperties()
        {
            ColumnHeadersHeightSizeMode = _columnHeadersHeightSizeMode;
            ColumnHeadersHeight = _columnHeadersHeight;
            AutoSizeColumnsMode = _autoSizeColumnsMode;
        }

        private void setHeaderPropertiesToVertical()
        {
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            ColumnHeadersHeight = 50;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
        }

        // @source https://stackoverflow.com/a/5783099
        void cellPaintHandlerOnVerticalHeader(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.ClipBounds, true);
                Rectangle rect = GetColumnDisplayRectangle(e.ColumnIndex, true);
                Size titleSize = TextRenderer.MeasureText(e.Value.ToString(), e.CellStyle.Font);
                if (ColumnHeadersHeight < titleSize.Width)
                {
                    ColumnHeadersHeight = titleSize.Width;
                }
                e.Graphics.TranslateTransform(0, titleSize.Width);
                e.Graphics.RotateTransform(-90.0F);
                e.Graphics.DrawString(e.Value.ToString(), this.Font, Brushes.Black, new PointF(rect.Y - (ColumnHeadersHeight - titleSize.Width), rect.X));
                e.Graphics.RotateTransform(90.0F);
                e.Graphics.TranslateTransform(0, -titleSize.Width);
                e.Handled = true;
            }
        }

    }
}
