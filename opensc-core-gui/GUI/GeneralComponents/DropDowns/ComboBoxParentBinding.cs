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

        private static Dictionary<ComboBox, Dictionary<ComboBox, Func<object, object>>> allParentBindingDataDictionary = new();

        public static void BindParent(this ComboBox child, ComboBox parent, Func<object, object> parentsValueSelector)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<ComboBox, Func<object, object>> parentBindingDataDictionary))
            {
                parentBindingDataDictionary = new();
                allParentBindingDataDictionary.Add(child, parentBindingDataDictionary);
            }
            if (!parentBindingDataDictionary.ContainsKey(parent))
                parentBindingDataDictionary.Add(parent, parentsValueSelector);
        }

        public static bool SelectWithParentsHelp(this ComboBox comboBox, object value)
        {
            if (comboBox.SelectIfContainsValue(value))
                return true;
            if (comboBox.CanParentsHelpSelection(value))
            {
                comboBox.DoParentsHelpSelection(value);
                return comboBox.SelectIfContainsValue(value);
            }
            return false;
        }

        public static bool CanParentsHelpSelection(this ComboBox child, object childValue)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<ComboBox, Func<object, object>> parentBindingDataDictionary))
                return false;
            return parentBindingDataDictionary.All(pbd => pbd.Key.CanHelpChildSelection(child, childValue));
        }

        public static bool CanHelpChildSelection(this ComboBox parent, ComboBox child, object childValue)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<ComboBox, Func<object, object>> parentBindingDataDictionary))
                return false;
            if (!parentBindingDataDictionary.TryGetValue(parent, out Func<object, object> parentsValueSelector))
                return false;
            object myValue = parentsValueSelector(childValue);
            if (parent.ContainsValue(myValue))
                return true;
            return parent.CanParentsHelpSelection(myValue);
        }

        public static void DoParentsHelpSelection(this ComboBox child, object childValue)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<ComboBox, Func<object, object>> parentBindingDataDictionary))
                return;
            foreach (KeyValuePair<ComboBox, Func<object, object>> parentBindingData in parentBindingDataDictionary)
                parentBindingData.Key.DoHelpChildSelection(child, childValue);
        }

        public static void DoHelpChildSelection(this ComboBox parent, ComboBox child, object childSelection)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<ComboBox, Func<object, object>> parentBindingDataDictionary))
                return;
            if (!parentBindingDataDictionary.TryGetValue(parent, out Func<object, object> parentValueSelector))
                return;
            object myValue = parentValueSelector(childSelection);
            if (parent.SelectIfContainsValue(myValue))
                return;
            parent.DoParentsHelpSelection(myValue);
            parent.SelectIfContainsValue(myValue);
        }

    }

}
