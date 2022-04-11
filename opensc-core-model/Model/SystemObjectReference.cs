using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    [Serializable]
    public sealed class SystemObjectReference : Object
    {
        public string GlobalID { get; init; }
        public SystemObjectReference(ISystemObject obj) => GlobalID = obj?.GlobalID;
        public SystemObjectReference(string globalId) => GlobalID = globalId;
        public ISystemObject Object => SystemObjectRegister.Instance[GlobalID];
    }
}
