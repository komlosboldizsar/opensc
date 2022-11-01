using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    internal class DatabasePersisterConstants
    {
        public const string ATTRIBUTE_ID = "id";
        public const string ATTRIBUTE_TYPE = "type";
        public const string UNDEFINED_ARRAY_ITEM_TAG = "arrayitem";
        public const string UNDEFINED_DICTIONARY_ITEM_KEY = "key";
        public const BindingFlags MEMBER_LOOKUP_BINDING_FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        public const string HETEROGENOUS_COLLECTION_TYPE = "type";
    }
}
