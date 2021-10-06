using System;

namespace OpenSC.Model.Settings.Converters
{
    [AttributeUsage(AttributeTargets.Class)]
    class SettingConverterAttribute : Attribute
    {
        public Type Type { get; private set; }
        public SettingConverterAttribute(Type Type) => this.Type = Type;
    }
}
