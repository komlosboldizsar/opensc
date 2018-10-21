using OpenSC.Model.Timers;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.TSL31;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{

    public partial class UmdPortEditorFormBase : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New UMD port";
        private const string TITLE_EDIT = "Edit UMD port: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New UMD port";
        private const string HEADER_TEXT_EDIT = "Edit UMD port";

        protected UmdPort port;

        bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), port?.ID, port?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), port?.ID, port?.Name);
            }
        }

        public UmdPortEditorFormBase()
        {
            InitializeComponent();
        }

        public UmdPortEditorFormBase(UmdPort port)
        {
            InitializeComponent();
            AddingNew = (port == null);
            if (port != null)
                this.port = port;
        }

        protected override void loadData()
        {
            if (port == null)
                return;
            idNumericField.Value = port.ID;
            nameTextBox.Text = port.Name;
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
            {
                UmdPortDatabase.Instance.Add(port);
            }

            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (port == null)
                return;
            port.ValidateId((int)idNumericField.Value);
            port.ValidateName(nameTextBox.Text);
        }

        protected virtual void writeFields()
        {
            if (port == null)
                return;
            port.ID = (int)idNumericField.Value;
            port.Name = nameTextBox.Text;
        }

    }

}
