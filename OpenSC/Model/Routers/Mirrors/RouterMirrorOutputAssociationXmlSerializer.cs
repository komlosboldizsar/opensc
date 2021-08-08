using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Mirrors
{
    class RouterMirrorOutputAssociationXmlSerializer : RouterMirrorAssociationXmlSerializer<RouterOutput, RouterMirrorOutputAssociation>
    {
        protected override int getItemIndex(RouterOutput item) => item.Index;
    }
}
