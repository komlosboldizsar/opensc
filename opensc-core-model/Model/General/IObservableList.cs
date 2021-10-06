using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{

    public delegate void ObservableListItemAddedDelegate(IEnumerable addedItems);
    public delegate void ObservableListItemRemovedDelegate(IEnumerable removedItems);
    public delegate void ObservableListItemsChangedDelegate();

    public interface IObservableList: IEnumerable
    {
        event ObservableListItemAddedDelegate ItemAdded;
        event ObservableListItemRemovedDelegate ItemRemoved;
        event ObservableListItemsChangedDelegate ItemsChanged;
        int Count { get; }
    }

}
