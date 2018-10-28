﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{

    public interface IObservableList<T> : IObservableList, IEnumerable<T>
    {
        T this[int index] { get; }
    }

}
