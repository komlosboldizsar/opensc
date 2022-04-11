using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{
    public record UmdTextInfo(string Name, bool Switchable, bool DefaultUsed, bool Alignable, UmdTextAlignment DefaultAlignment)
    { }
}
