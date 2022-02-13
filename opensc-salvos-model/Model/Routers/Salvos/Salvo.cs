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
        [PersistAs(null, 1)]
        private ObservableList<SalvoCrosspoint> crosspoints = new ObservableList<SalvoCrosspoint>();
        public ObservableList<SalvoCrosspoint> Crosspoints => crosspoints;

        private void restoreCrosspointsAssociations()
        {
            foreach (SalvoCrosspoint crosspoint in crosspoints)
                crosspoint.RestoreAssociations();
        }

        public void AddCrosspoint() => crosspoints.Add(new SalvoCrosspoint());
        public void RemoveCrosspoint(SalvoCrosspoint crosspoint) => crosspoints.Remove(crosspoint);
        #endregion

        public void Take()
        {
            foreach (SalvoCrosspoint crosspoint in crosspoints)
                crosspoint.Take();
        }
        

    }

}
