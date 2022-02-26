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
            mixerDropDown.SelectedIndexChanged += MixerDropDown_SelectedIndexChanged;
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
            nameTypeShortRadioButton.Checked = (bmdAtem.NameType == BmdAtemInputNameType.Short);
            nameTypeLongRadioButton.Checked = (bmdAtem.NameType == BmdAtemInputNameType.Long);
        }

        protected override void writeFields()
        {
            base.writeFields();
            BmdAtem bmdAtem = (BmdAtem)EditedModel;
            if (bmdAtem == null)
                return;
            bmdAtem.Input = inputDropDown.SelectedValue as MixerInput;
            bmdAtem.NameType = nameTypeShortRadioButton.Checked ? BmdAtemInputNameType.Short : BmdAtemInputNameType.Long;
        }

        protected override void validateFields()
        {
            base.validateFields();
            BmdAtem bmdAtem = (BmdAtem)EditedModel;
            if (bmdAtem == null)
                return;
        }

        private void initMixerDropDown()
            => mixerDropDown.CreateAdapterAsDataSource(MixerDatabase.Instance.OfType<BmdMixer>(), null, true, "(not associated)");

        private void initInputDropDown()
        {
            Mixer selectedMixer = mixerDropDown.SelectedValue as Mixer;
            IEnumerable<MixerInput> inputs = null;
            inputs = selectedMixer?.Inputs;
            inputs ??= new MixerInput[] { };
            inputDropDown.CreateAdapterAsDataSource(inputs, i => $"(#{i.Index}) {i.Name}", true, "(not associated)");
        }

        private void MixerDropDown_SelectedIndexChanged(object sender, EventArgs e)
            => initInputDropDown();

    }

}
