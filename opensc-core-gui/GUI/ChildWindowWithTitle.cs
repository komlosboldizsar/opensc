using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI
{
    public partial class ChildWindowWithTitle : ChildWindowBase
    {

        [Category("Header"), Description("Text in header bar.")]
        public string HeaderText
        {
            get { return headerText.Text; }
            set { headerText.Text = value; }
        }

        [Category("Header"), Description("Color of header bar.")]
        public Color HeaderColor
        {
            get { return headerPanel.BackColor; }
            set { headerPanel.BackColor = value; }
        }

        public ChildWindowWithTitle()
        {
            InitializeComponent();
        }

    }
}
