using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DynamicTextFunctionArgumentAttribute : Attribute
    {
        public readonly int Index;
        public readonly string Description;
        public DynamicTextFunctionArgumentAttribute(int Index, string Description)
        {
            this.Index = Index;
            this.Description = Description;
        }
    }
}
