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
            MinValue = minValue;
            MaxValue = maxValue;
            generateArgumentExceptionMessage();
        }

        public int? MinValue { get; private set; }
        public int? MaxValue { get; private set; }

        public override int Value
        {
            get => base.Value;
            set
            {
                if ((MinValue != null) && (value < MinValue))
                    throwArgumentException();
                if ((MaxValue != null) && (value > MaxValue))
                    throwArgumentException();
                base.Value = value;
            }
        }

        private string argumentExceptionMessage;

        private void generateArgumentExceptionMessage()
        {
            if ((MinValue != null) && (MaxValue != null))
                argumentExceptionMessage = string.Format("Value must be between {0} and {1}.", MinValue, MaxValue);
            else if (MinValue != null)
                argumentExceptionMessage = string.Format("Value must be greater than {0}.", MinValue);
            else if (MaxValue != null)
                argumentExceptionMessage = string.Format("Value must be less than {0}.", MaxValue);
            else
                argumentExceptionMessage = "Value is not limited.";
        }

        private void throwArgumentException() => throw new ArgumentException(argumentExceptionMessage);


    }

}
