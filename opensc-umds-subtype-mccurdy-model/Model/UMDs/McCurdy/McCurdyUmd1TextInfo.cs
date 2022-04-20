using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{
    public record McCurdyUmd1TextInfo(string Name, bool Switchable, bool DefaultUsed, bool Alignable, UmdTextAlignment DefaultAlignment, int DefaultColumnWidth)
        : UmdTextInfo(Name, Switchable, DefaultUsed, Alignable, DefaultAlignment) { }
}
