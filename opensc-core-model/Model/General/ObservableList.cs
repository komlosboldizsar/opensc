using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.General
{

    public class ObservableList<T> : List<T>, IObservableList<T>
    {

        public ObservableList()
        { }

        public ObservableList(int capacity) : base(capacity)
        { }

        public ObservableList(IEnumerable<T> collection) : base(collection)
        { }

        public event ObservableListItemAddedDelegate ItemAdded;
        public event ObservableListItemRemovedDelegate ItemRemoved;
        public event ObservableListItemsChangedDelegate ItemsChanged;

        public new void Add(T item)
        {
            base.Add(item);
            ItemAdded?.Invoke(new object[] { item });
            ItemsChanged?.Invoke();
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);
            if (collection.Count() > 0)
            {
                ItemAdded?.Invoke(collection);
                ItemsChanged?.Invoke();
            }
        }

        public new void Clear()
        {
            int count = Count;
            IEnumerable removedItems = new List<T>(this);
            base.Clear();
            if (count > 0)
            { 
                ItemRemoved?.Invoke(removedItems);
                ItemsChanged?.Invoke();
            }
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            ItemAdded?.Invoke(new object[] { item });
            ItemsChanged?.Invoke();
        }

        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);
            if (collection.Count() > 0) {
                ItemAdded?.Invoke(collection);
                ItemsChanged?.Invoke();
            }
        }

        public new bool Remove(T item)
        {
            if (base.Remove(item))
            {
                ItemRemoved?.Invoke(new object[] { item });
                ItemsChanged?.Invoke();
                return true;
            }
            return false;
        }

        public new int RemoveAll(Predicate<T> match)
        {
            IEnumerable removedItems = new List<T>(this.Where(i => match(i)));
            int removedCount = base.RemoveAll(match);
            ItemRemoved?.Invoke(removedItems);
            ItemsChanged?.Invoke();
            return removedCount;
        }

        public new void RemoveAt(int index)
        {
            T removedItem = this[index];
            base.RemoveAt(index);
            ItemRemoved?.Invoke(new object[] { removedItem });
            ItemsChanged?.Invoke();
        }

        public new void RemoveRange(int index, int count)
        {
            IEnumerable removedItems = new List<T>(this.GetRange(index, count));
            base.RemoveRange(index, count);
            ItemRemoved?.Invoke(removedItems);
            ItemsChanged?.Invoke();
        }

        public new void Reverse(int index, int count)
        {
            base.Reverse(index, count);
            ItemsChanged?.Invoke();
        }

        public new void Reverse()
        {
            base.Reverse();
            ItemsChanged?.Invoke();
        }

        public new void Sort(int index, int count, IComparer<T> comparer)
        {
            base.Sort(index, count, comparer);
            ItemsChanged?.Invoke();
        }

        public new void Sort(Comparison<T> comparison)
        {
            base.Sort(comparison);
            ItemsChanged?.Invoke();
        }

        public new void Sort()
        {
            base.Sort();
            ItemsChanged?.Invoke();
        }

        public new void Sort(IComparer<T> comparer)
        {
            base.Sort(comparer);
            ItemsChanged?.Invoke();
        }

        public new void TrimExcess()
            => base.TrimExcess();

    }

}
