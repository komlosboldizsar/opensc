using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{

    public delegate void ObservableListItemAddedDelegate();
    public delegate void ObservableListItemRemovedDelegate();
    public delegate void ObservableListItemsChangedDelegate();

    public interface IObservableList
    {
        event ObservableListItemAddedDelegate ItemAdded;
        event ObservableListItemRemovedDelegate ItemRemoved;
        event ObservableListItemsChangedDelegate ItemsChanged;
    }

}
