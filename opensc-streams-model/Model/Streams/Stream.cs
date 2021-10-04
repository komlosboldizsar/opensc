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

        #region ID validation
        protected override void validateIdForDatabase(int id)
        {
            if (!StreamDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
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
            protected set => setProperty(this, ref state, value, StateChanged);
        }
        #endregion

        #region Property: ViewerCount
        public event PropertyChangedTwoValuesDelegate<Stream, int?> ViewerCountChanged;

        private int? viewerCount = null;

        public int? ViewerCount
        {
            get => viewerCount;
            protected set => setProperty(this, ref viewerCount, value, ViewerCountChanged);
        }
        #endregion

    }
}
