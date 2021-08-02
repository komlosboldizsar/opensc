using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    interface IDatabaseBase: IObservableList
    {
        void Load();
        void Save();
        void BuildRelationsByForeignKeys();
        object GetById(int id);
        void NotifyItemsRestoredOwnFields();
        void NotifyItemsRestoredBasicRelations();
        void RequestRestoreCustomRelations();
        void NotifyItemsTotallyRestored();
    }
}
