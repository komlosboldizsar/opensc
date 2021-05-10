using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{

    public delegate void ModelRestoredHandler(IModel model);
    public delegate void ModelRemovedHandler(IModel model);

    public interface IModel
    {
        int ID { get; set; }
        void Restored();
        void Removed();
        void StartUpdate();
        void EndUpdate();
        event ModelRestoredHandler ModelRestored;
        event ModelRemovedHandler ModelRemoved;
    }

}
