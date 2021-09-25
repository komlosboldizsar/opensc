using OpenSC.Model.Signals;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals
{

    public partial class ExternalSignalCategoryEditorForm : ModelEditorFormBase, IModelEditorForm<ExternalSignalCategory>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as ExternalSignalCategory);
        public IModelEditorForm<ExternalSignalCategory> GetInstanceT(ExternalSignalCategory modelInstance) => new ExternalSignalCategoryEditorForm(modelInstance);

        public ExternalSignalCategoryEditorForm() : base() => InitializeComponent();
        public ExternalSignalCategoryEditorForm(ExternalSignalCategory externalSignalCategory) : base(externalSignalCategory) => InitializeComponent();

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<ExternalSignalCategory, ExternalSignalCategory>(this, ExternalSignalDatabases.Categories);

        protected override void loadData()
        {
            base.loadData();
            ExternalSignalCategory externalSignalCategory = (ExternalSignalCategory)EditedModel;
            if (externalSignalCategory == null)
                return;
            colorProperty = externalSignalCategory.Color;
        }

        protected override void validateFields()
        {
            base.validateFields();
            ExternalSignalCategory externalSignalCategory = (ExternalSignalCategory)EditedModel;
            if (externalSignalCategory == null)
                return;
            externalSignalCategory.ValidateId((int)idNumericField.Value);
            //category.ValidateName(nameTextBox.Text);
        }

        protected override void writeFields()
        {
            base.writeFields();
            ExternalSignalCategory externalSignalCategory = (ExternalSignalCategory)EditedModel;
            if (externalSignalCategory == null)
                return;
            externalSignalCategory.Color = colorProperty;
        }

        private Color _colorProperty;

        private Color colorProperty
        {
            get => _colorProperty;
            set
            {
                _colorProperty = value;
                colorPanel.BackColor = value;
                chooseColorDialog.Color = value;
            }
        }

        private void setColorButton_Click(object sender, EventArgs e)
        {
            if (chooseColorDialog.ShowDialog() == DialogResult.OK)
                colorProperty = chooseColorDialog.Color;
        }

    }

}
