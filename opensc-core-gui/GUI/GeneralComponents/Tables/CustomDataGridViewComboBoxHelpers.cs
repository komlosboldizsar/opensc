using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{

    public static class CustomDataGridViewComboBoxHelpers
    {

        public static bool ContainsValue(this DataGridViewComboBoxCell comboBoxCell, object value)
        {
            foreach (object itemProxy in comboBoxCell.Items)
                if ((itemProxy as ICustomDataGridViewComboBoxItem).ObjValue == value)
                    return true;
            return false;
        }

    }

}
