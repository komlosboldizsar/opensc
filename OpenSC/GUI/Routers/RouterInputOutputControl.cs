using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers
{
    public partial class RouterInputOutputControl : UserControl
    {
        public RouterInputOutputControl()
        {
            InitializeComponent();
        }

        private Color activeColor = Color.IndianRed;
        private Color defaultColor = Color.White;

        public Color ActiveColor
        {
            set { activeColor = value; }
        }

        public Color DefaultColor
        {
            set { defaultColor = value; }
        }

        public bool IsActive
        {
            set
            {
                if (value)
                    button.BackColor = activeColor;
                else
                    button.BackColor = defaultColor;
            }
        }

        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public delegate void ButtonClickHandler(object sender, EventArgs e);
        public event ButtonClickHandler Clicked;

        private void button_Click(object sender, EventArgs e)
        {
            Clicked?.Invoke(this, e);
        }

    }
}