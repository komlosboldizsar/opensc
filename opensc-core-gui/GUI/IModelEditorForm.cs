using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI
{

    public interface IModelEditorForm
    {
        IModelEditorForm GetInstance(object modelInstance);
    }

    public interface IModelEditorForm<TModel> : IModelEditorForm
        where TModel : class, IModel
    {
        IModelEditorForm<TModel> GetInstanceT(TModel modelInstance);
    }

}
