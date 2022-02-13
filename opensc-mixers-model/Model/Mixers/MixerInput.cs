using OpenSC.Model.General;
using OpenSC.Model.Signals;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers
{

    public class MixerInput : ObjectBase, ISignalTallySender
    {

        public MixerInput()
        { }

        public MixerInput(string name, Mixer mixer, int index)
        {
            this.name = name;
            this.Mixer = mixer;
            this.Index = index;
            createBooleans();
        }

        public void Restored()
        {
            restoreSource();
            createBooleans();
        }


        public Mixer Mixer { get; internal set; }

        public void RemovedFromMixer(Mixer mixer)
        {
            if (mixer != Mixer)
                return;
        }

        #region Property: Name
        public event PropertyChangedTwoValuesDelegate<MixerInput, string> NameChanged;

        private string name;

        public string Name
        {
            get => name;
            set
            {
                ValidateName(value);
                this.setProperty(ref name, value, NameChanged);
            }
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Index
        public event PropertyChangedTwoValuesDelegate<MixerInput, int> IndexChanged;

        private int index;

        public int Index
        {
            get => index;
            set => this.setProperty(ref index, value, IndexChanged);
        }
        #endregion

        #region Property: Source
        public event PropertyChangedTwoValuesDelegate<MixerInput, ISignalSourceRegistered> SourceChanged;

        private ISignalSourceRegistered source;

        public ISignalSourceRegistered Source
        {
            get => source;
            set
            {
                List<ISignalTallySender> recursionChain = new List<ISignalTallySender>();
                recursionChain.Add(this);
                BeforeChangePropertyDelegate<ISignalSourceRegistered> beforeChangeDelegate = (ov, nv) => {
                    if (ov != null)
                    {
                        ov.SignalLabelChanged -= signalLabelChangedHandler;
                        ov.RedTally.Revoke(recursionChain);
                        ov.YellowTally.Revoke(recursionChain);
                        ov.GreenTally.Revoke(recursionChain);
                    }
                };
                AfterChangePropertyDelegate<ISignalSourceRegistered> afterChangeDelegate = (ov, nv) => {
                    if (nv != null)
                    {
                        nv.SignalLabelChanged += signalLabelChangedHandler;
                        if (RedTally)
                            nv.RedTally.Give(recursionChain);
                        /* if (YellowTally)
                            nv.YellowTally.Give(recursionChain); */
                        if (GreenTally)
                            nv.GreenTally.Give(recursionChain);
                    }
                };
                this.setProperty(ref source, value, SourceChanged, beforeChangeDelegate, afterChangeDelegate);
                SourceSignalLabelChanged?.Invoke(this, source?.SignalLabel);
                ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(SourceName));
            }
        }
        #endregion

        #region Property: SourceName
        public string SourceName => source.SignalLabel;

        private void signalLabelChangedHandler(ISignalSource signal, string newLabel)
            => SourceSignalLabelChanged?.Invoke(this, newLabel);

        public PropertyChangedOneValueDelegate<MixerInput, string> SourceSignalLabelChanged;
        #endregion

        // "Temp foreign key"
        public string _sourceSignalUniqueId;

        private void restoreSource()
        {
            if (_sourceSignalUniqueId != null)
                Source = SignalRegister.Instance[_sourceSignalUniqueId];
        }

        public event PropertyChangedOneValueDelegate<MixerInput, bool> RedTallyChanged;

        private bool redTally;

        public bool RedTally
        {
            get => redTally;
            set
            {
                if (!this.setProperty(ref redTally, value, RedTallyChanged))
                    return;
                List<ISignalTallySender> recursionChain = new List<ISignalTallySender>();
                recursionChain.Add(this);
                if (value)
                    source?.RedTally.Give(recursionChain);
                else
                    source?.RedTally.Revoke(recursionChain);
            }
        }

        public event PropertyChangedOneValueDelegate<MixerInput, bool> GreenTallyChanged;

        private bool greenTally;

        public bool GreenTally
        {
            get => greenTally;
            set
            {
                if (!this.setProperty(ref greenTally, value, GreenTallyChanged))
                    return;
                List<ISignalTallySender> recursionChain = new List<ISignalTallySender>();
                recursionChain.Add(this);
                if (value)
                    source?.GreenTally.Give(recursionChain);
                else
                    source?.GreenTally.Revoke(recursionChain);

            }
        }

        #region Tally booleans
        private IBoolean redTallyBoolean = null;
        private IBoolean greenTallyBoolean = null;

        private void createBooleans()
        {
            redTallyBoolean = new MixerInputTallyBoolean(this, MixerInputTallyBoolean.TallyColor.Red);
            greenTallyBoolean = new MixerInputTallyBoolean(this, MixerInputTallyBoolean.TallyColor.Green);
            BooleanRegister.Instance.Register(redTallyBoolean);
            BooleanRegister.Instance.Register(greenTallyBoolean);
        }

        private void removeBooleans()
        {
            if (redTallyBoolean != null)
            {
                BooleanRegister.Instance.Unregister(redTallyBoolean);
                redTallyBoolean = null;
            }
            if (greenTallyBoolean != null)
            {
                BooleanRegister.Instance.Unregister(greenTallyBoolean);
                greenTallyBoolean = null;
            }
        }
        #endregion

        string ISignalTallySender.Label => string.Format("Mixer [#{0} ({1})], input [#{2} ({3})]", Mixer.ID, Mixer.Name, Index, Name);

    }

}
