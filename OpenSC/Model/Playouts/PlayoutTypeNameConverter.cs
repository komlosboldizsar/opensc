using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Playouts
{

    class PlayoutTypeNameConverter : TypeNameConverterBase
    {

        private Dictionary<string, Type> KNOWN_TYPES = new Dictionary<string, Type>()
        {
        };

        protected override Dictionary<string, Type> knownTypes => KNOWN_TYPES;

    }

}
