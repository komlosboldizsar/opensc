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
            if (!extendedMemberInfo.IsAssociationMember)
                return;
            if (!foreignKeysOfItem.TryGetValue(extendedMemberInfo.MemberInfo.Name, out object foreignKeys))
                return;
            if (foreignKeys == null)
                return;
            object foreignObjects = getAssociatedObjects(foreignKeys.GetType(), extendedMemberInfo.ValueType, foreignKeys);
            extendedMemberInfo.SetValue(item, foreignObjects);
        }

        private object getAssociatedObjects(Type memberType, Type originalType, object foreignKeys)
        {
            object result = null;
            if (tryGetAssociatedObjectsForArray(ref result, memberType, originalType, foreignKeys))
                return result;
            if (tryGetAssociatedObjectsForCollection(ref result, memberType, originalType, foreignKeys))
                return result;
            if (foreignKeys?.GetType() != typeof(string))
                return foreignKeys;
            if (foreignKeys is string foreignKeyString)
                return SystemObjectRegister.Instance[foreignKeyString];
            return null;
        }

        private bool tryGetAssociatedObjectsForArray(ref object result, Type memberType, Type originalType, object foreignKeys)
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
                    associatedObjects[i] = getAssociatedObjects(memberType.GetElementType(), originalType.GetElementType(), foreignKeysArray[i]);
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

        private bool tryGetAssociatedObjectsForCollection(ref object result, Type memberType, Type originalType, object foreignKeys)
        {

            CollectionDetails memberTypeCollectionDetails = memberType.GetCollectionDetails();
            CollectionDetails originalTypeCollectionDetails = originalType.GetCollectionDetails();
            if (!originalTypeCollectionDetails.IsCollection)
                return false;

            object associatedObjects = Activator.CreateInstance(originalType, Array.Empty<object>());
            MethodInfo addMethod = originalType.GetMethod(nameof(ICollection<object>.Add), originalTypeCollectionDetails.AsTypeArray);
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
                    associatedObject = getAssociatedObjects(memberTypeCollectionDetails.ElementType, originalTypeCollectionDetails.ElementType, foreignKeyValue);
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
