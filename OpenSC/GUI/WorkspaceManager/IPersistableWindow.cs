using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.WorkspaceManager
{
    interface IPersistableWindow
    {
        void RestoreData(Point position, Size size, Dictionary<string, object> keyValuePairs);
        void RestoreWindow();
        Dictionary<string, object> GetKeyValuePairs();
        Size Size { get; }
        Point Position { get; }
    }
}
