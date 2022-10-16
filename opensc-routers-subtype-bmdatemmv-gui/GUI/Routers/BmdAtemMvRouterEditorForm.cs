using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Mixers;
using OpenSC.Model.Mixers.BlackMagicDesign;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.BmdAtemMv;
using System;
using System.Linq;

namespace OpenSC.GUI.Routers
{

    public partial class BmdAtemMvRouterEditorForm : RouterEditorFormBase, IModelEditorForm<Router>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Router);
        public IModelEditorForm<Router> GetInstanceT(Router modelInstance) => new BmdAtemMvRouterEditorForm(modelInstance);

        public BmdAtemMvRouterEditorForm() : base() => InitializeComponent();

        public BmdAtemMvRouterEditorForm(Router router) : base(router)
        {
            InitializeComponent();
            if ((router != null) && !(router is BmdAtemMvRouter))
                throw new ArgumentException($"Type of router should be {nameof(BmdAtemMvRouter)}.", nameof(router));
            initDropDowns();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Router, BmdAtemMvRouter>(this, RouterDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            BmdAtemMvRouter bmdAtemMvRouter = (BmdAtemMvRouter)EditedModel;
            if (bmdAtemMvRouter == null)
                return;
            mixerDropDown.SelectByValue(bmdAtemMvRouter.Mixer);
        }

        protected override void writeFields()
        {
            base.writeFields();
            BmdAtemMvRouter bmdAtemMvRouter = (BmdAtemMvRouter)EditedModel;
            if (bmdAtemMvRouter == null)
                return;
            bmdAtemMvRouter.Mixer = mixerDropDown.SelectedValue as Mixer;
        }

        protected override void validateFields()
        {
            base.validateFields();
            BmdAtemMvRouter bmdAtemMvRouter = (BmdAtemMvRouter)EditedModel;
            if (bmdAtemMvRouter == null)
                return;
        }

        private void initDropDowns()
        {
            mixerDropDown.CreateAdapterAsDataSource(MixerDatabase.Instance.OfType<BmdMixer>(), null, true, "(not associated)");
            mixerDropDown.ReceiveObjectDrop().FilterByType<Mixer>();
        }

    }

}
