using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings
{

    public class IntSetting : Setting<int>
    {
        public IntSetting(string key, string category, string humanReadableTitle, string humanReadableDescription, int defaultValue, int? minValue, int? maxValue)
            : base(key, category, humanReadableTitle, humanReadableDescription, defaultValue)
        {
            if ((minValue != null) && (maxValue != null))
                argumentExceptionMessage = string.Format("Value must be between {0} and {1}.", minValue, maxValue);
            else if (minValue != null)
                argumentExceptionMessage = string.Format("Value must be greater than {0}.", minValue);
            else if (maxValue != null)
                argumentExceptionMessage = string.Format("Value must be less than {0}.", maxValue);
            else
                argumentExceptionMessage = "Value is not limited.";
        }

        private string argumentExceptionMessage;

        public int? MinValue { get; private set; }
        public int? MaxValue { get; private set; }

        public override int Value
        {
            get => base.Value;
            set
            {
                if ((MinValue != null) && (value < MinValue))
                    throw new ArgumentException(argumentExceptionMessage);
                if ((MaxValue != null) && (value > MaxValue))
                    throw new ArgumentException(argumentExceptionMessage);
                base.Value = value;
            }
        }

    }

}
