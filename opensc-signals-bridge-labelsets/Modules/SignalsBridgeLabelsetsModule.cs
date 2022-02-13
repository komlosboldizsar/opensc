using OpenSC.Model;
using OpenSC.Model.General;
using OpenSC.Model.Routers;
using OpenSC.Model.Labelsets;
using OpenSC.Model.Variables;
using OpenSC.Model.Signals;

namespace OpenSC.Modules
{

    [Module("signals-bridge-labelsets", "Signals (bridge to labelsets)", "TODO")]
    [DependsOnModule(typeof(SignalsModelModule))]
    public class SignalsBridgeLabelsetsModule : LabelsetsBridgeModuleBase<SignalsModelModule>
    {

        protected override void startRegisteringLabelableObjects()
        {
            SignalRegister.Instance.ItemsAdded += signalsAdded;
            SignalRegister.Instance.ItemsRemoved += signalsRemoved;
        }

        private void signalsAdded(System.Collections.Generic.IEnumerable<IObservableEnumerable<ISignalSourceRegistered>.ItemWithPosition> affectedItemsWithPositions)
            => affectedItemsWithPositions.Foreach(aiwp => LabelableObjectRegister.Instance.Register(aiwp.Item));

        private void signalsRemoved(System.Collections.Generic.IEnumerable<IObservableEnumerable<ISignalSourceRegistered>.ItemWithPosition> affectedItemsWithPositions)
            => affectedItemsWithPositions.Foreach(aiwp => LabelableObjectRegister.Instance.Unregister(aiwp.Item));

    }

}
