using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.DropDowns
{

    public class ParentBinder<T>
        where T : Control
    {

        public ParentBinder(Func<T, object, bool> selectIfCanMethod, Func<T, object, bool> canSelectMethod)
        {
            this.selectIfCanMethod = selectIfCanMethod;
            this.canSelectMethod = canSelectMethod;
        }

        private Func<T, object, bool> selectIfCanMethod;
        private Func<T, object, bool> canSelectMethod;

        private Dictionary<T, Dictionary<T, Func<object, object>>> allParentBindingDataDictionary = new();

        public void BindParent(T child, T parent, Func<object, object> parentsValueSelector)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<T, Func<object, object>> parentBindingDataDictionary))
            {
                parentBindingDataDictionary = new();
                allParentBindingDataDictionary.Add(child, parentBindingDataDictionary);
            }
            if (!parentBindingDataDictionary.ContainsKey(parent))
                parentBindingDataDictionary.Add(parent, parentsValueSelector);
        }

        public bool SelectWithParentsHelp(T child, object value)
        {
            if (selectIfCanMethod(child, value))
                return true;
            if (CanParentsHelpSelection(child, value))
            {
                DoParentsHelpSelection(child, value);
                return selectIfCanMethod(child, value);
            }
            return false;
        }

        public bool CanParentsHelpSelection(T child, object childValue)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<T, Func<object, object>> parentBindingDataDictionary))
                return false;
            return parentBindingDataDictionary.All(pbd => CanHelpChildSelection(pbd.Key, child, childValue));
        }

        public bool CanHelpChildSelection(T parent, T child, object childValue)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<T, Func<object, object>> parentBindingDataDictionary))
                return false;
            if (!parentBindingDataDictionary.TryGetValue(parent, out Func<object, object> parentsValueSelector))
                return false;
            object myValue = parentsValueSelector(childValue);
            if (canSelectMethod(parent, myValue))
                return true;
            return CanParentsHelpSelection(parent, myValue);
        }

        public void DoParentsHelpSelection(T child, object childValue)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<T, Func<object, object>> parentBindingDataDictionary))
                return;
            foreach (KeyValuePair<T, Func<object, object>> parentBindingData in parentBindingDataDictionary)
                DoHelpChildSelection(parentBindingData.Key, child, childValue);
        }

        public void DoHelpChildSelection(T parent, T child, object childSelection)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<T, Func<object, object>> parentBindingDataDictionary))
                return;
            if (!parentBindingDataDictionary.TryGetValue(parent, out Func<object, object> parentValueSelector))
                return;
            object myValue = parentValueSelector(childSelection);
            if (selectIfCanMethod(parent, myValue))
                return;
            DoParentsHelpSelection(child, myValue);
            selectIfCanMethod(parent, myValue);
        }

    }

}
