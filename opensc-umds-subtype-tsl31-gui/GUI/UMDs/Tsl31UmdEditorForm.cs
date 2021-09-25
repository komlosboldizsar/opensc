using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.TSL31;
using System;

namespace OpenSC.GUI.UMDs
{

    public partial class Tsl31UmdEditorForm : UmdEditorFormBase, IModelEditorForm<UMD>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as UMD);
        public IModelEditorForm<UMD> GetInstanceT(UMD modelInstance) => new Tsl31UmdEditorForm(modelInstance);

        public Tsl31UmdEditorForm(): base() => InitializeComponent();

        public Tsl31UmdEditorForm(UMD umd) : base(umd)
        {
            InitializeComponent();
            if ((umd != null) && !(umd is TSL31))
                throw new ArgumentException($"Type of UMD should be {nameof(TSL31)}.", nameof(umd));
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<UMD, TSL31>(this, UmdDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            TSL31 tsl31 = (TSL31)EditedModel;
            if (tsl31 == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            TSL31 tsl31 = (TSL31)EditedModel;
            if (tsl31 == null)
                return;
        }

        protected override void validateFields()
        {
            base.validateFields();
            TSL31 tsl31 = (TSL31)EditedModel;
            if (tsl31 == null)
                return;
        }

    }

}
