using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    internal record CollectionDetails(Type Type, bool IsDictionary, bool IsCollection, Type KeyType, Type ElementType, bool IsComponentCollection, Type ComponentOwnerType)
    {
        public Type[] AsTypeArray => IsDictionary ? new Type[] { KeyType, ElementType } : new Type[] { ElementType };
    }
}
