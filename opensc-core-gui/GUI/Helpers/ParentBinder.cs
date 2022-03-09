using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Helpers
{

    public class ParentBinder<TControl, TSelectEventArgs>
        where TSelectEventArgs : ParentBinderSelectEventArgs<TControl>
    {

        public ParentBinder(Func<TControl, object, TSelectEventArgs, bool> selectIfCanMethod, Func<TControl, object, TSelectEventArgs, bool> canSelectMethod)
        {
            this.selectIfCanMethod = selectIfCanMethod;
            this.canSelectMethod = canSelectMethod;
        }

        private Func<TControl, object, TSelectEventArgs, bool> selectIfCanMethod;
        private Func<TControl, object, TSelectEventArgs, bool> canSelectMethod;

        private Dictionary<TControl, Dictionary<TControl, Func<object, object>>> allParentBindingDataDictionary = new();

        public void BindParent(TControl child, TControl parent, Func<object, object> parentsValueSelector)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<TControl, Func<object, object>> parentBindingDataDictionary))
            {
                parentBindingDataDictionary = new();
                allParentBindingDataDictionary.Add(child, parentBindingDataDictionary);
            }
            if (!parentBindingDataDictionary.ContainsKey(parent))
                parentBindingDataDictionary.Add(parent, parentsValueSelector);
        }

        public bool SelectWithParentsHelp(TControl child, object value, TSelectEventArgs eventArgs = null)
        {
            if (selectIfCanMethod(child, value, eventArgs))
                return true;
            if (CanParentsHelpSelection(child, value, eventArgs))
            {
                DoParentsHelpSelection(child, value, eventArgs);
                return selectIfCanMethod(child, value, eventArgs);
            }
            return false;
        }

        public bool CanParentsHelpSelection(TControl child, object childValue, TSelectEventArgs eventArgs = null)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<TControl, Func<object, object>> parentBindingDataDictionary))
                return false;
            return parentBindingDataDictionary.All(pbd => canHelpChildSelection(pbd.Key, child, childValue, eventArgs));
        }

        private bool canHelpChildSelection(TControl parent, TControl child, object childValue, TSelectEventArgs eventArgs)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<TControl, Func<object, object>> parentBindingDataDictionary))
                return false;
            if (!parentBindingDataDictionary.TryGetValue(parent, out Func<object, object> parentsValueSelector))
                return false;
            object myValue = parentsValueSelector(childValue);
            if (canSelectMethod(parent, myValue, eventArgs))
                return true;
            return CanParentsHelpSelection(parent, myValue, eventArgs);
        }

        public void DoParentsHelpSelection(TControl child, object childValue, TSelectEventArgs eventArgs = null)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<TControl, Func<object, object>> parentBindingDataDictionary))
                return;
            foreach (KeyValuePair<TControl, Func<object, object>> parentBindingData in parentBindingDataDictionary)
                doHelpChildSelection(parentBindingData.Key, child, childValue, eventArgs);
        }

        private void doHelpChildSelection(TControl parent, TControl child, object childSelection, TSelectEventArgs eventArgs)
        {
            if (!allParentBindingDataDictionary.TryGetValue(child, out Dictionary<TControl, Func<object, object>> parentBindingDataDictionary))
                return;
            if (!parentBindingDataDictionary.TryGetValue(parent, out Func<object, object> parentValueSelector))
                return;
            object myValue = parentValueSelector(childSelection);
            if (selectIfCanMethod(parent, myValue, eventArgs))
                return;
            DoParentsHelpSelection(child, myValue, eventArgs);
            selectIfCanMethod(parent, myValue, eventArgs);
        }

    }

}
