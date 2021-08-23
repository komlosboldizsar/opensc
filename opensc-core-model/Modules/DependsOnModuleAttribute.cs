using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Modules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnModuleAttribute : Attribute
    {
        public Type Module { get; private set; }
        public DependsOnModuleAttribute(Type Module)
        {
            this.Module = Module;
        }
    }
}
