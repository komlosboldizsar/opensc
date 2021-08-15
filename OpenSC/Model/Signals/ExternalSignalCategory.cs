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
        public event PropertyChangedTwoValuesDelegate<ExternalSignalCategory, int> IdChanged;

        public int id = 0;

        public override int ID
        {
            get => id;
            set => setProperty(this, ref id, value, IdChanged, validator: ValidateId);
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!ExternalSignalDatabases.Categories.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public event PropertyChangedTwoValuesDelegate<ExternalSignalCategory, string> NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get => name;
            set => setProperty(this, ref name, value, NameChanged);
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
