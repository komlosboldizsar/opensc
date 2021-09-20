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

        private const string TITLE_NEW = "New signal category";
        private const string TITLE_EDIT = "Edit signal category: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New signal category";
        private const string HEADER_TEXT_EDIT = "Edit signal category";

        protected ExternalSignalCategory category;

        bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), category?.ID, category?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), category?.ID, category?.Name);
            }
        }

        public ExternalSignalCategoryEditorForm()
        {
            InitializeComponent();
        }

        public ExternalSignalCategoryEditorForm(ExternalSignalCategory category)
        {
            InitializeComponent();
            AddingNew = (category == null);
            this.category = (category != null) ? category : new ExternalSignalCategory();
        }

        protected override void loadData()
        {
            if (category == null)
                return;
            idNumericField.Value = (addingNew ? ExternalSignalDatabases.Categories.NextValidId() : category.ID);
            nameTextBox.Text = category.Name;
            colorProperty = category.Color;
        }

        protected sealed override bool saveData()
        {

            try
            {
                validateFields();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            category.StartUpdate();
            writeFields();
            category.EndUpdate();

            if (addingNew)
                ExternalSignalDatabases.Categories.Add(category);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (category == null)
                return;
            category.ValidateId((int)idNumericField.Value);
            //category.ValidateName(nameTextBox.Text);
        }

        protected virtual void writeFields()
        {
            if (category == null)
                return;
            category.ID = (int)idNumericField.Value;
            category.Name = nameTextBox.Text;
            category.Color = colorProperty;
        }

        private Color _colorProperty;

        private Color colorProperty
        {
            get { return _colorProperty; }
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
