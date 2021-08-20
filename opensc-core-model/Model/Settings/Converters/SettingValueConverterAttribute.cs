using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings.Converters
{
    [AttributeUsage(AttributeTargets.Class)]
    class SettingValueConverterAttribute: Attribute
    {
        public Type Type { get; private set; }
        public SettingValueConverterAttribute(Type Type)
        {
            this.Type = Type;
        }
    }
}
