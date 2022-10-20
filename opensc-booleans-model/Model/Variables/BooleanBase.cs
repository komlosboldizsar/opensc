using OpenSC.Model.General;
using OpenSC.Model.SourceGenerators;
using System.Drawing;

namespace OpenSC.Model.Variables
{

    public partial class BooleanBase : SystemObjectBase, IBoolean
    {

        public BooleanBase()
        { }

        public BooleanBase(string identifier, Color color, string description = "")
        {
            this.identifier = identifier;
            this.color = color;
            this.description = description;
        }

        #region Properties from IBoolean
        [AutoProperty]
        [AutoProperty.Event(typeof(IBoolean))]
        private string identifier;

        [AutoProperty]
        [AutoProperty.Event(typeof(IBoolean))]
        private Color color;

        [AutoProperty]
        [AutoProperty.Event(typeof(IBoolean))]
        private string description;

        [AutoProperty]
        [AutoProperty.Event(SenderType = typeof(IBoolean), EventName = nameof(IBoolean.StateChanged))]
        private bool currentState;
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
