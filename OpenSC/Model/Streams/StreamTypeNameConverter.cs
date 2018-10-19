﻿using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Streams
{

    class StreamTypeNameConverter : TypeNameConverterBase
    {

        private Dictionary<string, Type> KNOWN_TYPES = new Dictionary<string, Type>()
        {
            { "youtube", typeof(YoutubeStream) }
        };

        protected override Dictionary<string, Type> knownTypes => KNOWN_TYPES;

    }

}
