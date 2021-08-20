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
        }

        public string Key { get; private set; }
        public string Category { get; private set; }
        public string HumanReadableTitle { get; private set; }
        public string HumanReadableDescription { get; private set; }
        public Type Type { get; } = typeof(T);

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
        #endregion

    }

}
