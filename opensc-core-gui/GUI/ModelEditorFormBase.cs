using OpenSC.Model;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenSC.GUI
{
    public partial class ModelEditorFormBase : ChildWindowWithTitle
    {

        [Category("Texts"), Description("Singular form of the name of the edited object type."), Browsable(true)]
        public string SubjectSingular
        {
            get => subjectSingular;
            set
            {
                subjectSingular = value;
                doAutoSetTexts();
            }
        }

        private string subjectSingular = "subject";

        [Category("Texts"), Description("Plural form of the name of the edited object type."), Browsable(true)]
        public string SubjectPlural
        {
            get => subjectPlural;
            set
            {
                subjectPlural = value;
                doAutoSetTexts();
            }
        }

        private string subjectPlural = "subjects";

        [Category("Texts"), Description("Automatically set window's title and header text."), Browsable(true)]
        public bool AutoSetTexts
        {
            get => autoSetTexts;
            set
            {
                autoSetTexts = value;
                doAutoSetTexts();
            }
        }

        private bool autoSetTexts = true;

        protected virtual string TitleNew => string.Format("New {0}", SubjectSingular);
        protected virtual string TitleExisting => string.Format("Edit {0}: {1}", SubjectSingular, EditedModel);

        protected virtual string HeaderTextNew => string.Format("New {0}", SubjectSingular);
        protected virtual string HeaderTextExisting => string.Format("Edit {0}", SubjectSingular);

        [Category("Buttons"), Description("Visibility of Delete button at bottom-left.")]
        public bool DeleteButtonVisible
        {
            get => deleteButton.Visible;
            set => deleteButton.Visible = value;
        }

        private IModel editedModel = null;

        public IModel EditedModel
        {
            get => editedModel;
            protected set
            {
                editedModel = value;
                doAutoSetTexts();
            }
        }

        private bool addingNew = false;

        public bool AddingNew
        {
            get => addingNew;
            protected set
            {
                addingNew = value;
                doAutoSetTexts();
            }
        }

        private IModelEditorFormDataManager manager;
        protected virtual IModelEditorFormDataManager createManager() => null;

        public ModelEditorFormBase()
        {
            InitializeComponent();
            Load += windowLoaded;
            AddingNew = true;
        }

        public ModelEditorFormBase(IModel editedModel)
        {
            InitializeComponent();
            manager = createManager();
            Load += windowLoaded;
            AddingNew = (editedModel == null);
            EditedModel = (editedModel != null) ? editedModel : manager.CreateModel();
        }

        private void windowLoaded(object sender, EventArgs e)
        {
            doAutoSetTexts();
            loadData();
        }

        protected virtual void loadData()
        {
            if (editedModel == null)
                return;
            idNumericField.Value = (addingNew ? manager.GetNextValidId() : editedModel.ID);
            nameTextBox.Text = editedModel.Name;
        }

        protected virtual bool saveData()
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
            EditedModel.StartUpdate();
            writeFields();
            EditedModel.EndUpdate();
            if (addingNew)
                manager.AddToDatabase();
            AddingNew = false;
            return true;
        }

        protected virtual void validateFields()
        {
            if (editedModel == null)
                return;
            ((ModelBase)editedModel).ValidateId((int)idNumericField.Value);
            ((ModelBase)editedModel).ValidateName(nameTextBox.Text);
        }

        protected virtual void writeFields()
        {
            if (editedModel == null)
                return;
            editedModel.ID = (int)idNumericField.Value;
            editedModel.Name = nameTextBox.Text;
        }

        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            if (saveData())
                Close();
        }

        private void saveButton_Click(object sender, EventArgs e) => saveData();
        private void cancelButton_Click(object sender, EventArgs e) => Close();
        private void deleteButton_Click(object sender, EventArgs e)
        {
            string msgBoxText = string.Format("Do you really want to delete this {0}?\n{1}", SubjectSingular, editedModel);
            var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
                manager.DeleteFromDatabase();
        }

        protected void doAutoSetTexts()
        {
            if (!autoSetTexts)
                return;
            Text = addingNew ? TitleNew : TitleExisting;
            HeaderText = addingNew ? HeaderTextNew : HeaderTextExisting;
        }

    }
}
