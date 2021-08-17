using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{

    public class ObservableProxyList<TProxy, TOriginal> : IObservableList<TProxy>
    {

        protected ObservableList<TOriginal> originalList;

        protected List<TProxy> proxyList = new List<TProxy>();

        public delegate TProxy OriginalToProxyConverterMethodDelegate(TOriginal original);
        protected OriginalToProxyConverterMethodDelegate converterMethod;
        public ObservableProxyList(ObservableList<TOriginal> originalList, OriginalToProxyConverterMethodDelegate converterMethod)
        {
            this.originalList = originalList;
            this.converterMethod = converterMethod;
            rebuildProxies();
            originalList.ItemAdded += OriginalList_ItemAdded;
            originalList.ItemRemoved += OriginalList_ItemRemoved;
            originalList.ItemsChanged += OriginalList_ItemsChanged;
        }

        private void OriginalList_ItemAdded(IEnumerable addedItems)
        {
            rebuildProxies();
            ItemAdded?.Invoke(addedItems);
        }

        private void OriginalList_ItemRemoved(IEnumerable removedItems)
        {
            rebuildProxies();
            ItemRemoved?.Invoke(removedItems);
        }

        private void OriginalList_ItemsChanged()
        {
            rebuildProxies();
        }

        private void rebuildProxies()
        {
            proxyList.Clear();
            foreach (TOriginal original in originalList)
                proxyList.Add(converterMethod(original));
            ItemsChanged?.Invoke();
        }

        public event ObservableListItemAddedDelegate ItemAdded;
        public event ObservableListItemRemovedDelegate ItemRemoved;
        public event ObservableListItemsChangedDelegate ItemsChanged;

        public IEnumerator<TProxy> GetEnumerator()
        {
            return proxyList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return proxyList.GetEnumerator();
        }

        public int Count => proxyList.Count;

        public TProxy this[int index] => proxyList[index];

    }

}
