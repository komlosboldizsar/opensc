using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    interface IDatabaseBase
    {
        void Load();
        void Save();
        void BuildRelationsByForeignKeys();
        object GetById(int id);
        void NotifyItemsRestored();
    }
}
