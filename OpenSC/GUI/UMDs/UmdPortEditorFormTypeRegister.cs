using OpenSC.Model.UMDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.UMDs
{
    class UmdPortEditorFormTypeRegister: ModelEditorFormTypeRegister<UmdPort>
    {
        public static UmdPortEditorFormTypeRegister Instance { get; } = new UmdPortEditorFormTypeRegister();
    }
}
