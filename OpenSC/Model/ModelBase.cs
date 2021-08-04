using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{

    public abstract class ModelBase : IModel, INotifyPropertyChanged
    {
        public abstract int ID { get; set; }

        public event ModelRemovedHandler ModelRemoved;

        public virtual void Removed()
        {
            ModelRemoved?.Invoke(this);
        }

        public event ModelRestoredHandler ModelRestoredOwnFields;

        public virtual void RestoredOwnFields()
        {
            ModelRestoredOwnFields?.Invoke(this);
        }

        public virtual void RestoredBasicRelations()
        { }

        public virtual void RestoreCustomRelations()
        { }

        public event ModelRestoredHandler ModelTotallyRestored;

        public virtual void TotallyRestored()
        {
            ModelTotallyRestored?.Invoke(this);
        }

        private int updateCounter = 0;

        protected bool Updating { get; private set; }

        public void StartUpdate()
        {
            updateCounter++;
            Updating = true;
            beforeUpdate();
            ModelBeforeUpdate?.Invoke(this);
        }

        protected virtual void beforeUpdate()
        { }

        public event ModelBeforeUpdateHandler ModelBeforeUpdate;

        public void EndUpdate()
        {
            updateCounter--;
            if (updateCounter < 0)
                updateCounter = 0;
            Updating = !(updateCounter == 0);
            afterUpdate();
            ModelAfterUpdate?.Invoke(this);
        }

        protected virtual void afterUpdate()
        { }

        public event ModelAfterUpdateHandler ModelAfterUpdate;

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedDelegate PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(propertyName);
        }
        #endregion

    }

}
