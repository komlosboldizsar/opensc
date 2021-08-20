using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenSC.GUI
{
    public partial class ModelEditorFormBase : ChildWindowWithTitle
    {

        [Category("Buttons"), Description("Visibility of Delete button at bottom-left.")]
        public bool DeleteButtonVisible
        {
            get { return deleteButton.Visible; }
            set { deleteButton.Visible = value; }
        }

        public ModelEditorFormBase()
        {
            InitializeComponent();
            Load += windowLoaded;
        }

        private void windowLoaded(object sender, EventArgs e)
        {
            loadData();
        }

        protected virtual void loadData()
        { }

        protected virtual bool saveData()
        {
            return true;
        }

        protected virtual void delete()
        { }

        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            if (saveData())
                Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveData();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            delete();
        }

    }
}
