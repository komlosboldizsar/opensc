using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{

    public abstract class ModelBase : SystemObjectBase, IModel
    {

        #region Property: ID
        public event PropertyChangedTwoValuesDelegate<IModel, int> IdChanged;

        private int id;
        public int ID
        {
            get => id;
            set => setProperty(this, ref id, value, IdChanged, null, (ov, nv) => afterIdChange(), ValidateId);
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            validateIdForDatabase(id);
            validateIdExtended(id);
        }

        protected virtual void validateIdForDatabase(int id)
        {
            if (!OwnerDatabase.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        protected virtual void validateIdExtended(int id) { }

        protected virtual void afterIdChange() { }
        #endregion

        #region Property: Name
        public event PropertyChangedTwoValuesDelegate<IModel, string> NameChanged;

        [PersistAs("name")]
        protected string name;

        public string Name
        {
            get => name;
            set => setProperty(this, ref name, value, NameChanged, null, (ov, nv) => afterNameChange(), ValidateName);
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
            validateNameExtended(name);
        }

        protected virtual void validateNameExtended(string name)
        { }

        protected virtual void afterNameChange()
        { }
        #endregion

        #region Owner database
        public abstract IDatabaseBase OwnerDatabase { get; }
        #endregion

        public override string ToString() => $"(#{id}) {name}";

        public event ModelRemovedHandler ModelRemoved;

        public virtual void Removed()
        {
            IdChanged = null;
            ModelRemoved?.Invoke(this);
        }

        public event ModelRestoredHandler ModelRestoredOwnFields;

        public virtual void RestoredOwnFields() => ModelRestoredOwnFields?.Invoke(this);

        public virtual void RestoredBasicRelations() { }
        public virtual void RestoreCustomRelations() { }

        public event ModelRestoredHandler ModelTotallyRestored;
        public virtual void TotallyRestored() => ModelTotallyRestored?.Invoke(this);

        private int updateCounter = 0;
        protected bool Updating { get; private set; }

        public void StartUpdate()
        {
            updateCounter++;
            Updating = true;
            beforeUpdate();
            ModelBeforeUpdate?.Invoke(this);
        }

        protected virtual void beforeUpdate() { }
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

        protected virtual void afterUpdate() { }
        public event ModelAfterUpdateHandler ModelAfterUpdate;

    }

}
