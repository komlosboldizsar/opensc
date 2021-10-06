using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI
{
    public interface IModelEditorFormTypeRegister
    {
        IModelEditorForm GetFormForModel(IModel modelInstance);
        IModelEditorForm GetFormForType(Type type, IModel modelInstance = null);
    }
}
