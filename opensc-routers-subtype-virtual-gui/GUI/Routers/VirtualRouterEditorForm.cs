using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Virtual;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers
{

    public partial class VirtualRouterEditorForm : RouterEditorFormBase, IModelEditorForm<Router>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Router);
        public IModelEditorForm<Router> GetInstanceT(Router modelInstance) => new VirtualRouterEditorForm(modelInstance);

        public VirtualRouterEditorForm() : base() => InitializeComponent();

        public VirtualRouterEditorForm(Router router) : base(router)
        {
            InitializeComponent();
            if ((router != null) && !(router is VirtualRouter))
                throw new ArgumentException($"Type of router should be {nameof(VirtualRouter)}.", nameof(router));
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Router, VirtualRouter>(this, RouterDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            VirtualRouter virtualRouter = (VirtualRouter)EditedModel;
            if (virtualRouter == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            VirtualRouter virtualRouter = (VirtualRouter)EditedModel;
            if (virtualRouter == null)
                return;
        }

        protected override void validateFields()
        {
            base.validateFields();
            VirtualRouter virtualRouter = (VirtualRouter)EditedModel;
            if (virtualRouter == null)
                return;
        }

    }

}
