using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI
{

    public interface IModelEditorForm<TModel>
    {
        IModelEditorForm<TModel> GetInstance(TModel modelInstance);
    }

}
