using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.WorkspaceManager
{
    interface IPersistableWindow
    {
        void Restore(Dictionary<string, object> keyValuePairs);
        Dictionary<string, object> GetKeyValuePairs();
    }
}
