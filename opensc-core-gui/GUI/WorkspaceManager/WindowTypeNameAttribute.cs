using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.WorkspaceManager
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WindowTypeNameAttribute: Attribute
    {
        public string TypeName { get; private set; }
        public WindowTypeNameAttribute(string TypeName)
        {
            this.TypeName = TypeName;
        }
    }
}
