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

        private Dictionary<string, IBoolean> registeredBooleans;

        public IBoolean this[string id]
        {
            get
            {
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
            ItemAdded?.Invoke();
            ItemsChanged?.Invoke();

        }

        public void UnregisterBoolean(IBoolean boolean)
        {
            UnregisterBoolean(boolean.Name);
        }

        public void UnregisterBoolean(string name)
        {
            if (!registeredBooleans.ContainsKey(name))
                return;
            registeredBooleans.Remove(name);
            ItemRemoved?.Invoke();
            ItemsChanged?.Invoke();
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
