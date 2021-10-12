using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public class Label : ObjectBase
    {

        public Label(Labelset labelset, ISystemObject associatedObject = null)
        {
            Labelset = labelset;
            AssociatedObject = associatedObject;
        }

        public Labelset Labelset { get; init; }

        private ISystemObject associatedObject;
        public ISystemObject AssociatedObject
        {
            get => associatedObject;
            set => associatedObject ??= value;
        }

        #region Property: Text
        public event PropertyChangedTwoValuesDelegate<Label, string> TextChanged;

        private string text;

        public string Text
        {
            get => text;
            set => this.setProperty(ref text, value, TextChanged);
        }
        #endregion

    }

}
