using OpenSC.Model.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Persistence
{
    public class DatabasePersister<T>
        where T : class, IModel
    {

        private readonly DatabaseBase<T> database;

        private readonly bool isPolymorph = false;
        private readonly IModelTypeRegister typeRegister;
        private readonly string rootTag = "root";
        private readonly string itemTag = "item";

        private readonly DatabaseSerializer<T> serializer;
        private readonly DatabaseDeserializer<T> deserializer;
        private readonly DatabaseRelationBuilder<T> relationBuilder;

        private ForeignKeyCollection<T> lastDeserializationForeignKeyCollection;

        public DatabasePersister(DatabaseBase<T> database)
        {
            this.database = database;
            PolymorphDatabaseAttribute polymorphAttr = database.GetType().GetCustomAttribute<PolymorphDatabaseAttribute>();
            if (polymorphAttr != null)
            {
                isPolymorph = true;
                typeRegister = polymorphAttr.TypeRegister;
            }
            XmlTagNamesAttribute tagNameAttr = database.GetType().GetCustomAttribute<XmlTagNamesAttribute>();
            if (tagNameAttr != null)
            {
                rootTag = tagNameAttr.RootTag;
                itemTag = tagNameAttr.ItemTag;
            }
            serializer = new(isPolymorph, typeRegister, rootTag, itemTag);
            deserializer = new(isPolymorph, typeRegister, rootTag, itemTag);
            relationBuilder = new(isPolymorph, typeRegister, rootTag, itemTag);
        }

        public void Save()
        {
            using DatabaseFile fileToWrite = MasterDatabase.Instance.GetFileToWrite(database);
            serializer.Save(database, fileToWrite);
        }

        public Dictionary<int, T> Load()
        {
            using DatabaseFile fileToRead = MasterDatabase.Instance.GetFileToRead(database);
            DatabaseDeserializer<T>.Result result = deserializer.Load(fileToRead);
            lastDeserializationForeignKeyCollection = result.ForeignKeys;
            return result.Items;
        }

        public void BuildRelationsByForeignKeys()
            => relationBuilder.BuildRelationsByForeignKeys(database, lastDeserializationForeignKeyCollection);

    }
}
