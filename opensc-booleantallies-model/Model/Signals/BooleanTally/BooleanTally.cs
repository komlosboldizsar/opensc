using Microsoft.CodeAnalysis;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace OpenSC.Model.Signals.BooleanTallies
{

    public partial class BooleanTally : ModelBase, ISignalTallySender
    {

        #region Persistence, instantiation
        public BooleanTally()
        {
            myRecursionChain = new List<ISignalTallySender>() { this };
        }

        public override void Removed()
        {
            base.Removed();
            Name = null;
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            restoreBoolean();
            updateToTally();
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = BooleanTallyDatabase.Instance;
        #endregion

        #region Property: FromBoolean
        private string _fromBooleanUniqueId; // "Temp foreign key"

        [PersistAs("from")]
        private string fromBooleanUniqueId
        {
            get => fromBoolean?.Identifier;
            set => _fromBooleanUniqueId = value;
        }

        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_fromBoolean_afterChange))]
        private IBoolean fromBoolean;

        private void _fromBoolean_afterChange(IBoolean oldValue, IBoolean newValue)
        {
            bool oldState = oldValue?.CurrentState ?? false;
            bool newState = newValue?.CurrentState ?? false;
            if (!oldState && newState)
                toTally?.Give(myRecursionChain);
            if (oldState && !newState)
                toTally?.Revoke(myRecursionChain);
        }
        #endregion

        #region Property: ToSignal
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(updateToTally))]
        [PersistAs("to")]
        private ISignalSourceRegistered toSignal;
        #endregion

        #region Property: ToTallyColor
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(updateToTally))]
        [PersistAs("to/@color")]
        private SignalTallyColor toTallyColor;
        #endregion

        #region Property: ToTally
        [AutoProperty(PropertyAccessibility = Accessibility.Private)]
        [AutoProperty.BeforeChange(nameof(_toTally_beforeChange))]
        [AutoProperty.AfterChange(nameof(_toTally_afterChange))]
        private IBidirectionalSignalTally toTally;

        private void _toTally_beforeChange(IBidirectionalSignalTally oldValue, IBidirectionalSignalTally newValue)
        {
            if (oldValue != null)
                oldValue.Revoke(myRecursionChain);
        }

        private void _toTally_afterChange(IBidirectionalSignalTally oldValue, IBidirectionalSignalTally newValue)
        {
            if ((newValue != null) && (fromBoolean?.CurrentState == true))
                newValue.Give(myRecursionChain);
        }

        private void updateToTally() => ToTally = toSignal?.GetTally(toTallyColor);
        #endregion

        #region Boolean restoration
        private void restoreBoolean()
        {
            if (_fromBooleanUniqueId != null)
                FromBoolean = BooleanRegister.Instance[_fromBooleanUniqueId];
        }
        #endregion

        #region ISignalTallySender interface
        public string Label => string.Format("Boolean tally [(#{0}) {1}]", ID, Name);
        #endregion

        private List<ISignalTallySender> myRecursionChain;

    }

}
