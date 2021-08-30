using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI
{

    public interface IModelEditorForm<TModel>
        where TModel : class, IModel
    {
        IModelEditorForm<TModel> GetInstance(TModel modelInstance);
    }

}
