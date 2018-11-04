using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers
{

    public delegate void MixerInputNameChangedDelegate(MixerInput input, string oldName, string newName);

    public delegate void MixerInputSourceChangedDelegate(MixerInput input, Signal oldSource, Signal newSource);
    public delegate void MixerInputSourceNameChangedDelegate(MixerInput input, string newName);

    public delegate void MixerInputTallyChangedDelegate(MixerInput input, bool newState);

    public class MixerInput
    {

        public MixerInput()
        { }

        public MixerInput(string name, Mixer mixer, int index)
        {
            this.name = name;
            this.Mixer = mixer;
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
                NameChangedPCN?.Invoke();
            }
        }

        public event MixerInputNameChangedDelegate NameChanged;
        public event ParameterlessChangeNotifierDelegate NameChangedPCN;

        public Mixer Mixer { get; internal set; }

        public void RemovedFromMixer(Mixer mixer)
        {
            if (mixer != Mixer)
                return;
        }

        private int index;

        public int Index
        {
            get { return index; }
            internal set { index = value; }
        }

        Signal source;

        public Signal Source
        {
            get { return source; }
            set
            {

                if (value == source)
                    return;

                if (source != null)
                {
                    // Remove tally from the old assigned signal
                }

                Signal oldSource = source;
                source = value;

                SourceChanged?.Invoke(this, oldSource, value);
                SourceChangedPCN?.Invoke();

                if (source != null)
                {
                    // Set tally for the assigned signal
                }

            }
        }

        public event MixerInputSourceChangedDelegate SourceChanged;
        public event ParameterlessChangeNotifierDelegate SourceChangedPCN;

        // "Temp foreign key"
        public int _sourceSignalId;

        public void RestoreSource()
        {
            if (_sourceSignalId > 0)
                Source = SignalDatabases.Signals.GetTById(_sourceSignalId);
        }

        public string SourceName
        {
            get => source.Name;
        }

        private bool redTally;

        public bool RedTally
        {
            get => redTally;
            set
            {
                if (value == redTally)
                    return;
                redTally = value;
                RedTallyChanged?.Invoke(this, value);
                RedTallyChangedPCN?.Invoke();
                if (source != null) {
                    // Source signal is tallied/not tallied from this input
                }
            }
        }

        public event MixerInputTallyChangedDelegate RedTallyChanged;
        public event ParameterlessChangeNotifierDelegate RedTallyChangedPCN;

        private bool greenTally;

        public bool GreenTally
        {
            get => greenTally;
            set
            {
                if (value == greenTally)
                    return;
                greenTally = value;
                GreenTallyChanged?.Invoke(this, value);
                GreenTallyChangedPCN?.Invoke();
                if (source != null)
                {
                    // Source signal is tallied/not tallied from this input
                }
            }
        }

        public event MixerInputTallyChangedDelegate GreenTallyChanged;
        public event ParameterlessChangeNotifierDelegate GreenTallyChangedPCN;

    }

}
