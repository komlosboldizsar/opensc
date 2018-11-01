using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public delegate void RouterInputNameChanged(RouterInput input, string oldName, string newName);
    public delegate void RouterInputNameChangedPCN();

    public delegate void RouterInputSourceNameChanged(RouterInput input, string newName);
    public delegate void RouterInputTallyChanged(RouterInput input, bool newState);

    public class RouterInput
    {

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(name))
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
            private set { source = value; }
        }

        // "Temp foreign key"
        public string _sourceString;

        public void RestoreSource()
        {
            if(_sourceString != null)
                Source = RouterInputXmlSerializer.GetSourceByString(_sourceString);
        }

        public string SourceName
        {
            get => source?.SourceName;
        }

        public event RouterInputSourceNameChanged SourceNameChanged;

        public bool RedTally =>
            (source != null) ? source.RedTally : false;

        public bool GreenTally =>
            (source != null) ? source.GreenTally : false;

        public event RouterInputTallyChanged RedTallyChanged;
        public event RouterInputTallyChanged GreenTallyChanged;

    }

}
