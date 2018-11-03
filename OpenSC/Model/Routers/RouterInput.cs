using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public delegate void RouterInputNameChanged(RouterInput input, string oldName, string newName);
    public delegate void RouterInputNameChangedPCN();

    public delegate void RouterInputSourceChangedDelegate(RouterInput input, IRouterInputSource oldSource, IRouterInputSource newSource);

    public delegate void RouterInputSourceNameChanged(RouterInput input, string newName);
    public delegate void RouterInputTallyChanged(RouterInput input, bool newState);

    public class RouterInput
    {

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
                NameChangedPCN?.Invoke();
            }
        }

        public event RouterInputNameChanged NameChanged;
        public event RouterInputNameChangedPCN NameChangedPCN;

        public Router Router { get; internal set; }

        private int index;

        public int Index
        {
            get { return index; }
            internal set { index = value; }
        }

        IRouterInputSource source;

        public IRouterInputSource Source
        {
            get { return source; }
            set
            {

                if (value == source)
                    return;

                if(source != null)
                {
                    source.SourceNameChanged -= sourceNameChangedHandler;
                    source.RedTallyChanged -= sourceRedTallyChangedHandler;
                    source.GreenTallyChanged -= sourceGreenTallyChangedHandler;
                }

                IRouterInputSource oldSource = source;
                source = value;

                RouterInputSourceChanged?.Invoke(this, oldSource, value);
                RouterInputSourceChangedPCN?.Invoke();

                SourceNameChanged?.Invoke(this, source?.SourceName);
                RedTallyChanged?.Invoke(this, (source?.RedTally == true));
                GreenTallyChanged?.Invoke(this, (source?.GreenTally == true));

                if (source != null)
                {
                    source.SourceNameChanged += sourceNameChangedHandler;
                    source.RedTallyChanged += sourceRedTallyChangedHandler;
                    source.GreenTallyChanged += sourceGreenTallyChangedHandler;
                }

            }
        }

        public event RouterInputSourceChangedDelegate RouterInputSourceChanged;
        public event ParameterlessChangeNotifierDelegate RouterInputSourceChangedPCN;

        // "Temp foreign key"
        public string _sourceString;

        public void RestoreSource()
        {
            if(_sourceString != null)
                Source = RouterInputXmlSerializer.GetSourceByString(_sourceString);
        }

        public string SourceName
        {
            get => GetSourceName();
        }

        public string GetSourceName(List<object> recursionChain = null)
        {
            if (source == null)
                return null;
            if (recursionChain == null)
                recursionChain = new List<object>();
            if (recursionChain.Contains(this))
                return "(cyclic tieline)";
            recursionChain.Add(this);
            return source.GetSourceName(recursionChain);
        }

        public event RouterInputSourceNameChanged SourceNameChanged;

        private void sourceNameChangedHandler(IRouterInputSource inputSource, string newName)
        {
            SourceNameChanged?.Invoke(this, newName);
        }

        public bool RedTally =>
            (source != null) ? source.RedTally : false;

        public bool GreenTally =>
            (source != null) ? source.GreenTally : false;

        public bool GetRedTally(List<object> recursionChain = null)
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
        }

        public event RouterInputTallyChanged RedTallyChanged;
        public event RouterInputTallyChanged GreenTallyChanged;

        private void sourceRedTallyChangedHandler(IRouterInputSource inputSource, bool newState)
        {
            RedTallyChanged?.Invoke(this, newState);
        }

        private void sourceGreenTallyChangedHandler(IRouterInputSource inputSource, bool newState)
        {
            GreenTallyChanged?.Invoke(this, newState);
        }

    }

}
