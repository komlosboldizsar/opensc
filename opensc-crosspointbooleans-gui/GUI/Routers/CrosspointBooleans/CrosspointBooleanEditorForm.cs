﻿using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.CrosspointBooleans;
using OpenSC.Model.Variables;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers.CrosspointBooleans
{

    public partial class CrosspointBooleanEditorForm : ModelEditorFormBase, IModelEditorForm<CrosspointBoolean>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as CrosspointBoolean);
        public IModelEditorForm<CrosspointBoolean> GetInstanceT(CrosspointBoolean modelInstance) => new CrosspointBooleanEditorForm(modelInstance);

        public CrosspointBooleanEditorForm() : base() => InitializeComponent();

        public CrosspointBooleanEditorForm(CrosspointBoolean crosspointBoolean)
            : base(crosspointBoolean)
        {
            InitializeComponent();
            initRouterDropDown();
            routerDropDown.SelectedIndexChanged += selectedRouterChanged;
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<CrosspointBoolean, CrosspointBoolean>(this, CrosspointBooleanDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            CrosspointBoolean crosspointBoolean = ((CrosspointBoolean)EditedModel);
            if (crosspointBoolean == null)
                return;
            routerDropDown.SelectByValue(crosspointBoolean.WatchedRouter);
            routerInputDropDown.SelectByValue(crosspointBoolean.WatchedInput);
            routerOutputDropDown.SelectByValue(crosspointBoolean.WatchedOutput);
        }

        protected override void validateFields()
        {
            base.validateFields();
            CrosspointBoolean crosspointBoolean = ((CrosspointBoolean)EditedModel);
            if (crosspointBoolean == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            CrosspointBoolean crosspointBoolean = ((CrosspointBoolean)EditedModel);
            if (crosspointBoolean == null)
                return;
            crosspointBoolean.WatchedInput = routerInputDropDown.SelectedValue as RouterInput;
            crosspointBoolean.WatchedOutput = routerOutputDropDown.SelectedValue as RouterOutput;
        }

        private void initRouterDropDown()
        {
            routerDropDown.CreateAdapterAsDataSource(RouterDatabase.Instance, null, true, "(not associated)");
            routerDropDown.ReceiveObjectDrop().FilterByType<Router>();
        }

        private void updateRouterInputDropDown()
        {
            routerInputDropDown.CreateAdapterAsDataSource(
                (routerDropDown.SelectedValue as Router)?.Inputs,
                routerInput => string.Format("(#{0}) {1}", routerInput.Index, routerInput.Name),
                true, "(not associated)");
            routerInputDropDown.ReceiveObjectDrop().FilterByType<RouterInput>();
        }

        private void updateRouterOutputDropDown()
        {
            routerOutputDropDown.CreateAdapterAsDataSource(
                (routerDropDown.SelectedValue as Router)?.Outputs,
                routerOutput => string.Format("(#{0}) {1}", routerOutput.Index, routerOutput.Name),
                true, "(not associated)");
            routerOutputDropDown.ReceiveObjectDrop().FilterByType<RouterOutput>();
        }

        private void selectedRouterChanged(object sender, EventArgs e)
        {
            updateRouterInputDropDown();
            updateRouterOutputDropDown();
        }

    }

}
