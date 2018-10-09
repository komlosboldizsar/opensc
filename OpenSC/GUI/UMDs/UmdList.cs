using OpenSC.GUI.GeneralComponents;
using OpenSC.Model.Timers;
using OpenSC.Model.UMDs;
using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = OpenSC.Model.Timers.Timer;

namespace OpenSC.GUI.UMDs
{
    public partial class UmdList : ChildWindowWithTitle
    {
        public UmdList()
        {
            InitializeComponent();
            HeaderText = "List of UMDs";
        }

        private void UmdList_Load(object sender, EventArgs e)
        {
            loadUMDs();
            UmdDatabase.Instance.ElementsChanged += umdDatabaseElementsChangedHandler;
        }

        private void UmdList_FormClosed(object sender, FormClosedEventArgs e)
        {
            UmdDatabase.Instance.ElementsChanged -= umdDatabaseElementsChangedHandler;
        }

        private void umdDatabaseElementsChangedHandler()
        {
            loadUMDs();
        }

        private void loadUMDs()
        {
            umdListTable.Rows.Clear();
            foreach(UMD umd in UmdDatabase.Instance.UMDs)
            {
                var row = new UmdListTableRow(umd);
                umdListTable.Rows.Add(row);
            }
        }

        private class UmdListTableRow : DataGridViewRow
        {

            private UMD umd;

            private DataGridViewTextBoxCell idCell;
            private DataGridViewTextBoxCell nameCell;
            private DataGridViewButtonCell editButtonCell;
            private DataGridViewButtonCell deleteButtonCell;

            public UmdListTableRow(UMD umd)
            {
                this.umd = umd;
                addCells();
                loadData();
                subscribeUmdEvents();
            }

            private void addCells()
            {
                idCell = new DataGridViewTextBoxCell();
                this.Cells.Add(idCell);

                nameCell = new DataGridViewTextBoxCell();
                this.Cells.Add(nameCell);

                editButtonCell = new DataGridViewButtonCell()
                {
                    Value = "Edit"
                };
                this.Cells.Add(editButtonCell);

                deleteButtonCell = new DataGridViewButtonCell()
                {
                    Value = "Delete"
                };
                this.Cells.Add(deleteButtonCell);

            }

            private void loadData()
            {
                updateUmdSettings();
            }

            private void updateUmdSettings()
            {
                idCell.Value = string.Format("#{0}", umd.ID);
                nameCell.Value = umd.Name;
            }

            private void subscribeUmdEvents()
            {
                umd.NameChanged += umdNameChangedHandler;
                umd.IdChanged += umdIdChangedHandler;
            }

            public void HandleCellClick(object sender, DataGridViewCellEventArgs e)
            {

                // Edit
                /*if(e.ColumnIndex == editButtonCell.ColumnIndex)
                {
                    var editWindow = new TimerEditWindow(timer);
                    editWindow.ShowAsChild();
                    return;
                }*/

            }

            private void umdIdChangedHandler(UMD umd, int oldValue, int newValue)
            {
                if (umd != this.umd)
                    return;
                updateUmdSettings();
            }

            private void umdNameChangedHandler(UMD umd, string oldName, string newName)
            {
                if (umd != this.umd)
                    return;
                updateUmdSettings();
            }

        }

        private void umdListTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender != umdListTable)
                return;
            UmdListTableRow rowObject = umdListTable.Rows[e.RowIndex] as UmdListTableRow;
            if (rowObject != null)
                rowObject.HandleCellClick(sender, e);
        }
    }
}