using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC
{
    public partial class ChildWindowBase : Form
    {

        protected string HeaderText
        {
            get { return headerText.Text; }
            set { headerText.Text = value; }
        }

        public ChildWindowBase()
        {
            InitializeComponent();
        }

    }
}
