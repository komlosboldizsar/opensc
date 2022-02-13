using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Settings
{
    [AttributeUsage(AttributeTargets.Class)]
    class EditorForSettingValueAttribute : Attribute
    {
        public Type Type { get; private set; }
        public EditorForSettingValueAttribute(Type Type) => this.Type = Type;
    }
}
