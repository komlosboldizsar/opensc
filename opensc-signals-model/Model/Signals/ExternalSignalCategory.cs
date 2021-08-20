using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class ExternalSignalCategory : ModelBase
    {

        #region Property: ID
        protected override void validateIdForDatabase(int id)
        {
            if (!ExternalSignalDatabases.Categories.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Color
        public event PropertyChangedTwoValuesDelegate<ExternalSignalCategory, Color> ColorChanged;

        [PersistAs("color")]
        private Color color;

        public Color Color
        {
            get => color;
            set => setProperty(this, ref color, value, ColorChanged);
        }
        #endregion

    }

}
