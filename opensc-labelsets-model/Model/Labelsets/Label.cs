using OpenSC.Model.General;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Labelsets
{

    public partial class Label : ObjectBase
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
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_text_afterChange))]
        private string text;

        private void _text_afterChange(string oldValue, string newValue) => Labelset.NotifyLabelTextChanged(this, newValue);
        #endregion

    }

}
