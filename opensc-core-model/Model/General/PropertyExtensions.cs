using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{

    public delegate void PropertyChangedTwoValuesDelegate<TItem, TProperty>(TItem item, TProperty oldValue, TProperty newValue)
        where TItem : INotifyPropertyChanged;
    public delegate void PropertyChangedOneValueDelegate<TItem, TProperty>(TItem item, TProperty newValue)
        where TItem : INotifyPropertyChanged;
    public delegate void PropertyChangedNoValueDelegate<TItem>(TItem item)
        where TItem : INotifyPropertyChanged;

    public delegate void BeforeChangePropertyDelegate<TProperty>(TProperty oldValue, TProperty newValue);
    public delegate void AfterChangePropertyDelegate<TProperty>(TProperty oldValue, TProperty newValue);
    public delegate void PropertyValidatorDelegate<TProperty>(TProperty value);

    public static class PropertyExtensions
    {

        public static bool setProperty<TItem, TProperty>
               (this TItem item, ref TProperty member, TProperty newValue,
               PropertyChangedTwoValuesDelegate<TItem, TProperty> propertyChangedDelegate,
               BeforeChangePropertyDelegate<TProperty> beforeChangePropertyDelegate = null,
               AfterChangePropertyDelegate<TProperty> afterChangePropertyDelegate = null,
               PropertyValidatorDelegate<TProperty> validator = null,
               [CallerMemberName] string propertyName = "")
            where TItem : INotifyPropertyChanged
        {
            validator?.Invoke(newValue);
            if (Equals(member, newValue))
                return false;
            TProperty oldValue = member;
            beforeChangePropertyDelegate?.Invoke(oldValue, newValue);
            member = newValue;
            afterChangePropertyDelegate?.Invoke(oldValue, newValue);
            propertyChangedDelegate?.Invoke(item, oldValue, newValue);
            item.RaisePropertyChanged(propertyName);
            return true;
        }

        public static bool setProperty<TItem, TProperty>
            (this TItem item, ref TProperty member, TProperty newValue, PropertyChangedOneValueDelegate<TItem, TProperty> propertyChangedDelegate,
            BeforeChangePropertyDelegate<TProperty> beforeChangePropertyDelegate = null,
            AfterChangePropertyDelegate<TProperty> afterChangePropertyDelegate = null,
            PropertyValidatorDelegate<TProperty> validator = null,
            [CallerMemberName] string propertyName = "")
            where TItem : INotifyPropertyChanged
            => item.setProperty(ref member, newValue, (i, ov, nv) => propertyChangedDelegate?.Invoke(i, nv), beforeChangePropertyDelegate, afterChangePropertyDelegate, validator, propertyName);

        public static bool setProperty<TItem, TProperty>
            (this TItem item, ref TProperty member, TProperty newValue, PropertyChangedNoValueDelegate<TItem> propertyChangedDelegate,
            BeforeChangePropertyDelegate<TProperty> beforeChangePropertyDelegate = null,
            AfterChangePropertyDelegate<TProperty> afterChangePropertyDelegate = null,
            PropertyValidatorDelegate<TProperty> validator = null,
            [CallerMemberName] string propertyName = "")
            where TItem : INotifyPropertyChanged
            => item.setProperty(ref member, newValue, (i, ov, nv) => propertyChangedDelegate?.Invoke(i), beforeChangePropertyDelegate, afterChangePropertyDelegate, validator, propertyName);

    }
}