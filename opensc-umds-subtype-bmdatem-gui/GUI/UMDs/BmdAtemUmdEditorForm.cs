using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Mixers;
using OpenSC.Model.Mixers.BlackMagicDesign;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.BmdAtem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSC.GUI.UMDs
{

    public partial class BmdAtemUmdEditorForm : UmdEditorFormBase, IModelEditorForm<UMD>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as UMD);
        public IModelEditorForm<UMD> GetInstanceT(UMD modelInstance) => new BmdAtemUmdEditorForm(modelInstance);

        public BmdAtemUmdEditorForm(): base() => InitializeComponent();

        public BmdAtemUmdEditorForm(UMD umd) : base(umd)
        {
            InitializeComponent();
            if ((umd != null) && !(umd is BmdAtem))
                throw new ArgumentException($"Type of UMD should be {nameof(BmdAtem)}.", nameof(umd));
            initMixerDropDown();
            initInputDropDown();
            mixerDropDown.SelectedIndexChanged += mixerDropDownSelectionChanged;
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<UMD, BmdAtem>(this, UmdDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            BmdAtem bmdAtem = (BmdAtem)EditedModel;
            if (bmdAtem == null)
                return;
            mixerDropDown.SelectByValue(bmdAtem.Input?.Mixer);
            inputDropDown.SelectByValue(bmdAtem.Input);
        }

        protected override void writeFields()
        {
            base.writeFields();
            BmdAtem bmdAtem = (BmdAtem)EditedModel;
            if (bmdAtem == null)
                return;
            bmdAtem.Input = inputDropDown.SelectedValue as MixerInput;
        }

        protected override void validateFields()
        {
            base.validateFields();
            BmdAtem bmdAtem = (BmdAtem)EditedModel;
            if (bmdAtem == null)
                return;
        }

        private void initMixerDropDown()
        {
            mixerDropDown.CreateAdapterAsDataSource(MixerDatabase.Instance.OfType<BmdMixer>(), null, true, "(not associated)");
            mixerDropDown.ReceiveSystemObjectDrop().FilterByType<Mixer>();
        }

        private void initInputDropDown()
        {
            populateInputDropDown();
            inputDropDown.ReceiveSystemObjectDrop().FilterByType<MixerInput>();
            inputDropDown.BindParent(mixerDropDown, mi => ((MixerInput)mi)?.Mixer);
        }

        private void populateInputDropDown()
        {
            Mixer selectedMixer = mixerDropDown.SelectedValue as Mixer;
            IEnumerable<MixerInput> inputs = null;
            inputs = selectedMixer?.Inputs;
            inputs ??= new MixerInput[] { };
            inputDropDown.CreateAdapterAsDataSource(inputs, i => $"(#{i.Index}) {i.Name}", true, "(not associated)");
        }

        private void mixerDropDownSelectionChanged(object sender, EventArgs e) => populateInputDropDown();

    }

}
