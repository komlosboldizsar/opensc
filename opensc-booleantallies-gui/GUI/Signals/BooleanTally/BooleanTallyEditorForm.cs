﻿using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model;
using OpenSC.Model.Signals;
using OpenSC.Model.Signals.BooleanTallies;
using OpenSC.Model.Variables;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals.BooleanTallies
{

    public partial class BooleanTallyEditorForm : ModelEditorFormBase, IModelEditorForm<BooleanTally>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as BooleanTally);
        public IModelEditorForm<BooleanTally> GetInstanceT(BooleanTally modelInstance) => new BooleanTallyEditorForm(modelInstance);

        public BooleanTallyEditorForm() : base() => InitializeComponent();

        public BooleanTallyEditorForm(BooleanTally booleanTally)
            : base(booleanTally)
        {
            InitializeComponent();
            initFromBooleanDropDown();
            initToSignalDropDown();
            initToColorDropDown();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<BooleanTally, BooleanTally>(this, BooleanTallyDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            BooleanTally booleanTally = (BooleanTally)EditedModel;
            if (booleanTally == null)
                return;
            fromBooleanDropDown.SelectByValue(booleanTally.FromBoolean);
            toSignalDropDown.SelectByValue(booleanTally.ToSignal);
            toColorDropDown.SelectByValue(booleanTally.ToTallyColor);
        }

        protected override void validateFields()
        {
            base.validateFields();
            BooleanTally booleanTally = (BooleanTally)EditedModel;
            if (booleanTally == null)
                return;
            booleanTally.ValidateId((int)idNumericField.Value);
            //category.ValidateName(nameTextBox.Text);
        }

        protected override void writeFields()
        {
            base.writeFields();
            BooleanTally booleanTally = (BooleanTally)EditedModel;
            if (booleanTally == null)
                return;
            booleanTally.FromBoolean = fromBooleanDropDown.SelectedValue as IBoolean;
            booleanTally.ToSignal = toSignalDropDown.SelectedValue as ISignalSourceRegistered;
            booleanTally.ToTallyColor = (SignalTallyColor)toColorDropDown.SelectedValue;
        }

        private void initFromBooleanDropDown()
            => fromBooleanDropDown.CreateAdapterAsDataSource<IBoolean>(
                BooleanRegister.Instance,
                boolean => boolean.Name,
                true,
                "(not associated)");

        private void initToSignalDropDown()
            => toSignalDropDown.CreateAdapterAsDataSource<ISignalSourceRegistered>(
                SignalRegister.Instance,
                signal => string.Format("[{0}] {1}", signal.SignalUniqueId, signal.SignalLabel),
                true,
                "(not associated)");

        private void initToColorDropDown() =>
            toColorDropDown.SetAdapterAsDataSource(new EnumComboBoxAdapter<SignalTallyColor>());

    }

}