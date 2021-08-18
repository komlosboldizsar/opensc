using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{

    public delegate void PropertyChangedTwoValuesDelegate<TItem, TProperty>(TItem item, TProperty oldValue, TProperty newValue);
    public delegate void PropertyChangedOneValueDelegate<TItem, TProperty>(TItem item, TProperty newValue);
    public delegate void PropertyChangedNoValueDelegate<TItem>(TItem item);

    public class SystemObjectBase : INotifyPropertyChanged
    {

        public delegate void BeforeChangePropertyDelegate<TProperty>(TProperty oldValue, TProperty newValue);
        public delegate void AfterChangePropertyDelegate<TProperty>(TProperty oldValue, TProperty newValue);
        public delegate void PropertyValidatorDelegate<TProperty>(TProperty value);

        protected bool setProperty<TItem, TProperty>
            (TItem item, ref TProperty member, TProperty newValue,
            PropertyChangedTwoValuesDelegate<TItem, TProperty> propertyChangedDelegate,
            BeforeChangePropertyDelegate<TProperty> beforeChangePropertyDelegate = null,
            AfterChangePropertyDelegate<TProperty> afterChangePropertyDelegate = null,
            PropertyValidatorDelegate<TProperty> validator = null,
            [CallerMemberName] string propertyName = "")
        {
            validator?.Invoke(newValue);
            if (object.Equals(member, newValue))
                return false;
            TProperty oldValue = member;
            beforeChangePropertyDelegate?.Invoke(oldValue, newValue);
            member = newValue;
            afterChangePropertyDelegate?.Invoke(oldValue, newValue);
            propertyChangedDelegate?.Invoke(item, oldValue, newValue);
            RaisePropertyChanged(propertyName);
            return true;
        }

        protected bool setProperty<TItem, TProperty>
            (TItem item, ref TProperty member, TProperty newValue, PropertyChangedOneValueDelegate<TItem, TProperty> propertyChangedDelegate,
            BeforeChangePropertyDelegate<TProperty> beforeChangePropertyDelegate = null,
            AfterChangePropertyDelegate<TProperty> afterChangePropertyDelegate = null,
            PropertyValidatorDelegate<TProperty> validator = null,
            [CallerMemberName] string propertyName = "")
            => setProperty(item, ref member, newValue, (i, ov, nv) => propertyChangedDelegate?.Invoke(i, nv), beforeChangePropertyDelegate, afterChangePropertyDelegate, validator, propertyName);

        protected bool setProperty<TItem, TProperty>
            (TItem item, ref TProperty member, TProperty newValue, PropertyChangedNoValueDelegate<TItem> propertyChangedDelegate,
            BeforeChangePropertyDelegate<TProperty> beforeChangePropertyDelegate = null,
            AfterChangePropertyDelegate<TProperty> afterChangePropertyDelegate = null,
            PropertyValidatorDelegate<TProperty> validator = null,
            [CallerMemberName] string propertyName = "")
            => setProperty(item, ref member, newValue, (i, ov, nv) => propertyChangedDelegate?.Invoke(i), beforeChangePropertyDelegate, afterChangePropertyDelegate, validator, propertyName);

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedDelegate PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(propertyName);
        #endregion


    }

}
