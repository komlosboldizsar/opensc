using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    public class MasterDatabase
    {
        public static MasterDatabase Instance { get; } = new MasterDatabase();

        private Dictionary<string, IDatabaseBase> databases = new Dictionary<string, IDatabaseBase>();

        public void RegisterSingletonDatabase(Type databaseClass)
        {

            var instanceObj = databaseClass.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null);
            var instance = instanceObj as IDatabaseBase;
            if (instance == null)
                throw new Exception();

            // https://stackoverflow.com/a/2656211
            var nameAttribute = databaseClass.GetCustomAttributes(typeof(DatabaseNameAttribute), true).FirstOrDefault() as DatabaseNameAttribute;
            if (nameAttribute == null)
                return;
            string name = nameAttribute.Name;
            if (databases.ContainsKey(name))
                throw new Exception();

            databases.Add(name, instance);
         
        }

        public void RegisterSingletonDatabase(IDatabaseBase database, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException();
            if (databases.ContainsKey(key))
                throw new Exception();
            databases.Add(key, database);
        }

        public void LoadEverything()
        {
            foreach (var database in databases.Values)
                database.Load();
            foreach (var database in databases.Values)
                database.NotifyItemsRestoredOwnFields();
            foreach (var database in databases.Values)
                database.BuildRelationsByForeignKeys();
            foreach (var database in databases.Values)
                database.NotifyItemsRestoredBasicRelations();
            foreach (var database in databases.Values)
                database.RequestRestoreCustomRelations();
            foreach (var database in databases.Values)
                database.NotifyItemsTotallyRestored();
        }

        public void SaveEverything()
        {
            foreach (var database in databases.Values)
                database.Save();
        }

        public DatabaseFile GetFileToWrite(IDatabaseBase database)
        {
            string key = getKeyForDatabase(database);
            return new DatabaseFile(key + ".db", DatabaseFile.DatabaseFileMode.Write);
        }

        public DatabaseFile GetFileToRead(IDatabaseBase database)
        {
            string key = getKeyForDatabase(database);
            return new DatabaseFile(key + ".db", DatabaseFile.DatabaseFileMode.Read);
        }

        private string getKeyForDatabase(IDatabaseBase database)
        {
            if (!databases.ContainsValue(database))
                throw new Exception();
            return databases.FirstOrDefault(d => (d.Value == database)).Key;
        }

        public void DatabaseFileWritten(DatabaseFile file)
        {

        }

        public object GetItem(string databaseName, int id)
        {
            if (!databases.TryGetValue(databaseName, out IDatabaseBase database))
                return null;
            return database.GetById(id);
        }

    }
}
