using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleAttribute : Attribute
    {
        public string Name { get; private set; }
        public string HumanReadableName { get; private set; }
        public string Description { get; private set; }
        public ModuleAttribute(string Name, string HumanReadableName, string Description)
        {
            this.Name = Name;
        }
    }
}
