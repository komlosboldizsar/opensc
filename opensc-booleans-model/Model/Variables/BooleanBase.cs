using OpenSC.Model.General;
using System.Drawing;

namespace OpenSC.Model.Variables
{

    public class BooleanBase : SystemObjectBase, IBoolean
    {

        public BooleanBase()
        { }

        public BooleanBase(string identifier, Color color, string description = "")
        {
            this.identifier = identifier;
            this.color = color;
            this.description = description;
        }

        #region Property: Identifier
        public event PropertyChangedTwoValuesDelegate<IBoolean, string> IdentifierChanged;

        private string identifier;

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
        #endregion

        #region Property: Color
        public event PropertyChangedTwoValuesDelegate<IBoolean, Color> ColorChanged;

        private Color color;

        public Color Color
        {
            get => color;
            set => this.setProperty(ref color, value, ColorChanged);
        }
        #endregion

        #region Property: Description
        public event PropertyChangedTwoValuesDelegate<IBoolean, string> DescriptionChanged;

        private string description;

        public string Description
        {
            get => description;
            set => this.setProperty(ref description, value, DescriptionChanged);
        }
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

    }

}
