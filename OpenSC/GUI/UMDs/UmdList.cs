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

        private const int MAX_TALLIES = 2;

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
            private DataGridViewTextBoxCell staticTextCell;
            private DataGridViewCheckBoxCell useStaticTextCell;
            private DataGridViewTextBoxCell currentTextCell;
            private DataGridViewTextBoxCell[] tallyCells = new DataGridViewTextBoxCell[MAX_TALLIES];
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

                staticTextCell = new DataGridViewTextBoxCell()
                {
                    ReadOnly = false
                };
                this.Cells.Add(staticTextCell);

                useStaticTextCell = new DataGridViewCheckBoxCell()
                {
                    FalseValue = false,
                    TrueValue = true,
                    Value = false
                };
                this.Cells.Add(useStaticTextCell);

                currentTextCell = new DataGridViewTextBoxCell();
                this.Cells.Add(currentTextCell);

                for (int i = 0; i < MAX_TALLIES; i++)
                {
                    tallyCells[i] = new DataGridViewTextBoxCell();
                    tallyCells[i].Style.BackColor = Color.LightGray;
                    this.Cells.Add(tallyCells[i]);
                }

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
                for(int i = 0; i < MAX_TALLIES; i++)
                    updateTally(i);
                updateCurrentText();
                updateStaticText();
                updateUseStaticText();
            }

            private void updateUmdSettings()
            {
                idCell.Value = string.Format("#{0}", umd.ID);
                nameCell.Value = umd.Name;
            }

            private void updateTally(int index)
            {
                if(umd.Type.TallyCount > index)
                tallyCells[index].Style.BackColor = umd.TallyStates[index] ? umd.TallyColors[index] : Color.White;
            }

            private void updateCurrentText()
            {
                currentTextCell.Value = umd.CurrentText;
            }

            private void updateStaticText()
            {
                staticTextCell.Value = umd.StaticText;
            }

            private void updateUseStaticText()
            {
                useStaticTextCell.Value = umd.UseStaticText;
            }

            private void subscribeUmdEvents()
            {
                umd.NameChanged += umdNameChangedHandler;
                umd.IdChanged += umdIdChangedHandler;
                umd.TallyChanged += umdTallyChangedHandler;
                umd.StaticTextChanged += umdStaticTextChangedHandler;
                umd.CurrentTextChanged += umdCurrentTextChangedHandler;
                umd.UseStaticTextChanged += umdUseStaticTextChangedHandler;
            }

            public void HandleCellClick(object sender, DataGridViewCellEventArgs e)
            {

                // Static checkbox
                if (e.ColumnIndex == useStaticTextCell.ColumnIndex)
                {
                    umd.UseStaticText = !(bool)useStaticTextCell.Value;
                }

                // Edit
                if (e.ColumnIndex == editButtonCell.ColumnIndex)
                {
                    var editWindow = new UmdEditWindow(umd);
                    editWindow.ShowAsChild();
                    return;
                }

                // Delete
                if (e.ColumnIndex == deleteButtonCell.ColumnIndex)
                {
                    string msgBoxText = string.Format("Do you really want to delete this UMD?\n(#{0}) {1}", umd.ID, umd.Name);
                    var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                        UmdDatabase.Instance.Remove(umd);
                }

            }

            public void HandleCellEndEdit(object sender, DataGridViewCellEventArgs e)
            {

                // Static text
                if (e.ColumnIndex == staticTextCell.ColumnIndex)
                {
                    umd.StaticText = (string)staticTextCell.Value;
                }

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

            private void umdTallyChangedHandler(UMD umd, int index, bool oldState, bool newState)
            {
                if (umd != this.umd)
                    return;
                if (index >= MAX_TALLIES)
                    return;
                updateTally(index);
            }

            private void umdCurrentTextChangedHandler(UMD umd, string oldText, string newText)
            {
                if (umd != this.umd)
                    return;
                updateCurrentText();
            }

            private void umdStaticTextChangedHandler(UMD umd, string oldText, string newText)
            {
                if (umd != this.umd)
                    return;
                updateStaticText();
            }

            private void umdUseStaticTextChangedHandler(UMD umd, bool oldState, bool newState)
            {
                if (umd != this.umd)
                    return;
                updateUseStaticText();
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

        private void umdListTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (sender != umdListTable)
                return;
            UmdListTableRow rowObject = umdListTable.Rows[e.RowIndex] as UmdListTableRow;
            if (rowObject != null)
                rowObject.HandleCellEndEdit(sender, e);
        }

    }
}