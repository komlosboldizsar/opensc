using OpenSC.Model.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{
    class BooleanRegister : IObservableList<IBoolean>
    {

        #region Singleton
        public static BooleanRegister Instance { get; } = new BooleanRegister();

        private BooleanRegister()
        { }
        #endregion

        #region IObservableList<IBoolean> implementation
        public IBoolean this[int index] => registeredBooleans.Values.ToList()[index];

        public int Count => registeredBooleans.Count;

        public event ObservableListItemAddedDelegate ItemAdded;
        public event ObservableListItemRemovedDelegate ItemRemoved;
        public event ObservableListItemsChangedDelegate ItemsChanged;

        public IEnumerator<IBoolean> GetEnumerator()
        {
            return registeredBooleans.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return registeredBooleans.Values.GetEnumerator();
        }
        #endregion

        private Dictionary<string, IBoolean> registeredBooleans = new Dictionary<string, IBoolean>();

        public IBoolean this[string id]
        {
            get
            {
                if (string.IsNullOrEmpty(id))
                    return null;
                if (!registeredBooleans.TryGetValue(id, out IBoolean foundBoolean))
                    return null;
                return foundBoolean;
            }
        }

        public void RegisterBoolean(IBoolean boolean)
        {

            string name = boolean.Name;
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
            if (registeredBooleans.ContainsKey(name))
                throw new NameIsAlreadyUsedException(name);

            registeredBooleans.Add(name, boolean);
            ItemAdded?.Invoke(new object[] { boolean });
            ItemsChanged?.Invoke();

        }

        public void UnregisterBoolean(IBoolean boolean)
        {
            UnregisterBoolean(boolean.Name);
        }

        public void UnregisterBoolean(string name)
        {
            if (!registeredBooleans.TryGetValue(name, out IBoolean removedBoolean))
                return;
            registeredBooleans.Remove(name);
            ItemRemoved?.Invoke(new object[] { removedBoolean });
            ItemsChanged?.Invoke();
        }

        public void BooleanNameChanged(IBoolean boolean)
        {
            if (!registeredBooleans.ContainsValue(boolean))
                return;
            if(!CanNameUsedForBoolean(boolean, boolean.Name))
                throw new NameIsAlreadyUsedException(boolean.Name);
            string previousRegisteredName = registeredBooleans.FirstOrDefault(kvp => (kvp.Value == boolean)).Key;
            registeredBooleans.Remove(previousRegisteredName);
            registeredBooleans.Add(boolean.Name, boolean);
        }

        public bool CanNameUsedForBoolean(IBoolean boolean, string name)
        {
            if (!registeredBooleans.TryGetValue(name, out IBoolean foundBoolean))
                return true;
            return (foundBoolean == boolean);
        }

        public class NameIsAlreadyUsedException : Exception
        {

            public NameIsAlreadyUsedException()
            {}

            public NameIsAlreadyUsedException(string message) : base(message)
            { }

            public NameIsAlreadyUsedException(string message, Exception innerException) : base(message, innerException)
            { }

        }

    }
}
