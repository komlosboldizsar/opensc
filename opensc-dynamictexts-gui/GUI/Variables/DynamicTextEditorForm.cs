using OpenSC.Model.Variables;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Variables
{

    public partial class DynamicTextEditorForm : ModelEditorFormBase, IModelEditorForm<DynamicText>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as DynamicText);
        public IModelEditorForm<DynamicText> GetInstanceT(DynamicText modelInstance) => new DynamicTextEditorForm(modelInstance);

        public DynamicTextEditorForm() : base() => InitializeComponent();

        public DynamicTextEditorForm(DynamicText dynamicText) : base(dynamicText)
        {
            InitializeComponent();
            dynamicText.CurrentTextChanged += dynamicTextCurrentTextChangedHandler;
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<DynamicText, DynamicText>(this, DynamicTextDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            DynamicText dynamicText = (DynamicText)EditedModel;
            if (dynamicText == null)
                return;
            formulaTextBox.Text = dynamicText.Formula;
            currentTextTextBox.Text = dynamicText.CurrentText;
        }

        protected override void validateFields()
        {
            base.validateFields();
            DynamicText dynamicText = (DynamicText)EditedModel;
            if (dynamicText == null)
                return;
            dynamicText.ValidateId((int)idNumericField.Value);
            //dynamicText.ValidateName(nameTextBox.Text);
        }

        protected override void writeFields()
        {
            base.writeFields();
            DynamicText dynamicText = (DynamicText)EditedModel;
            if (dynamicText == null)
                return;
            dynamicText.Formula = formulaTextBox.Text;
        }

        private void dynamicTextCurrentTextChangedHandler(DynamicText dynamicText, string oldText, string newText)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => dynamicTextCurrentTextChangedHandler(dynamicText, oldText, newText)));
                return;
            }
            if (dynamicText == EditedModel)
                currentTextTextBox.Text = newText;
        }

    }

}
