﻿using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI
{

    public class ModelEditorFormTypeRegister<TModelBasetype> : IModelEditorFormTypeRegister
        where TModelBasetype : class, IModel
    {

        private Dictionary<Type, IModelEditorForm<TModelBasetype>> registeredTypes = new Dictionary<Type, IModelEditorForm<TModelBasetype>>();

        public List<Type> RegisteredTypes => registeredTypes.Keys.ToList();

        public void RegisterFormType<TModelSubtype, TForm>()
            where TModelSubtype : TModelBasetype
            where TForm: IModelEditorForm<TModelBasetype>, new()
        {
            registeredTypes.Add(typeof(TModelSubtype), new TForm());
        }

        public IModelEditorForm GetFormForModel(IModel modelInstance)
            => GetFormForType(modelInstance.GetType(), modelInstance);

        public IModelEditorForm GetFormForType(Type type, IModel modelInstance = null)
        {
            if (!registeredTypes.TryGetValue(type, out IModelEditorForm<TModelBasetype> foundForm))
                return null;
            return foundForm.GetInstanceT(modelInstance as TModelBasetype);
        }

    }

}
