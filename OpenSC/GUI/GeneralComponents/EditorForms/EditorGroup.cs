using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.EditorForms
{
    public partial class EditorGroup : UserControl
    {

        [Category("Text"), Description("Caption of the contained group box.")]
        public string GroupBoxText
        {
            get { return groupBox.Text; }
            set { groupBox.Text = value; }
        }

        public EditorGroup()
        {
            InitializeComponent();
        }
    }
}
