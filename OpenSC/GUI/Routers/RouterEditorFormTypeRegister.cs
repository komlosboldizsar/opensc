using OpenSC.Model.Routers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Routers
{
    class RouterEditorFormTypeRegister: ModelEditorFormTypeRegister<Router>
    {
        public static RouterEditorFormTypeRegister Instance { get; } = new RouterEditorFormTypeRegister();
    }
}
