using OpenSC.Model;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.McCurdy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI
{

    public class ModelEditorFormTypeRegister<TModelBase>
        where TModelBase : IModel
    {

        private Dictionary<Type, IModelEditorForm<TModelBase>> registeredTypes = new Dictionary<Type, IModelEditorForm<TModelBase>>();

        public IReadOnlyList<Type> RegisteredTypes
        {
            get { return registeredTypes.Keys.ToList(); }
        }

        public void RegisterFormType<TModel, TForm>()
            where TModel: TModelBase
            where TForm: IModelEditorForm<TModelBase>, new()
        {
            registeredTypes.Add(typeof(TModel), new TForm());
        }

        public IModelEditorForm<TModelBase> GetFormForModel(TModelBase modelInstance)
        {
            Type modelType = modelInstance.GetType();
            if (!registeredTypes.TryGetValue(modelType, out IModelEditorForm<TModelBase> foundForm))
                return null;
            return foundForm.GetInstance(modelInstance);
        }

        public IModelEditorForm<TModelBase> GetFormForType(Type type)
        {
            if (!registeredTypes.TryGetValue(type, out IModelEditorForm<TModelBase> foundForm))
                return null;
            return foundForm.GetInstance(default(TModelBase));
        }

    }

}
