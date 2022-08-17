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
            => IsAssociationTypeComplex(type).TypeIs;

        public static AssociationTypeData IsAssociationTypeComplex(this Type type)
        {
            bool typeIs = false;
            bool? keyIs = null, elementIs = null;
            if (type.IsArray)
            {
                elementIs = type.GetElementType().IsAssociationType();
                return new((bool)elementIs, null, elementIs);
            }
            CollectionDetails collectionDetails = type.GetCollectionDetails();
            if (collectionDetails.KeyType != null) {
                keyIs = collectionDetails.KeyType.IsAssociationType();
                if (keyIs == true)
                    typeIs = true;
            }
            if (collectionDetails.ElementType != null)
            {
                elementIs = collectionDetails.ElementType.IsAssociationType();
                if (elementIs == true)
                    typeIs = true;
            }
            if ((type == typeof(ISystemObject)) || type.GetInterfaces().Any(iface => iface == typeof(ISystemObject)))
                typeIs = true;
            return new(typeIs, keyIs, elementIs);
        }

        public record AssociationTypeData(bool TypeIs, bool? KeyIs, bool? ElementIs);

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
