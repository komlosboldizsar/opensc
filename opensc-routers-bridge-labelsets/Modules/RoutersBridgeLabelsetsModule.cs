using OpenSC.Model;
using OpenSC.Model.General;
using OpenSC.Model.Routers;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("routers-bridge-labelsets", "Routers (bridge to labelsets)", "TODO")]
    [DependsOnModule(typeof(RoutersModelModule))]
    public class RoutersBridgeLabelsetsModule : LabelsetsBridgeModuleBase<RoutersModelModule>
    {

        protected override void startRegisteringLabelableObjects()
        {
            RouterDatabase.Instance.Loaded += routerDatabaseLoaded;
            RouterDatabase.Instance.AddedItem += routerAdded;
            RouterDatabase.Instance.RemovedItem += routerRemoved;
        }

        private void routerDatabaseLoaded(DatabaseBase<Router> database)
        {
            RouterDatabase.Instance.Foreach(router => routerAdded(database, router));
        }

        private void routerAdded(DatabaseBase<Router> database, Router router)
        {
            router.Inputs.Foreach(input => LabelableObjectRegister.Instance.Register(input));
            router.Outputs.Foreach(output => LabelableObjectRegister.Instance.Register(output));
            router.Inputs.ItemsAdded += routerInputsAdded;
            router.Outputs.ItemsAdded += routerOutputsAdded;
        }

        private void routerRemoved(DatabaseBase<Router> database, Router router)
        {
            router.Inputs.ItemsAdded += routerInputsAdded;
            router.Outputs.ItemsAdded += routerOutputsAdded;
        }

        private void routerInputsAdded(System.Collections.Generic.IEnumerable<IObservableEnumerable<RouterInput>.ItemWithPosition> affectedItemsWithPositions)
            => affectedItemsWithPositions.Foreach(aiwp => LabelableObjectRegister.Instance.Register(aiwp.Item));

        private void routerOutputsAdded(System.Collections.Generic.IEnumerable<IObservableEnumerable<RouterOutput>.ItemWithPosition> affectedItemsWithPositions)
            => affectedItemsWithPositions.Foreach(aiwp => LabelableObjectRegister.Instance.Register(aiwp.Item));

    }

}
