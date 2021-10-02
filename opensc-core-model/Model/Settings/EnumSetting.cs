using OpenSC.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings
{

    public abstract class EnumSetting : Setting<Enum>
    {
        protected EnumSetting(string key, string category, string humanReadableTitle, string humanReadableDescription, Enum defaultValue = null)
            : base(key, category, humanReadableTitle, humanReadableDescription, defaultValue) { }
        public abstract Type EnumType { get; }
        public abstract Dictionary<Enum, string> Translations { get; }
        public abstract string NullTranslation { get; }
    }

    public class EnumSetting<TEnum> : EnumSetting
        where TEnum : Enum
    {

        public EnumSetting(string key, string category, string humanReadableTitle, string humanReadableDescription, TEnum defaultValue, Dictionary<TEnum, string> translations = null, string nullTranslation = null)
            : base(key, category, humanReadableTitle, humanReadableDescription, defaultValue)
        {
            this.translations = translations;
            this.nullTranslation = nullTranslation;
        }

        public TEnum EnumValue
        {
            get => (TEnum)Value;
            set => Value = value;
        }

        public override Type EnumType => typeof(TEnum);

        private Dictionary<TEnum, string> translations;
        public override Dictionary<Enum, string> Translations => translations?.Cast<Enum, string, TEnum, string>();
        private string nullTranslation;
        public override string NullTranslation => nullTranslation;

    }

}
