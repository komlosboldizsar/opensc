using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model;
using OpenSC.Model.General;
using System.Collections.Generic;

namespace OpenSC.GUI
{

    public interface IModelListFormBaseManager : IItemListFormBaseManager
    {
        void DeleteItem(IModel item);
    }

    public class ModelListFormBaseManager<TModel> : ItemListFormBaseManager<TModel>, IModelListFormBaseManager
        where TModel : class, IModel, INotifyPropertyChanged
    {

        public ModelListFormBaseManager(ModelListFormBase form, DatabaseBase<TModel> boundCollection, ColumnDescriptorCreatorDelegate baseColumnCreatorMethod)
            : base(form, boundCollection, baseColumnCreatorMethod)
        { }

        public void DeleteItem(IModel item)
        {
            TModel itemCasted = item as TModel;
            if (itemCasted == null)
                return;
            ((DatabaseBase<TModel>)boundCollection).Remove(itemCasted);
        }

    }

}
