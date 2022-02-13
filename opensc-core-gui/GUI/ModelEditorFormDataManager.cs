using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI
{

    public interface IModelEditorFormDataManager
    {
        IModel CreateModel();
        void DeleteFromDatabase();
        void AddToDatabase();
        int GetNextValidId();
    }

    public class ModelEditorFormDataManager<TModelBasetype, TModelSubtype> : IModelEditorFormDataManager
        where TModelBasetype : class, IModel
        where TModelSubtype : TModelBasetype, new()
    {

        private IModelEditorForm<TModelBasetype> form;
        private DatabaseBase<TModelBasetype> database;

        public ModelEditorFormDataManager(IModelEditorForm<TModelBasetype> form, DatabaseBase<TModelBasetype> database)
        {
            this.form = form;
            this.database = database;
        }

        public virtual IModel CreateModel() => new TModelSubtype();
        public void DeleteFromDatabase() => database.Remove(form.EditedModel as TModelBasetype);
        public void AddToDatabase() => database.Add(form.EditedModel as TModelBasetype);
        public int GetNextValidId() => database.NextValidId();

    }

}
