using OpenSC.Model.Streams;
using OpenSC.Model.UMDs;
using OpenSC.Model.VTRs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.UMDs
{
    class VtrEditorFormTypeRegister: ModelEditorFormTypeRegister<Vtr>
    {
        public static VtrEditorFormTypeRegister Instance { get; } = new VtrEditorFormTypeRegister();
    }
}
