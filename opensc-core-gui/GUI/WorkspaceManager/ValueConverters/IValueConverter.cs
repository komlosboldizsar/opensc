using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.WorkspaceManager.ValueConverters
{
    public interface IValueConverter
    {
        string Serialize(object obj);
        object Deserialize(string serialized);
    }
}
