using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.Tsl50;
using System;

namespace OpenSC.GUI.UMDs
{

    public partial class Tsl50DisplayUmdEditorForm : UmdEditorFormBase, IModelEditorForm<Umd>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Umd);
        public IModelEditorForm<Umd> GetInstanceT(Umd modelInstance) => new Tsl50DisplayUmdEditorForm(modelInstance);

        public Tsl50DisplayUmdEditorForm(): base() => InitializeComponent();

        public Tsl50DisplayUmdEditorForm(Umd umd) : base(umd)
        {
            InitializeComponent();
            if ((umd != null) && !(umd is Tsl50Display))
                throw new ArgumentException($"Type of UMD should be {nameof(Tsl50Display)}.", nameof(umd));
            initScreenDropDown();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Umd, Tsl50Display>(this, UmdDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            Tsl50Display tsl50Display = (Tsl50Display)EditedModel;
            if (tsl50Display == null)
                return;
            screenDropDown.SelectByValue(tsl50Display.Screen);
            indexNumericInput.Value = tsl50Display.Index;
        }

        protected override void writeFields()
        {
            base.writeFields();
            Tsl50Display tsl50Display = (Tsl50Display)EditedModel;
            if (tsl50Display == null)
                return;
            tsl50Display.Screen = screenDropDown.SelectedValue as Tsl50Screen;
            tsl50Display.Index = (int)indexNumericInput.Value;
        }

        protected override void validateFields()
        {
            base.validateFields();
            Tsl50Display tsl50Display = (Tsl50Display)EditedModel;
            if (tsl50Display == null)
                return;
            tsl50Display.ValidateIndex((int)indexNumericInput.Value);
        }

        private void initScreenDropDown()
        {
            screenDropDown.CreateAdapterAsDataSource(Tsl50ScreenDatabase.Instance, null, true, "(not associated)");
            screenDropDown.ReceiveSystemObjectDrop().FilterByType<Tsl50Screen>();
        }

    }

}
