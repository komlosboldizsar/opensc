using OpenSC.Model.Streams;
using System;
using System.Windows.Forms;

namespace OpenSC.GUI.Streams
{
    public partial class StreamEditorFormBase : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New stream";
        private const string TITLE_EDIT = "Edit stream: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New stream";
        private const string HEADER_TEXT_EDIT = "Edit stream";

        private Stream stream;

        private bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), stream?.ID, stream?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), stream?.ID, stream?.Name);
            }
        }

        public StreamEditorFormBase(Stream stream)
        {
            InitializeComponent();
            AddingNew = (stream == null);
            if (stream != null)
                this.stream = stream;
        }

        protected override void loadData()
        {
            idNumericField.Value = stream.ID;
            nameTextBox.Text = stream.Name;
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
                StreamDatabase.Instance.Add(stream);

            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            validateFields();
            stream.ValidateId((int)idNumericField.Value);
            stream.ValidateName(nameTextBox.Text);
        }

        protected virtual void writeFields()
        {
            stream.ID = (int)idNumericField.Value;
            stream.Name = nameTextBox.Text;
        }

    }
}
