using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model.Signals;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals
{

    public partial class ExternalSignalEditorForm : ModelEditorFormBase, IModelEditorForm<ExternalSignal>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as ExternalSignal);
        public IModelEditorForm<ExternalSignal> GetInstanceT(ExternalSignal modelInstance) => new ExternalSignalEditorForm(modelInstance);
        
        public ExternalSignalEditorForm() : base() => InitializeComponent();

        public ExternalSignalEditorForm(ExternalSignal externalSignal) : base(externalSignal)
        {
            InitializeComponent();
            initCategoryDropDown();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<ExternalSignal, ExternalSignal>(this, ExternalSignalDatabases.Signals);

        protected override void loadData()
        {
            base.loadData();
            ExternalSignal externalSignal = (ExternalSignal)EditedModel;
            if (externalSignal == null)
                return;
            categoryDropDown.SelectByValue(externalSignal.Category);
        }

        protected override void validateFields()
        {
            base.validateFields();
            ExternalSignal externalSignal = (ExternalSignal)EditedModel;
            if (externalSignal == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            ExternalSignal externalSignal = (ExternalSignal)EditedModel;
            if (externalSignal == null)
                return;
            externalSignal.Category = categoryDropDown.SelectedValue as ExternalSignalCategory;
        }

        private void initCategoryDropDown()
        {
            categoryDropDown.CreateAdapterAsDataSource(ExternalSignalDatabases.Categories, null, true, "(not associated)");
            categoryDropDown.ReceiveSystemObjectDrop().FilterByType<ExternalSignalCategory>();
        }

    }

}
