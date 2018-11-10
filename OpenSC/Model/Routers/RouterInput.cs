using OpenSC.Model.General;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public class RouterInput : INotifyPropertyChanged
    {

        public RouterInput()
        { }

        public RouterInput(string name, Router router, int index)
        {
            this.name = name;
            this.Router = router;
            this.Index = index;
        }

        public void Restored()
        { }
        
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException();
                if (value == name)
                    return;
                string oldName = name;
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                PropertyChanged?.Invoke(nameof(Name));
            }
        }

        public delegate void NameChangedDelegate(RouterInput input, string oldName, string newName);
        public event NameChangedDelegate NameChanged;

        public Router Router { get; internal set; }

        public void RemovedFromRouter(Router router)
        {
            if (router != Router)
                return;
        }

        private int index;

        public int Index
        {
            get { return index; }
            internal set { index = value; }
        }

        ISignal source;

        public ISignal Source
        {
            get { return source; }
            set
            {

                if (value == source)
                    return;

                if(source != null)
                {
                    source.SignalLabelChanged -= sourceNameChangedHandler;
                    source.RedTallyChanged -= sourceRedTallyChangedHandler;
                    source.GreenTallyChanged -= sourceGreenTallyChangedHandler;
                }

                ISignal oldSource = source;
                source = value;

                SourceChanged?.Invoke(this, oldSource, value);
                PropertyChanged?.Invoke(nameof(Source));

                SourceNameChanged?.Invoke(this, source?.SignalLabel);
                RedTallyChanged?.Invoke(this, (source?.RedTally == true));
                GreenTallyChanged?.Invoke(this, (source?.GreenTally == true));

                if (source != null)
                {
                    source.SignalLabelChanged += sourceNameChangedHandler;
                    source.RedTallyChanged += sourceRedTallyChangedHandler;
                    source.GreenTallyChanged += sourceGreenTallyChangedHandler;
                }

            }
        }


        public delegate void SourceChangedDelegate(RouterInput input, ISignal oldSource, ISignal newSource);
        public event SourceChangedDelegate SourceChanged;

        // "Temp foreign key"
        public string _sourceSignalUniqueId;

        public void RestoreSource()
        {
            if (_sourceSignalUniqueId != null)
                Source = SignalRegister.Instance.GetSignalByUniqueId(_sourceSignalUniqueId);
        }

        public string SourceName
        {
            get => source.SignalLabel;
        }

        /*public string GetSourceName(List<object> recursionChain = null)
        {
            if (source == null)
                return null;
            if (recursionChain == null)
                recursionChain = new List<object>();
            if (recursionChain.Contains(this))
                return "(cyclic tieline)";
            recursionChain.Add(this);
            return source.GetSourceName(recursionChain);
        }*/

        public delegate void RouterInputSourceNameChanged(RouterInput input, string newName);
        public event RouterInputSourceNameChanged SourceNameChanged;

        private void sourceNameChangedHandler(ISignal inputSource, string newName)
        {
            SourceNameChanged?.Invoke(this, newName);
        }

        public bool RedTally =>
            (source != null) ? source.RedTally : false;

        public bool GreenTally =>
            (source != null) ? source.GreenTally : false;

        /*public bool GetRedTally(List<object> recursionChain = null)
        {
            if (source == null)
                return false;
            if (recursionChain == null)
                recursionChain = new List<object>();
            if (recursionChain.Contains(this))
                return false;
            recursionChain.Add(this);
            return source.GetRedTally(recursionChain);
        }

        public bool GetGreenTally(List<object> recursionChain = null)
        {
            if (source == null)
                return false;
            if (recursionChain == null)
                recursionChain = new List<object>();
            if (recursionChain.Contains(this))
                return false;
            recursionChain.Add(this);
            return source.GetGreenTally(recursionChain);
        }*/

        public delegate void TallyChangedDelegate(RouterInput input, bool newState);
        public event TallyChangedDelegate RedTallyChanged;
        public event TallyChangedDelegate GreenTallyChanged;

        private void sourceRedTallyChangedHandler(ISignal inputSource, bool oldState, bool newState)
        {
            RedTallyChanged?.Invoke(this, newState);
        }

        private void sourceGreenTallyChangedHandler(ISignal inputSource, bool oldState, bool newState)
        {
            GreenTallyChanged?.Invoke(this, newState);
        }

        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedDelegate PropertyChanged;
        #endregion

    }

}
