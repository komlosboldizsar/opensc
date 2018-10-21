using OpenSC.Model.UMDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.UMDs
{
    class UmdEditorFormTypeRegister: ModelEditorFormTypeRegister<UMD>
    {
        public static UmdEditorFormTypeRegister Instance { get; } = new UmdEditorFormTypeRegister();
    }
}
