using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;

namespace OpenSC.Model.Streams
{

    public abstract class Stream : ModelBase
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
        public event PropertyChangedTwoValuesDelegate<Stream, StreamState> StateChanged;

        private StreamState state;

        public StreamState State
        {
            get => state;
            protected set => this.setProperty(ref state, value, StateChanged);
        }
        #endregion

        #region Property: ViewerCount
        public event PropertyChangedTwoValuesDelegate<Stream, int?> ViewerCountChanged;

        private int? viewerCount = null;

        public int? ViewerCount
        {
            get => viewerCount;
            protected set => this.setProperty(ref viewerCount, value, ViewerCountChanged);
        }
        #endregion

    }
}
