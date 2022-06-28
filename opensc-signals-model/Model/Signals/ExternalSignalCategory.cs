using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public partial class ExternalSignalCategory : ModelBase
    {

        #region Property: ID
        protected override void validateIdForDatabase(int id)
        {
            if (!ExternalSignalDatabases.Categories.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = ExternalSignalDatabases.Categories;
        #endregion

        #region Property: Color
        [AutoProperty]
        [PersistAs("color")]
        private Color color;
        #endregion

    }

}
