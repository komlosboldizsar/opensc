using Microsoft.CodeAnalysis;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;

namespace OpenSC.Model.Streams
{

    public abstract partial class Stream : ModelBase
    {

        #region Persistence, instantiation
        public override void Removed()
        {
            base.Removed();
            StateChanged = null;
            ViewerCountChanged = null;
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = StreamDatabase.Instance;
        #endregion

        #region Property: State
        [AutoProperty(SetterAccessibility = Accessibility.Protected)]
        private StreamState state;
        #endregion

        #region Property: ViewerCount
        [AutoProperty(SetterAccessibility = Accessibility.Protected)]
        private int? viewerCount = null;
        #endregion

    }
}
