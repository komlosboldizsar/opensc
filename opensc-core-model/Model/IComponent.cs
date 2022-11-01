using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    public interface IComponent<TParent, TComponent, TCollection> :
        INotifyPropertyChanged
        where TComponent : IComponent<TParent, TComponent, TCollection>
        where TCollection : ComponentCollection<TParent, TComponent, TCollection>
    {
        TParent Parent { get; }
        int Index { get; set; }
        string Name { get; set; }
        event PropertyChangedTwoValuesDelegate<TComponent, int> IndexChanged;
        void Removed();
        void AssignParent(TParent parent, TCollection parentCollection);
    }
}
