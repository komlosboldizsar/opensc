using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    internal class ForeignKeyCollection<T>
    {

        private readonly Dictionary<T, Dictionary<string, object>> foreignKeys = new();

        public Dictionary<string, object> GetCollectionForItem(T item)
        {
            if (foreignKeys.TryGetValue(item, out Dictionary<string, object> foreignKeyCollection))
                return foreignKeyCollection;
            Dictionary<string, object> newForeignKeyCollection = new();
            foreignKeys.Add(item, newForeignKeyCollection);
            return newForeignKeyCollection;
        }

    }
}
