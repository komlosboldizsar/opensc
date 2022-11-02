using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Salvos
{

    public class Salvo : ModelBase
    {

        public const string LOG_TAG = "Salvo";

        #region Persistence, instantiation
        public Salvo()
        { }

        public override void Removed()
        { }
        #endregion

        #region Restoration
        public override void TotallyRestored()
        {
            base.TotallyRestored();
            restoreCrosspointsAssociations();
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = SalvoDatabase.Instance;
        #endregion

        #region Crosspoints
        public event PropertyChangedTwoValuesDelegate<Salvo, List<SalvoCrosspoint>> CrosspointsChanged;

        [PersistAs("crosspoints")]
        [PersistAs("crosspoint", 1)]
        private ObservableList<SalvoCrosspoint> crosspoints = new ObservableList<SalvoCrosspoint>();
        public ObservableList<SalvoCrosspoint> Crosspoints => crosspoints;

        private void restoreCrosspointsAssociations()
        {
            foreach (SalvoCrosspoint crosspoint in crosspoints)
                crosspoint.RestoreAssociations();
        }

        public void AddCrosspoint(RouterOutput output = null, RouterInput input = null) => crosspoints.Add(new SalvoCrosspoint(output, input));
        public void RemoveCrosspoint(SalvoCrosspoint crosspoint) => crosspoints.Remove(crosspoint);
        #endregion

        public void Take()
        {
            IEnumerable<Router> routers = crosspoints.Select(cp => cp.Router);
            foreach (Router router in routers)
            {
                IEnumerable<SalvoCrosspoint> validCrosspoints = crosspoints.Where(cp => ((cp.Router == router) && (cp.Output != null) && (cp.Input != null)));
                IEnumerable<RouterCrosspoint> routerCrosspoints = validCrosspoints.Select(cp => new RouterCrosspoint(cp.Output, cp.Input));
                router.RequestCrosspointUpdates(routerCrosspoints);
            }
        }
        
        public void StoreCurrentCrosspoints()
        {
            foreach (SalvoCrosspoint crosspoint in crosspoints)
                crosspoint.StoreCurrentInput();
        }

    }

}
