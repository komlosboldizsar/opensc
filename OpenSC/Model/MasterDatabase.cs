using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    class MasterDatabase
    {
        public static MasterDatabase Instance { get; } = new MasterDatabase();

        private Dictionary<string, IDatabaseBase> databases = new Dictionary<string, IDatabaseBase>();

        public void RegisterDatabase(Type databaseClass)
        {

            var instanceObj = databaseClass.GetProperty("Instace", BindingFlags.Public | BindingFlags.Static).GetValue(null);
            var instance = instanceObj as IDatabaseBase;
            if (instance == null)
                throw new Exception();

            // https://stackoverflow.com/a/2656211
            var nameAttribute = databaseClass.GetCustomAttributes(typeof(DatabaseNameAttribute), true).FirstOrDefault() as DatabaseNameAttribute;
            if (nameAttribute == null)
                return;
            string name = nameAttribute.Name;
            if (!databases.ContainsKey(name))
                throw new Exception();

            databases.Add(name, instance);
         
        }

    }
}
