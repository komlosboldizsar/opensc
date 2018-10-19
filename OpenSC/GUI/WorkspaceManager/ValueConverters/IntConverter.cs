using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.WorkspaceManager.ValueConverters
{
    [ConverterType(typeof(int), "int")]
    class IntConverter : IValueConverter
    {

        public string Serialize(object obj)
        {
            return obj.ToString();
        }

        public object Deserialize(string serialized)
        {
            if (!int.TryParse(serialized, out int result))
                return 0;
            return result;
        }

    }

}
