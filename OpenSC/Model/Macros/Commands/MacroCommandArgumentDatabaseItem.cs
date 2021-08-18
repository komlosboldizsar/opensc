using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    public class MacroCommandArgumentDatabaseItem<TModel> : MacroCommandArgumentBase
        where TModel : class, IModel
    {

        private readonly DatabaseBase<TModel> database;

        public MacroCommandArgumentDatabaseItem(DatabaseBase<TModel> database) : base()
        {
            this.database = database;
        }

        public override object GetObjectByKey(string key, object[] previousArgumentObjects)
        {
            if (!int.TryParse(key, out int objectId))
                return null;
            return database.GetTById(objectId);
        }

        public override string GetKeyByObject(object obj) => (obj as TModel)?.ID.ToString() ?? "0";
        public override IEnumerable<object> GetPossibilities(object[] previousArgumentValues) => database;

    }
}
