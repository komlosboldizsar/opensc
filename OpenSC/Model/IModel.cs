using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{

    public delegate void ModelRestoredHandler(IModel model);
    public delegate void ModelRemovedHandler(IModel model);
    public delegate void ModelBeforeUpdateHandler(IModel model);
    public delegate void ModelAfterUpdateHandler(IModel model);

    public interface IModel
    {
        int ID { get; set; }
        void RestoredOwnFields();
        void RestoredBasicRelations();
        void RestoreCustomRelations();
        void TotallyRestored();
        void Removed();
        void StartUpdate();
        void EndUpdate();
        event ModelRestoredHandler ModelRestoredOwnFields;
        event ModelRemovedHandler ModelRemoved;
        event ModelBeforeUpdateHandler ModelBeforeUpdate;
        event ModelAfterUpdateHandler ModelAfterUpdate;
    }

}
