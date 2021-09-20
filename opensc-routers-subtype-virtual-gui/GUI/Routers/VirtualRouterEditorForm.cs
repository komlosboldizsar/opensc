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

        public VirtualRouterEditorForm() : base()
        {
            InitializeComponent();
        }

        public VirtualRouterEditorForm(Router router) : base(router)
        {
            InitializeComponent();
            if (router == null)
                this.router = new VirtualRouter();
            else if (!(router is VirtualRouter))
                throw new ArgumentException();
        }

        protected override void loadData()
        {
            base.loadData();
            VirtualRouter virtualRouter = router as VirtualRouter;
            if (virtualRouter == null)
                return;
            // ...
        }

        protected override void writeFields()
        {
            base.writeFields();
            VirtualRouter virtualRouter = router as VirtualRouter;
            if (virtualRouter == null)
                return;
            // ...
        }

        protected override void validateFields()
        {
            base.validateFields();
            VirtualRouter virtualRouter = router as VirtualRouter;
            if (virtualRouter == null)
                return;
            // TODO: Validation
        }

    }

}
