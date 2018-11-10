using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers
{

    public class MixerInput : ISignalTallySource
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
            }
        }

        public delegate void InputNameChangedDelegate(MixerInput input, string oldName, string newName);
        public event InputNameChangedDelegate NameChanged;

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

        ExternalSignal source;

        public ExternalSignal Source
        {
            get { return source; }
            set
            {

                if (value == source)
                    return;

                source?.IsTalliedFrom(this, SignalTallyType.Red, false);
                source?.IsTalliedFrom(this, SignalTallyType.Green, false);

                ExternalSignal oldSource = source;
                source = value;

                SourceChanged?.Invoke(this, oldSource, value);

                source?.IsTalliedFrom(this, SignalTallyType.Red, RedTally);
                source?.IsTalliedFrom(this, SignalTallyType.Green, GreenTally);

            }
        }

        public event SourceChangedDelegate SourceChanged;

        public delegate void SourceChangedDelegate(MixerInput input, ExternalSignal oldSource, ExternalSignal newSource);
        public delegate void SourceNameChangedDelegate(MixerInput input, string newName);

        // "Temp foreign key"
        public int _sourceSignalId;

        public void RestoreSource()
        {
            if (_sourceSignalId > 0)
                Source = ExternalSignalDatabases.Signals.GetTById(_sourceSignalId);
        }

        public string SourceName
        {
            get => source.Name;
        }


        public delegate void TallyChangedDelegate(MixerInput input, bool newState);

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
                source?.IsTalliedFrom(this, SignalTallyType.Red, value);
            }
        }

        public event TallyChangedDelegate RedTallyChanged;

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
                source?.IsTalliedFrom(this, SignalTallyType.Green, value);
            }
        }

        public event TallyChangedDelegate GreenTallyChanged;

    }

}
