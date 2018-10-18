using OpenSC.Model;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{
    public class CustomDataGridViewRow<T>: DataGridViewRow
        where T : class, IModel
    {

        CustomDataGridView<T> table;

        T item;

        private static readonly Type storedType = typeof(T);

        private static readonly EventInfo[] typeEventInfos = storedType.GetEvents();

        private static EventInfo getEventInfoByName(string name)
        {
            foreach (EventInfo eventInfo in typeEventInfos)
                if (eventInfo.Name == name)
                    return eventInfo;
            return null;
        }

        public CustomDataGridViewRow(CustomDataGridView<T> table, T item)
        {
            if ((table == null) || (item == null))
                throw new ArgumentNullException();
            this.table = table;
            this.item = item;
            createCells();
        }

        private void createCells()
        {
            foreach (CustomDataGridViewColumnDescriptor<T> columnDescriptor in table.ColumnDescriptors)
            {
                DataGridViewCell cell = createAndInitCell(columnDescriptor);
                subscribeEventsForCell(cell, columnDescriptor);
            }
        }

        private DataGridViewCell createAndInitCell(CustomDataGridViewColumnDescriptor<T> columnDescriptor)
        {

            DataGridViewCell cell = getCellByType(columnDescriptor.Type);
            columnDescriptor.InitializerMethod?.Invoke(item, cell);
            columnDescriptor.UpdaterMethod?.Invoke(item, cell);

            if (columnDescriptor.Type == DataGridViewColumnType.CheckBox)
            {
                DataGridViewCheckBoxCell typedCell = (DataGridViewCheckBoxCell)cell;
                typedCell.FalseValue = false;
                typedCell.TrueValue = true;
            }

            if (columnDescriptor.Type == DataGridViewColumnType.ImageButton)
            {
                DataGridViewImageButtonCell typedCell = (DataGridViewImageButtonCell)cell;
                typedCell.Image = columnDescriptor.ButtonImage;
                typedCell.ImagePadding = columnDescriptor.ButtonImagePadding;
            }

            if ((columnDescriptor.Type == DataGridViewColumnType.Button) || (columnDescriptor.Type == DataGridViewColumnType.DisableButton))
            {
                cell.Value = columnDescriptor.ButtonText;
            }

            Cells.Add(cell);

                        if (columnDescriptor.Type == DataGridViewColumnType.TextBox)
            {
                cell.ReadOnly = !columnDescriptor.TextEditable;
            }

            return cell;

        }

        private void subscribeEventsForCell(DataGridViewCell cell, CustomDataGridViewColumnDescriptor<T> columnDescriptor)
        {
            ParameterlessChangeNotifierDelegate cellUpdaterDelegate = new ParameterlessChangeNotifierDelegate(() => updateCell(cell.ColumnIndex));
            foreach (string eventName in columnDescriptor.ChangeEvents) {
                try
                {
                    getEventInfoByName(eventName)?.AddEventHandler(item, cellUpdaterDelegate);
                }
                catch { }
            }
        }

        private void updateCell(int columnIndex)
        {
            getColumnDescriptor(columnIndex).UpdaterMethod?.Invoke(item, Cells[columnIndex]);
        }

        private static DataGridViewCell getCellByType(DataGridViewColumnType type)
        {
            switch (type)
            {
                case DataGridViewColumnType.TextBox:
                    return new DataGridViewTextBoxCell();
                case DataGridViewColumnType.CheckBox:
                    return new DataGridViewCheckBoxCell();
                case DataGridViewColumnType.Image:
                    return new DataGridViewImageCell();
                case DataGridViewColumnType.Button:
                    return new DataGridViewButtonCell();
                case DataGridViewColumnType.ComboBox:
                    return new DataGridViewComboBoxCell();
                case DataGridViewColumnType.Link:
                    return new DataGridViewLinkCell();
                case DataGridViewColumnType.DisableButton:
                    return new DataGridViewDisableButtonCell();
                case DataGridViewColumnType.ImageButton:
                    return new DataGridViewImageButtonCell();
            }
            return null;
        }

        public void HandleContentClick(DataGridViewCellEventArgs eventArgs)
        {
            int ci = eventArgs.ColumnIndex;
            getColumnDescriptor(ci).ContentClickHandlerMethod?.Invoke(item, Cells[ci], eventArgs);
        }

        public void HandleEndEdit(DataGridViewCellEventArgs eventArgs)
        {
            int ci = eventArgs.ColumnIndex;
            getColumnDescriptor(ci).EndEditHandlerMethod?.Invoke(item, Cells[ci], eventArgs);
        }

        private CustomDataGridViewColumnDescriptor<T> getColumnDescriptor(int columnIndex)
        {
            return table.ColumnDescriptors[columnIndex];
        }

    }
}