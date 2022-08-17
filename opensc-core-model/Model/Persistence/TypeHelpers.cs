using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    internal static class TypeHelpers
    {

        public static bool IsAssociationType(this Type type)
        {
            if (type.IsArray)
                return type.GetElementType().IsAssociationType();
            CollectionDetails collectionDetails = type.GetCollectionDetails();
            if ((collectionDetails.KeyType != null) && collectionDetails.KeyType.IsAssociationType())
                return true;
            if ((collectionDetails.ElementType != null) && collectionDetails.ElementType.IsAssociationType())
                return true;
            return ((type == typeof(ISystemObject)) || type.GetInterfaces().Any(iface => (iface == typeof(ISystemObject))));
        }

        public static bool IsKeyValuePair(this Type type, out Type keyType, out Type valueType)
        {
            if (!type.IsGenericType || (type.GetGenericTypeDefinition() != typeof(KeyValuePair<,>)))
            {
                keyType = null;
                valueType = null;
                return false;
            }
            Type[] genericArguments = type.GetGenericArguments();
            keyType = genericArguments[0].IsAssociationType() ? typeof(string) : genericArguments[0];
            valueType = genericArguments[1].IsAssociationType() ? typeof(string) : genericArguments[1];
            return true;
        }

        public static Type MakeKeyValuePairType(Type keyType, Type valueType)
            => typeof(KeyValuePair<,>).GetGenericTypeDefinition().MakeGenericType(new Type[] { keyType, valueType });


    }
}
