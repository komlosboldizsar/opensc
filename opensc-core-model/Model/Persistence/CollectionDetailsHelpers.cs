using OpenSC.Model.General;
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
            bool isComponentCollection = false;
            Type componentOwnerType = null;
            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (interfaceType.IsGenericType)
                {
                    Type genericTypeDefinition = interfaceType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(IDictionary<,>))
                    {
                        isDictionary = true;
                        isCollection = true;
                        keyType = interfaceType.GetGenericArguments()[0];
                        elementType = interfaceType.GetGenericArguments()[1];
                    }
                    if (!isDictionary && (genericTypeDefinition == typeof(ICollection<>)))
                    {
                        isCollection = true;
                        elementType = interfaceType.GetGenericArguments()[0];
                    }
                    if (genericTypeDefinition == typeof(IComponentCollection<>))
                    {
                        isComponentCollection = true;
                        componentOwnerType = interfaceType.GetGenericArguments()[0];
                    }
                }
            }
            return new(type, isDictionary, isCollection, keyType, elementType, isComponentCollection, componentOwnerType);
        }
    }

}
