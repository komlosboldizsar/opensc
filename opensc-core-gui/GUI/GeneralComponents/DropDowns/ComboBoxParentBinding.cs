using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.DropDowns
{

    public static class ComboBoxParentBinding
    {

        private static ParentBinder<ComboBox> BinderBase { get; } = new(selectIfCanMethod, canSelectMethod);

        private static bool selectIfCanMethod(ComboBox comboBox, object value) => comboBox.SelectIfContainsValue(value);
        private static bool canSelectMethod(ComboBox comboBox, object value) => comboBox.ContainsValue(value);

        public static void BindParent(this ComboBox child, ComboBox parent, Func<object, object> parentsValueSelector)
            => BinderBase.BindParent(child, parent, parentsValueSelector);

        public static bool SelectWithParentsHelp(this ComboBox comboBox, object value)
            => BinderBase.SelectWithParentsHelp(comboBox, value);

    }

}
