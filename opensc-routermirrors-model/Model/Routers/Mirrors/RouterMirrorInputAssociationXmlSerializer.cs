﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Mirrors
{
    public class RouterMirrorInputAssociationXmlSerializer : RouterMirrorAssociationXmlSerializer<RouterInput, RouterMirrorInputAssociation>
    {
        protected override int getItemIndex(RouterInput item) => item.Index;
    }
}
