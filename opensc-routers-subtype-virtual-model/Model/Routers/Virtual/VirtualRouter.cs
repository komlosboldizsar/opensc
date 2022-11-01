using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Virtual
{

    [TypeLabel("Virtual router")]
    [TypeCode("virtual")]
    public class VirtualRouter : Router
    {

        public VirtualRouter()
        {
            State = RouterState.Unknown;
            StateString = "unknown";
        }

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            queryAllStates();
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            State = RouterState.Ok;
            StateString = "OK";
        }

        protected override void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input)
            => notifyCrosspointChanged(output, input);

        protected override void requestCrosspointUpdatesImpl(IEnumerable<RouterCrosspoint> crosspoints)
        {
            foreach (RouterCrosspoint crosspoint in crosspoints)
                notifyCrosspointChanged(crosspoint.Output, crosspoint.Input);
        }

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

        #region Inputs and outputs
        protected override RouterOutputCollection createOutputCollection()
            => new VirtualRouterOutputCollection(this);

        protected override Type getOutputCollectionType()
            => typeof(VirtualRouterOutputCollection);
        #endregion

    }

}
