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

        public virtual void Removed()
        { }

        public virtual void Restored()
        { }

        private int updateCounter = 0;

        protected bool Updating { get; private set; }

        public void StartUpdate()
        {
            updateCounter++;
            Updating = true;
            beforeUpdate();
        }

        protected virtual void beforeUpdate()
        { }

        public void EndUpdate()
        {
            updateCounter--;
            if (updateCounter < 0)
                updateCounter = 0;
            Updating = !(updateCounter == 0);
            afterUpdate();
        }

        protected virtual void afterUpdate()
        { }

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedDelegate PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(propertyName);
        }
        #endregion

    }

}
