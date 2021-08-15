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
            NameChanged = null;
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

        #region Property: Name
        public event PropertyChangedTwoValuesDelegate<Stream, string> NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get => name;
            set => setProperty(this, ref name, value, NameChanged, validator: ValidateName);
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();
        }
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
