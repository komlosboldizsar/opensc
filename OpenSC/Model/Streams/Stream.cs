using OpenSC.Model.Persistence;
using System;

namespace OpenSC.Model.Streams
{

    public delegate void StreamIdChangingDelegate(Stream stream, int oldValue, int newValue);
    public delegate void StreamIdChangedDelegate(Stream stream, int oldValue, int newValue);
   
    public delegate void StreamNameChangingDelegate(Stream stream, string oldName, string newName);
    public delegate void StreamNameChangedDelegate(Stream stream, string oldName, string newName);

    public delegate void StreamStateChangingDelegate(Stream stream, StreamState oldState, StreamState newState);
    public delegate void StreamStateChangedDelegate(Stream stream, StreamState oldState, StreamState newState);

    public delegate void StreamViewerCountChangingDelegate(Stream stream, int? oldCount, int? newCount);
    public delegate void StreamViewerCountChangedDelegate(Stream stream, int? oldCount, int? newCount);

    public abstract class Stream: ModelBase
    {

        public override void Restored()
        { }

        public event StreamIdChangingDelegate IdChanging;
        public event StreamIdChangedDelegate IdChanged;
        public event ParameterlessChangeNotifierDelegate IdChangingPCN;
        public event ParameterlessChangeNotifierDelegate IdChangedPCN;

        public int id = 0;

        public override int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                int oldValue = id;
                IdChanging?.Invoke(this, oldValue, value);
                IdChangingPCN?.Invoke();
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(ID));
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!StreamDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public event StreamNameChangingDelegate NameChanging;
        public event StreamNameChangedDelegate NameChanged;
        public event ParameterlessChangeNotifierDelegate NameChangingPCN;
        public event ParameterlessChangeNotifierDelegate NameChangedPCN;

        [PersistAs("name")]
        private string name = "Test";

        public string Name
        {
            get { return name; }
            set
            {
                ValidateName(value);
                string oldName = name;
                NameChanging?.Invoke(this, oldName, value);
                NameChangingPCN?.Invoke();
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                RaisePropertyChanged(nameof(Name));
            }
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();
        }

        public event StreamStateChangingDelegate StateChanging;
        public event StreamStateChangedDelegate StateChanged;
        public event ParameterlessChangeNotifierDelegate StateChangingPCN;
        public event ParameterlessChangeNotifierDelegate StateChangedPCN;

        private StreamState state;

        public StreamState State
        {
            get { return state; }
            protected set {
                StreamState oldState = state;
                StateChanging?.Invoke(this, oldState, value);
                StateChangingPCN?.Invoke();
                state = value;
                StateChanged?.Invoke(this, oldState, value);
                RaisePropertyChanged(nameof(State));
            }
        }

        public event StreamViewerCountChangingDelegate ViewerCountChanging;
        public event StreamViewerCountChangedDelegate ViewerCountChanged;
        public event ParameterlessChangeNotifierDelegate ViewerCountChangingPCN;
        public event ParameterlessChangeNotifierDelegate ViewerCountChangedPCN;

        private int? viewerCount;

        public int? ViewerCount
        {
            get { return viewerCount; }
            protected set
            {
                int? oldCount = viewerCount;
                ViewerCountChanging?.Invoke(this, oldCount, value);
                ViewerCountChangingPCN?.Invoke();
                viewerCount = value;
                ViewerCountChanged?.Invoke(this, oldCount, value);
                RaisePropertyChanged(nameof(ViewerCount));
            }
        }

    }
}
