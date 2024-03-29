﻿using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Salvos;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers.Salvos
{

    public partial class SalvoEditorForm : ModelEditorFormBase, IModelEditorForm<Salvo>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Salvo);
        public IModelEditorForm<Salvo> GetInstanceT(Salvo modelInstance) => new SalvoEditorForm(modelInstance);

        public SalvoEditorForm() : base() => InitializeComponent();
        public SalvoEditorForm(Salvo salvo) : base(salvo) => InitializeComponent();

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Salvo, Salvo>(this, SalvoDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            Salvo salvo = (Salvo)EditedModel;
            if (salvo == null)
                return;
            initInputsTable();
        }

        protected override void validateFields()
        {
            base.validateFields();
            Salvo salvo = (Salvo)EditedModel;
            if (salvo == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            Salvo salvo = (Salvo)EditedModel;
            if (salvo == null)
                return;
        }

        private CustomDataGridView<SalvoCrosspoint> crosspointsTableCDGV;

        private void initInputsTable()
        {

            Salvo salvo = (Salvo)EditedModel;
            crosspointsTableCDGV = createTable<SalvoCrosspoint>(crosspointsTableContainerPanel, ref this.crosspointsTable);
            CustomDataGridViewColumnDescriptorBuilder<SalvoCrosspoint> builder, routerBuilder;

            // Column: router
            CustomDataGridViewComboBoxItem<Router>[] routers = getArrayForDropDown(RouterDatabase.Instance);
            builder = getColumnDescriptorBuilderForTable<SalvoCrosspoint>(crosspointsTableCDGV);
            builder.Type(DataGridViewColumnType.ComboBox);
            builder.Header("Router");
            builder.Width(180);
            builder.InitializerMethod((crosspoint, cell) => { });
            builder.UpdaterMethod((crosspoint, cell) => { cell.Value = crosspoint.Router; });
            builder.CellValueChangedHandlerMethod((crosspoint, cell, eventargs) => {
                ((DataGridViewComboBoxCell)cell.OwningRow.Cells[1]).Value = null;
                ((DataGridViewComboBoxCell)cell.OwningRow.Cells[2]).Value = null;
                ((DataGridViewComboBoxCell)cell.OwningRow.Cells[1]).Items.Clear();
                ((DataGridViewComboBoxCell)cell.OwningRow.Cells[2]).Items.Clear();
                ((DataGridViewComboBoxCell)cell.OwningRow.Cells[1]).Items.AddRange(getArrayForDropDown((cell.Value as Router)?.Outputs));
                ((DataGridViewComboBoxCell)cell.OwningRow.Cells[2]).Items.AddRange(getArrayForDropDown((cell.Value as Router)?.Inputs));
            });
            builder.DropDownPopulatorMethod((crosspoint, cell) => routers);
            builder.BuildAndAdd();
            routerBuilder = builder;

            // Column: output
            builder = getColumnDescriptorBuilderForTable<SalvoCrosspoint>(crosspointsTableCDGV);
            builder.Type(DataGridViewColumnType.ComboBox);
            builder.Header("Output");
            builder.Width(180);
            builder.InitializerMethod((crosspoint, cell) => { });
            builder.UpdaterMethod((crosspoint, cell) => { cell.Value = crosspoint.Output; });
            builder.CellEndEditHandlerMethod((crosspoint, cell, eventargs) => { crosspoint.Output = cell.Value as RouterOutput; });
            builder.DropDownPopulatorMethod((crosspoint, cell) => getArrayForDropDown<RouterOutput>(crosspoint.Router?.Outputs));
            builder.ReceiveObjectDrop().FilterByType<RouterOutput>();
            builder.BindParent(routerBuilder, ro => ((RouterOutput)ro)?.Router);
            builder.AddChangeEvent(nameof(SalvoCrosspoint.Output));
            builder.BuildAndAdd();

            // Column: input
            builder = getColumnDescriptorBuilderForTable<SalvoCrosspoint>(crosspointsTableCDGV);
            builder.Type(DataGridViewColumnType.ComboBox);
            builder.Header("Input");
            builder.Width(180);
            builder.InitializerMethod((crosspoint, cell) => { });
            builder.UpdaterMethod((crosspoint, cell) => { cell.Value = crosspoint.Input; });
            builder.CellEndEditHandlerMethod((crosspoint, cell, eventargs) => { crosspoint.Input = cell.Value as RouterInput; });
            builder.DropDownPopulatorMethod((crosspoint, cell) => getArrayForDropDown<RouterInput>(crosspoint.Router?.Inputs));
            builder.ReceiveObjectDrop().FilterByType<RouterInput>();
            builder.BindParent(routerBuilder, ri => ((RouterInput)ri)?.Router);
            builder.AddChangeEvent(nameof(SalvoCrosspoint.Input));
            builder.BuildAndAdd();

            // Column: delete button
            builder = getColumnDescriptorBuilderForTable<SalvoCrosspoint>(crosspointsTableCDGV);
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Delete");
            builder.Width(70);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((crosspoint, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this crosspoint?\nRouter: {0}\nOutput: {1}\nInput: {2}", crosspoint.Router, crosspoint.Output, crosspoint.Input);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    salvo.RemoveCrosspoint(crosspoint);
            });
            builder.BuildAndAdd();

            crosspointsTableCDGV
                .ReceiveSystemObjectDrop(crosspointsTableObjectDropReceiver)
                .EnableMulti()
                .FilterByType<RouterOutput>();

            // Bind collection
            crosspointsTableCDGV.BoundCollection = salvo.Crosspoints;

        }

        public CustomDataGridView<T> createTable<T>(Control container, ref DataGridView originalTableMember)
            where T : class
        {
            var customTable = new CustomDataGridView<T>();
            container.Controls.Clear();
            container.Controls.Add(customTable);
            customTable.Dock = DockStyle.Fill;
            originalTableMember = customTable;
            return customTable;
        }

        private CustomDataGridViewColumnDescriptorBuilder<T> getColumnDescriptorBuilderForTable<T>(CustomDataGridView<T> table)
            where T : class
            => new CustomDataGridViewColumnDescriptorBuilder<T>(table);

        private CustomDataGridViewComboBoxItem<T>[] getArrayForDropDown<T>(IEnumerable<T> sourceCollection, string nullItemLabel = "(not selected)")
            where T : class
        {
            List<CustomDataGridViewComboBoxItem<T>> list = new List<CustomDataGridViewComboBoxItem<T>>();
            list.Add(new CustomDataGridViewComboBoxItem<T>.NullItem(nullItemLabel));
            if (sourceCollection != null)
                foreach (T item in sourceCollection)
                    list.Add(new CustomDataGridViewComboBoxItem<T>(item));
            return list.ToArray();
        }

        private void addCrosspointButton_Click(object sender, EventArgs e) => ((Salvo)EditedModel).AddCrosspoint();
        private void storeCurrentCrosspointsButton_Click(object sender, EventArgs e) => ((Salvo)EditedModel).StoreCurrentCrosspoints();

        private void crosspointsTableObjectDropReceiver(DataGridView table, IEnumerable<object> objects)
        {
            Salvo salvo = (Salvo)EditedModel;
            if (objects.First() is RouterOutput)
            {
                foreach (RouterOutput routerOutput in objects.Cast<RouterOutput>())
                    if (!(salvo.Crosspoints.Any(cp => cp.Output == routerOutput)))
                        salvo.AddCrosspoint(routerOutput, null);
                return;
            }
        }

    }

}
