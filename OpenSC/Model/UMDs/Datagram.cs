using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{
    struct Datagram
    {
        public string Text;
        public bool[] Tallies;
        public DateTime ValidUntil;
    }
}
