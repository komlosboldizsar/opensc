using OpenSC.Model.UMDs.Tsl50;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{

    public partial class Tsl50ScreenEditorForm : ModelEditorFormBase, IModelEditorForm<Tsl50Screen>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Tsl50Screen);
        public IModelEditorForm<Tsl50Screen> GetInstanceT(Tsl50Screen modelInstance) => new Tsl50ScreenEditorForm(modelInstance);

        public Tsl50ScreenEditorForm() : base() => InitializeComponent();
        public Tsl50ScreenEditorForm(Tsl50Screen tsl50screen) : base(tsl50screen) =>InitializeComponent();

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Tsl50Screen, Tsl50Screen>(this, Tsl50ScreenDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            Tsl50Screen tsl50screen = (Tsl50Screen)EditedModel;
            if (tsl50screen == null)
                return;
            ipAddressTextBox.Text = tsl50screen.IpAddress;
            portNumericInput.Value = tsl50screen.Port;
            indexNumericInput.Value = tsl50screen.Index;
        }

        protected override void validateFields()
        {
            base.validateFields();
            Tsl50Screen tsl50screen = (Tsl50Screen)EditedModel;
            if (tsl50screen == null)
                return;
            tsl50screen.IpAddress = ipAddressTextBox.Text;
            tsl50screen.Port = (int)portNumericInput.Value;
            tsl50screen.Index = (int)indexNumericInput.Value;
        }

        protected override void writeFields()
        {
            base.writeFields();
            Tsl50Screen tsl50screen = (Tsl50Screen)EditedModel;
            if (tsl50screen == null)
                return;
            tsl50screen.ValidatePort((int)portNumericInput.Value);
            tsl50screen.ValidateIndex((int)indexNumericInput.Value);
        }

    }

}
