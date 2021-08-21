using OpenSC.Model.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Streams
{
    public class StreamEditorFormTypeRegister: ModelEditorFormTypeRegister<Stream>
    {
        public static StreamEditorFormTypeRegister Instance { get; } = new StreamEditorFormTypeRegister();
    }
}
