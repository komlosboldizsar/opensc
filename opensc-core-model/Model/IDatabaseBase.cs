using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    public interface IDatabaseBase
    {
        string Name { get; }
        void Load();
        void Save();
        void BuildRelationsByForeignKeys();
        object GetById(int id);
        bool CanIdBeUsedForItem(int id, object forItem);
        void NotifyItemsRestoredOwnFields();
        void NotifyItemsRestoredBasicRelations();
        void RequestRestoreCustomRelations();
        void NotifyItemsTotallyRestored();
    }
}
