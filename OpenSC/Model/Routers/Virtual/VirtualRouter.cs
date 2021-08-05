using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Virtual
{

    [TypeLabel("Virtual router")]
    [TypeCode("virtual")]
    class VirtualRouter : Router
    {

        public VirtualRouter()
        {
            State = RouterState.Ok;
            StateString = "OK";
        }

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            State = RouterState.Ok;
            StateString = "OK";
            queryAllStates();
        }

        protected override void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input)
            => notifyCrosspointChanged(output, input);

        protected override void requestLockOperationImpl(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType)
        {
            RouterOutputLockState newState = (lockOperationType == RouterOutputLockOperationType.Lock) ? RouterOutputLockState.Locked : RouterOutputLockState.Clear;
            notifyLockChanged(output, lockType, newState);
        }
        protected override void queryAllStates()
        {
            Random r = new Random();
            int c = Inputs.Count;
            foreach (RouterOutput output in Outputs)
            {
                notifyCrosspointChanged(output, (c > 0) ? Inputs[r.Next(c - 1)] : null);
                int lockRandom = r.Next(9);
                notifyLockChanged(output, RouterOutputLockType.Lock, (lockRandom == 7) ? RouterOutputLockState.Locked : RouterOutputLockState.Clear);
                notifyLockChanged(output, RouterOutputLockType.Lock, (lockRandom == 8) ? RouterOutputLockState.Locked : RouterOutputLockState.Clear);
            }
        }

        #region Input and output instantiation
        public override RouterInput CreateInput(string name, int index) => new RouterInput(name, this, index);
        public override RouterOutput CreateOutput(string name, int index) => new VirtualRouterOutput(name, this, index);

        private static readonly Dictionary<Type, string> OUTPUT_TYPES = new Dictionary<Type, string>()
        {
            {  typeof(VirtualRouterOutput), "virtual" }
        };

        protected override Dictionary<Type, string> OutputTypesDictionaryGetter() => OUTPUT_TYPES;
        #endregion

    }

}
