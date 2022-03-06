using OpenSC.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings
{

    public class Setting<T>: ISetting
    {

        private const string LOG_TAG = "Settings";

        public Setting(string key, string category, string humanReadableTitle, string humanReadableDescription, T defaultValue = default(T))
        {
            Key = key;
            Category = category;
            HumanReadableTitle = humanReadableTitle;
            HumanReadableDescription = humanReadableDescription;
            value = defaultValue;
            DefaultValue = defaultValue;
        }

        public string Key { get; init; }
        public string Category { get; init; }
        public string HumanReadableTitle { get; init; }
        public string HumanReadableDescription { get; init; }
        public bool Hidden { get; init; } = false;
        public Type ValueType { get; } = typeof(T);

        #region Value properties
        public event SettingValueChangedDelegate ValueChanged;

        private T value;

        public virtual T Value {
            get { return value; }
            set
            {
                T oldValue = value;
                if (EqualityComparer<T>.Default.Equals(this.value, value))
                    return;
                this.value = value;
                LogDispatcher.I(LOG_TAG, string.Format("Value of setting '{0}' changed.", Key));
                ValueChanged?.Invoke(this, oldValue, value);
            }
        }

        public object ObjValue
        {
            get { return value; }
            set { Value = (T)value; }
        }

        public T DefaultValue { get; private set; }
        #endregion

    }

}
