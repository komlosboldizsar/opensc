using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public abstract class CustomBoolean : ModelBase, IBoolean
    {

        public const string LOG_TAG = "CustomBoolean";

        #region Instantiation, restoration, persistence, removation
        public CustomBoolean()
        {
            setBaseFieldsDefaults();
        }

        public override void Removed()
        {
            base.Removed();
            IdentifierChanged = null;
            ColorChanged = null;
            DescriptionChanged = null;
            StateChanged = null;
            unregister();
        }

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            register();
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            checkBaseFields();
        }
        #endregion

        #region Before & after update
        protected override void afterUpdate()
        {
            base.afterUpdate();
            checkBaseFields();
            if (!Registered)
                register();
            CustomBooleanDatabase.Instance.ItemUpdated(this);
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = CustomBooleanDatabase.Instance;
        #endregion

        #region Property: Identifier
        public event PropertyChangedTwoValuesDelegate<IBoolean, string> IdentifierChanged;

        private string identifier;

        [PersistAs("identifier")]
        public string Identifier
        {
            get => identifier;
            set
            {
                if (!BooleanRegister.Instance.CanKeyBeUsedForItem(this, value, out IBoolean identifierOwnerItem))
                    return;
                if (!this.setProperty(ref identifier, value, IdentifierChanged))
                    return;
            }
        }

        public abstract bool IdentifierUserEditable { get; }
        public abstract string GetIdentifierByData(CustomBooleanDataStore dataStore);
        #endregion

        #region Property: Color
        public event PropertyChangedTwoValuesDelegate<IBoolean, Color> ColorChanged;

        private Color color;

        [PersistAs("color")]
        public Color Color
        {
            get => color;
            set => this.setProperty(ref color, value, ColorChanged);
        }

        public abstract bool ColorUserEditable { get; }
        public abstract Color GetColorByData(CustomBooleanDataStore dataStore);
        #endregion

        #region Property: Description
        public event PropertyChangedTwoValuesDelegate<IBoolean, string> DescriptionChanged;

        private string description;

        [PersistAs("description")]
        public string Description
        {
            get => description;
            set => this.setProperty(ref description, value, DescriptionChanged);
        }

        public abstract bool DescriptionUserEditable { get; }
        public abstract string GetDescriptionByData(CustomBooleanDataStore dataStore);
        #endregion

        #region Property: CurrentState
        public event PropertyChangedTwoValuesDelegate<IBoolean, bool> StateChanged;

        private bool currentState;

        public bool CurrentState
        {
            get => currentState;
            protected set => this.setProperty(ref currentState, value, StateChanged);
        }
        #endregion

        #region Base fields
        private void setBaseFieldsDefaults()
        {
            CustomBooleanDataStore dataStore = getDataStore();
            Identifier = GetIdentifierByData(dataStore);
            Color = GetColorByData(dataStore);
            Description = GetDescriptionByData(dataStore);
        }

        private void checkBaseFields()
        {
            CustomBooleanDataStore dataStore = getDataStore();
            if (!IdentifierUserEditable)
                Identifier = GetIdentifierByData(dataStore);
            if (!ColorUserEditable)
                Color = GetColorByData(dataStore);
            if (!DescriptionUserEditable)
                Description = GetDescriptionByData(dataStore);
        }
        #endregion

        #region Data store
        protected virtual CustomBooleanDataStore getDataStore(CustomBooleanDataStore dataStore = null)
        {
            if (dataStore == null)
                dataStore = new CustomBooleanDataStore();
            dataStore.ID = ID;
            dataStore.Name = Name;
            return dataStore;
        }
        #endregion

        #region Registration
        protected void register()
        {
            if (Registered)
                return;
            BooleanRegister.Instance.Register(this);
            Registered = true;
        }

        protected void unregister()
        {
            if (!Registered)
                return;
            BooleanRegister.Instance.Unregister(this);
            Registered = false;
        }

        protected bool Registered { get; private set; }
        #endregion

    }

}
