using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    public class MacroCommandArgumentDatabaseSubTypeItem<TModelDatabase, TModelSubType> : MacroCommandArgumentDatabaseItem<TModelDatabase>
        where TModelDatabase : class, IModel
        where TModelSubType : class, TModelDatabase
    {

        public MacroCommandArgumentDatabaseSubTypeItem(DatabaseBase<TModelDatabase> database) : base(database)
        {
            ObjectType = typeof(TModelSubType);
        }

        public override object GetObjectByKey(string key, object[] previousArgumentObjects) => base.GetObjectByKey(key, previousArgumentObjects) as TModelSubType;
        public override string GetKeyByObject(object obj) => (obj as TModelSubType)?.ID.ToString() ?? "0";
        public override IEnumerable<object> GetPossibilities(object[] previousArgumentValues) => GetPossibilities(previousArgumentValues).OfType<TModelSubType>();

    }
}
