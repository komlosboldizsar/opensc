using OpenSC.Model.VTRs;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.VTRs
{
    public partial class VtrEditorFormBase : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New VTR";
        private const string TITLE_EDIT = "Edit VTR: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New VTR";
        private const string HEADER_TEXT_EDIT = "Edit VTR";

        protected Vtr vtr;

        private bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), vtr?.ID, vtr?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), vtr?.ID, vtr?.Name);
            }
        }

        public VtrEditorFormBase()
        {
            InitializeComponent();
        }

        public VtrEditorFormBase(Vtr vtr)
        {
            InitializeComponent();
            AddingNew = (vtr == null);
            if (vtr != null)
                this.vtr = vtr;
        }

        protected override void loadData()
        {
            if (vtr == null)
                return;
            idNumericField.Value = (addingNew ? VtrDatabase.Instance.NextValidId() : vtr.ID);
            nameTextBox.Text = vtr.Name;
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

            writeFields();
            if (addingNew)
                VtrDatabase.Instance.Add(vtr);

            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (vtr == null)
                return;
            vtr.ValidateId((int)idNumericField.Value);
            // TODO: validate name
        }

        protected virtual void writeFields()
        {
            if (vtr == null)
                return;
            vtr.ID = (int)idNumericField.Value;
            vtr.Name = nameTextBox.Text;
        }

    }
}
