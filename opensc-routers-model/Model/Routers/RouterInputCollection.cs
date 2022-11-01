using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public class RouterInputCollection : ComponentCollection<Router, RouterInput, RouterInputCollection>
    {

        public RouterInputCollection(Router owner) : base(owner)
        { }

        internal void RestoreSources()
        {
            foreach (RouterInput input in this)
                input.RestoreSource();
        }

        protected override RouterInput createInstance(string typeCode) => new();
        protected override string InstanceNameTemplate => "Input #{0}";

    }
}
