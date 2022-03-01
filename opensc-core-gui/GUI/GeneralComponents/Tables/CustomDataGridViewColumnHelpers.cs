using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{

    public static class CustomDataGridViewColumnHelpers
    {

        public static string GetID(this DataGridViewColumn column)
            => (column.Tag as CustomDataGridViewColumnTag)?.ID;

    }

}
