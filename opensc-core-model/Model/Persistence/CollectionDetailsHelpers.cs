using System;
using System.Collections.Generic;

namespace OpenSC.Model.Persistence
{
    internal static class CollectionDetailsHelpers
    {
        public static CollectionDetails GetCollectionDetails(this Type type)
        {
            bool isDictionary = false;
            bool isCollection = false;
            Type keyType = null;
            Type elementType = null;
            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (interfaceType.IsGenericType)
                {
                    if (interfaceType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                    {
                        isDictionary = true;
                        isCollection = true;
                        keyType = interfaceType.GetGenericArguments()[0];
                        elementType = interfaceType.GetGenericArguments()[1];
                        break;
                    }
                    if (interfaceType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        isCollection = true;
                        elementType = interfaceType.GetGenericArguments()[0];
                        break;
                    }
                }
            }
            return new(type, isDictionary, isCollection, keyType, elementType);
        }
    }

}
