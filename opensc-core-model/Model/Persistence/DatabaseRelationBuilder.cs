using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    internal class DatabaseRelationBuilder<T>
        where T : class, IModel
    {

        private readonly bool isPolymorph;
        private readonly IModelTypeRegister typeRegister;
        private readonly string rootTag;
        private readonly string itemTag;

        public DatabaseRelationBuilder(bool isPolymorph, IModelTypeRegister typeRegister, string rootTag, string itemTag)
        {
            this.isPolymorph = isPolymorph;
            this.typeRegister = typeRegister;
            this.rootTag = rootTag;
            this.itemTag = itemTag;
        }

        private static readonly Type storedType = typeof(T);

        public void BuildRelationsByForeignKeys(IEnumerable<T> items, ForeignKeyCollection<T> foreignKeyCollection)
        {
            foreach (T item in items)
            {
                Type typeToRetrieve = isPolymorph ? item.GetType() : storedType;
                Dictionary<string, object> foreignKeysOfItem = foreignKeyCollection.GetCollectionForItem(item);
                foreach (ExtendedMemberInfo extendedMemberInfo in typeToRetrieve.GetExtendedMemberInfos())
                    buildRelationForField(item, extendedMemberInfo, foreignKeysOfItem);
                item.RestoreCustomRelations();
            }
        }

        private void buildRelationForField(T item, ExtendedMemberInfo extendedMemberInfo, Dictionary<string, object> foreignKeysOfItem)
        {
            if (!extendedMemberInfo.RequiresRelationBuilding)
                return;
            if (!foreignKeysOfItem.TryGetValue(extendedMemberInfo.MemberInfo.Name, out object foreignKeys))
                return;
            if (foreignKeys == null)
                return;
            object foreignObjects = getAssociatedObjects(extendedMemberInfo, foreignKeys.GetType(), extendedMemberInfo.ValueType, foreignKeys, item);
            extendedMemberInfo.SetValue(item, foreignObjects);
        }

        private object getAssociatedObjects(ExtendedMemberInfo extendedMemberInfo, Type memberType, Type originalType, object foreignKeys, object parentItem, int arrayDimension = 0)
        {
            object result = null;
            if (tryGetAssociatedObjectsForArray(ref result, extendedMemberInfo, memberType, originalType, foreignKeys, parentItem, arrayDimension))
                return result;
            if (tryGetAssociatedObjectsForCollection(ref result, extendedMemberInfo, memberType, originalType, foreignKeys, parentItem, arrayDimension))
                return result;
            if (foreignKeys?.GetType() != typeof(string))
                return foreignKeys;
            if (foreignKeys is string foreignKeyString)
                return SystemObjectRegister.Instance[foreignKeyString];
            return null;
        }

        private bool tryGetAssociatedObjectsForArray(ref object result, ExtendedMemberInfo extendedMemberInfo, Type memberType, Type originalType, object foreignKeys, object parentItem, int arrayDimension)
        {
            if (!memberType.IsArray)
                return false;
            if (memberType.GetElementType() != typeof(string))
            {
                if (foreignKeys is not object[] foreignKeysArray)
                {
                    result = null;
                    return true;
                }
                object[] associatedObjects = (object[])Activator.CreateInstance(originalType, new object[] { foreignKeysArray.Length });
                for (int i = 0; i < foreignKeysArray.Length; i++)
                    associatedObjects[i] = getAssociatedObjects(extendedMemberInfo, memberType.GetElementType(), originalType.GetElementType(), foreignKeysArray[i], parentItem, arrayDimension + 1);
                result = Convert.ChangeType(associatedObjects, originalType);
            }
            else
            {
                if (foreignKeys is not string[] foreignKeysArray)
                {
                    result = null;
                    return true;
                }
                object[] associatedObjects = (object[])Activator.CreateInstance(originalType, new object[] { foreignKeysArray.Length });
                for (int i = 0; i < foreignKeysArray.Length; i++)
                    if (foreignKeysArray[i] is string foreignKeyString)
                        associatedObjects[i] = SystemObjectRegister.Instance[foreignKeyString];
                result = associatedObjects;
            }
            return true;
        }

        private bool tryGetAssociatedObjectsForCollection(ref object result, ExtendedMemberInfo extendedMemberInfo, Type memberType, Type originalType, object foreignKeys, object parentItem, int arrayDimension)
        {

            CollectionDetails memberTypeCollectionDetails = memberType.GetCollectionDetails();
            CollectionDetails originalTypeCollectionDetails = originalType.GetCollectionDetails();
            if (!originalTypeCollectionDetails.IsCollection)
                return false;

            Type deserializeAsType = originalType;
            PersistSubclassAttribute persistSubclassAttribute = extendedMemberInfo.GetPersistSubclassAttributeForDimension(arrayDimension);
            if (persistSubclassAttribute != null) // should check if given type is subclass of member type
            {
                MethodInfo subclassTypeGetterMethodInfo = parentItem.GetType().GetMethod(persistSubclassAttribute.SubclassTypeGetterName, DatabasePersisterConstants.MEMBER_LOOKUP_BINDING_FLAGS);
                deserializeAsType = subclassTypeGetterMethodInfo.Invoke(parentItem, null) as Type;
            }

            Type[] constructorTypeArgs = Array.Empty<Type>();
            object[] constructorArgs = Array.Empty<object>();
            if (originalTypeCollectionDetails.IsComponentCollection)
            {
                constructorTypeArgs = new Type[] { originalTypeCollectionDetails.ComponentOwnerType };
                constructorArgs = new object[] { parentItem };
            }
            ConstructorInfo constructor = deserializeAsType.GetConstructor(constructorTypeArgs);
            object associatedObjects = constructor.Invoke(constructorArgs);
            MethodInfo addMethod = deserializeAsType.GetMethod(nameof(ICollection<object>.Add), originalTypeCollectionDetails.AsTypeArray);
            if (foreignKeys is not IEnumerable foreignKeysEnumerable)
            {
                result = null;
                return true;
            }

            foreach (object foreignKey in foreignKeysEnumerable)
            {

                object foreignKeyValue = foreignKey;
                object foreignKeyKey = null;
                if (originalTypeCollectionDetails.IsDictionary)
                {
                    Type foreignKeyType = foreignKey.GetType();
                    foreignKeyKey = foreignKeyType.GetProperty(nameof(KeyValuePair<object, object>.Key), BindingFlags.Public | BindingFlags.Instance).GetValue(foreignKey);
                    foreignKeyValue = foreignKeyType.GetProperty(nameof(KeyValuePair<object, object>.Value), BindingFlags.Public | BindingFlags.Instance).GetValue(foreignKey);
                }

                object associatedObject = null;
                if (memberTypeCollectionDetails.ElementType != typeof(string))
                {
                    associatedObject = getAssociatedObjects(extendedMemberInfo, memberTypeCollectionDetails.ElementType, originalTypeCollectionDetails.ElementType, foreignKeyValue, parentItem, arrayDimension + 1);
                }
                else
                {
                    string foreignKeyString = null;
                    foreignKeyString = foreignKeyValue as string;
                    if (foreignKeyString != null)
                        associatedObject = SystemObjectRegister.Instance[foreignKeyString];
                }

                if (originalTypeCollectionDetails.IsDictionary)
                {
                    if (foreignKeyKey != null)
                    {
                        if (originalTypeCollectionDetails.KeyType.IsAssignableTo(typeof(ISystemObject)) && (foreignKeyKey is string foreignKeyKeyString))
                            foreignKeyKey = SystemObjectRegister.Instance[foreignKeyKeyString];
                        if (foreignKeyKey != null)
                            addMethod.Invoke(associatedObjects, new object[] { foreignKeyKey, associatedObject });
                    }
                }
                else
                {
                    addMethod.Invoke(associatedObjects, new object[] { associatedObject });
                }

            }

            result = associatedObjects;
            return true;

        }

    }
}
