using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.EditorForms
{
    public partial class EditorLabel : Label
    {

        public EditorLabel()
        {
            InitializeComponent();
        }

        public EditorLabel(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

    }
}
