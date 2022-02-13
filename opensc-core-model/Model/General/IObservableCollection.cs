using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{
    public interface IObservableCollection<T> : IObservableEnumerable<T>, ICollection<T>
    { }
}
