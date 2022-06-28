using OpenSC.Model.General;
using OpenSC.Model.Signals;
using OpenSC.Model.SourceGenerators;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers
{

    public partial class MixerInput : ObjectBase, ISignalTallySender
    {

        public MixerInput()
        {
            createMyRecursionChain();
        }

        public MixerInput(string name, Mixer mixer, int index) : this()
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

        private Mixer mixer;

        public Mixer Mixer
        {
            get => mixer;
            internal set
            {
                if (mixer != null)
                {
                    mixer.GivesRedTallyToSourcesChanged -= mixerGivesRedTallyToSourcesChanged;
                    mixer.GivesGreenTallyToSourcesChanged -= mixerGivesGreenTallyToSourcesChanged;
                }
                mixer = value;
                if (mixer != null)
                {
                    mixer.GivesRedTallyToSourcesChanged += mixerGivesRedTallyToSourcesChanged;
                    mixer.GivesGreenTallyToSourcesChanged += mixerGivesGreenTallyToSourcesChanged;
                }
                updateRedTally();
                updateGreenTally();
            }
        }

        private void mixerGivesRedTallyToSourcesChanged(Mixer item, bool oldValue, bool newValue) => updateRedTally();
        private void mixerGivesGreenTallyToSourcesChanged(Mixer item, bool oldValue, bool newValue) => updateGreenTally();

        public void RemovedFromMixer(Mixer mixer)
        {
            if (mixer != Mixer)
                return;
        }

        #region Property: Name
        [AutoProperty]
        [AutoProperty.Validator(nameof(ValidateName))]
        private string name;

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Index
        [AutoProperty]
        private int index;
        #endregion

        #region Property: Source
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_source_beforeChange))]
        [AutoProperty.AfterChange(nameof(_source_afterChange))]
        private ISignalSourceRegistered source;

        private void _source_beforeChange(ISignalSourceRegistered oldValue, ISignalSourceRegistered newValue, BeforeChangePropertyArgs args)
        {
            if (oldValue != null)
            {
                oldValue.SignalLabelChanged -= signalLabelChangedHandler;
                oldValue.RedTally.Revoke(myRecursionChain);
                oldValue.YellowTally.Revoke(myRecursionChain);
                oldValue.GreenTally.Revoke(myRecursionChain);
            }
        }

        private void _source_afterChange(ISignalSourceRegistered oldValue, ISignalSourceRegistered newValue)
        {
            if (newValue != null)
            {
                newValue.SignalLabelChanged += signalLabelChangedHandler;
                if (RedTally)
                    newValue.RedTally.Give(myRecursionChain);
                /* if (YellowTally)
                    nv.YellowTally.Give(myRecursionChain); */
                if (GreenTally)
                    newValue.GreenTally.Give(myRecursionChain);
            }
            SourceSignalLabelChanged?.Invoke(this, source?.SignalLabel);
            ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(SourceName));
        }
        #endregion

        #region Property: SourceName
        public string SourceName => source.SignalLabel;

        private void signalLabelChangedHandler(ISignalSource signal, string newLabel)
            => SourceSignalLabelChanged?.Invoke(this, newLabel);

        public PropertyChangedOneValueDelegate<MixerInput, string> SourceSignalLabelChanged;
        #endregion

        #region My recursion chain
        private List<ISignalTallySender> myRecursionChain;
        private void createMyRecursionChain() => myRecursionChain = new List<ISignalTallySender>() { this };
        #endregion

        // "Temp foreign key"
        public string _sourceSignalUniqueId;

        private void restoreSource()
        {
            if (_sourceSignalUniqueId != null)
                Source = SignalRegister.Instance[_sourceSignalUniqueId];
        }

        #region Property: RedTally
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_redTally_afterChange))]
        private bool redTally;

        private void _redTally_afterChange(bool oldValue, bool newValue) => updateRedTally();

        private void updateRedTally()
        {
            if (redTally && Mixer.GivesRedTallyToSources)
                source?.RedTally.Give(myRecursionChain);
            else
                source?.RedTally.Revoke(myRecursionChain);
        }
        #endregion

        #region Property: GreenTally
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_greenTally_afterChange))]
        private bool greenTally;

        private void _greenTally_afterChange(bool oldValue, bool newValue) => updateGreenTally();

        private void updateGreenTally()
        {
            if (greenTally && Mixer.GivesGreenTallyToSources)
                source?.RedTally.Give(myRecursionChain);
            else
                source?.RedTally.Revoke(myRecursionChain);
        }
        #endregion

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
