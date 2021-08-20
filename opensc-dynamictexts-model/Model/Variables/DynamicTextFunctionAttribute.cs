using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DynamicTextFunctionAttribute : Attribute
    {
        public readonly string Name;
        public readonly string Description;
        public DynamicTextFunctionAttribute(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }
    }
}
