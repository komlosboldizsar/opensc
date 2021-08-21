using OpenSC.Model.Mixers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Mixers
{
    public class MixerEditorFormTypeRegister: ModelEditorFormTypeRegister<Mixer>
    {
        public static MixerEditorFormTypeRegister Instance { get; } = new MixerEditorFormTypeRegister();
    }
}
