﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{
    public interface IInstantiatorCollection<TItem>
    {
        TItem CreateEmptyInstance(string typeCode = null);
    }
}
