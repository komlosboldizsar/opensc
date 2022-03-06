﻿using OpenSC.Model;
using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{
    public class CustomDataGridView<T> : DataGridView
    {

        private IObservableEnumerable<T> boundCollection;

        public IObservableEnumerable<T> BoundCollection
        {
            get => boundCollection;
            set
            {
                if (boundCollection != null)
                {
                    boundCollection.ItemsAdded -= itemsAddedHandler;
                    boundCollection.ItemsRemoved -= itemsRemovedHandler;
                }
                boundCollection = value;
                loadItems();
                if (boundCollection != null)
                {
                    boundCollection.ItemsAdded += itemsAddedHandler;
                    boundCollection.ItemsRemoved += itemsRemovedHandler;
                }
            }
        }

        private List<CustomDataGridViewColumnDescriptor<T>> columnDescriptors = new List<CustomDataGridViewColumnDescriptor<T>>();
        public IReadOnlyList<CustomDataGridViewColumnDescriptor<T>> ColumnDescriptors => columnDescriptors;

        public CustomDataGridView() => init();

        public CustomDataGridView(IObservableEnumerable<T> boundCollection)
        {
            BoundCollection = boundCollection;
            init();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
                BoundCollection = null;
        }

        private void init()
        {
            CellContentClick += cellContentClickHandler;
            CellDoubleClick += cellDoubleClickHandler;
            CellEndEdit += cellEndEditHandler;
            CellMouseDown += cellMouseDownHandler;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = false;
            saveHeaderProperties();
        }

        private void cellEndEditHandler(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.RowIndex >= Rows.Count))
                return;
            CustomDataGridViewRow<T> row = Rows[e.RowIndex] as CustomDataGridViewRow<T>;
            row?.HandleEndEdit(e);
        }

        private void cellContentClickHandler(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.RowIndex >= Rows.Count))
                return;
            CustomDataGridViewRow<T> row = Rows[e.RowIndex] as CustomDataGridViewRow<T>;
            row?.HandleContentClick(e);
        }

        private void cellDoubleClickHandler(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.RowIndex >= Rows.Count))
                return;
            CustomDataGridViewRow<T> row = Rows[e.RowIndex] as CustomDataGridViewRow<T>;
            row?.HandleDoubleClick(e);
        }

        private void cellMouseDownHandler(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.RowIndex >= Rows.Count))
                return;
            CustomDataGridViewRow<T> row = Rows[e.RowIndex] as CustomDataGridViewRow<T>;
            DataGridViewColumn column = Columns[e.ColumnIndex];
            CustomDataGridViewDragSourceEventArgs<T> dragEventArgs = new()
            {
                Table = this,
                ColumnIndex = e.ColumnIndex,
                Column = column,
                RowIndex = e.RowIndex,
                Row = row
            };
            DragHandlers.GetDragData(dragEventArgs, out DragDropEffects allowedEffects, out object draggedObject);
            if (allowedEffects != DragDropEffects.None)
                DoDragDrop(draggedObject, allowedEffects);
        }

        public CustomDataGridViewDragHandlerCollection<T> DragHandlers { get; } = new();

        private void itemsAddedHandler(IEnumerable<IObservableCollection<T>.ItemWithPosition> affectedItemsWithPositions)
            => affectedItemsWithPositions.Foreach(aiwp => Rows.Insert(aiwp.Position, new CustomDataGridViewRow<T>(this, aiwp.Item)));

        private void itemsRemovedHandler(IEnumerable<IObservableCollection<T>.ItemWithPosition> affectedItemsWithPositions)
            => affectedItemsWithPositions.Foreach(aiwp => Rows.RemoveAt(aiwp.Position));

        private void loadItems()
        {
            Rows.Clear();
            if (boundCollection == null)
                return;
            foreach (T item in boundCollection)
                Rows.Add(new CustomDataGridViewRow<T>(this, item));
        }

        public void AddColumn(CustomDataGridViewColumnDescriptor<T> columnDescriptor)
        {
            columnDescriptors.Add(columnDescriptor);
            DataGridViewColumn column = getColumnByType(columnDescriptor.Type);
            column.Tag = new CustomDataGridViewColumnTag()
            {
                ID = columnDescriptor.ID
            };
            column.HeaderText = columnDescriptor.Header;
            column.Width = columnDescriptor.Width + columnDescriptor.DividerWidth;
            column.DividerWidth = columnDescriptor.DividerWidth;
            if(columnDescriptor.CellStyle != null)
                column.DefaultCellStyle = columnDescriptor.CellStyle;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Columns.Add(column);
            columnDescriptor.Extensions?.Foreach(ext => ext.ColumnReady(this, column));
        }

        public void ColumnChangeReady() => loadItems();

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
                case DataGridViewColumnType.SmallIcon:
                    return new DataGridViewTextBoxColumn();
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
                    ColumnHeadersHeight = titleSize.Width + 15;
                e.Graphics.TranslateTransform(0, titleSize.Width);
                e.Graphics.RotateTransform(-90.0F);
                e.Graphics.DrawString(e.Value.ToString(), this.Font, Brushes.Black, new PointF(rect.Y - (ColumnHeadersHeight - titleSize.Width), rect.X + rect.Width/2 - titleSize.Height/2));
                e.Graphics.RotateTransform(90.0F);
                e.Graphics.TranslateTransform(0, -titleSize.Width);
                e.Handled = true;
            }
        }

    }
}
