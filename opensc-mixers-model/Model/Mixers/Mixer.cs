using Microsoft.CodeAnalysis;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers
{

    public abstract partial class Mixer : ModelBase
    {

        public Mixer()
        { }

        public override void RestoredOwnFields()
        {
            restoreInputs();
        }

        public override void Removed()
        {
            base.Removed();
            OnProgramInputChanged = null;
            OnProgramInputNameChanged = null;
            OnPreviewInputChanged = null;
            OnPreviewInputNameChanged = null;
            StateChanged = null;
            StateStringChanged = null;
            InputsChanged = null;
            inputs.Foreach(i => i.RemovedFromMixer(this));
            inputs.Clear();
        }

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = MixerDatabase.Instance;
        #endregion

        #region Property: OnProgramInput, OnProgramInputName
        [AutoProperty(SetterAccessibility = Accessibility.Protected)]
        [AutoProperty.BeforeChange(nameof(_onProgramInput_beforeChange))]
        [AutoProperty.AfterChange(nameof(_onProgramInput_afterChange))]
        private MixerInput onProgramInput;

        private void _onProgramInput_beforeChange(MixerInput oldValue, MixerInput newValue, BeforeChangePropertyArgs args)
        {
            if (oldValue != null)
                oldValue.NameChanged -= onProgramInputNameChangedHandler;
        }

        private void _onProgramInput_afterChange(MixerInput oldValue, MixerInput newValue)
        {
            if (newValue != null)
            {
                newValue.NameChanged += onProgramInputNameChangedHandler;
                OnProgramInputName = newValue.Name;
            }
            else
            {
                OnProgramInputName = null;
            }
        }

        [AutoProperty(SetterAccessibility = Accessibility.Private)]
        private string onProgramInputName;

        private void onProgramInputNameChangedHandler(MixerInput input, string oldValue, string newValue)
            => OnProgramInputName = newValue;
        #endregion

        #region Property: OnPreviewInput, OnPreviewInputName
        [AutoProperty(SetterAccessibility = Accessibility.Protected)]
        [AutoProperty.BeforeChange(nameof(_onPreviewInput_beforeChange))]
        [AutoProperty.AfterChange(nameof(_onPreviewInput_afterChange))]
        private MixerInput onPreviewInput;

        private void _onPreviewInput_beforeChange(MixerInput oldValue, MixerInput newValue, BeforeChangePropertyArgs args)
        {
            if (oldValue != null)
                oldValue.NameChanged -= onPreviewInputNameChangedHandler;
        }

        private void _onPreviewInput_afterChange(MixerInput oldValue, MixerInput newValue)
        {
            if (newValue != null)
            {
                newValue.NameChanged += onPreviewInputNameChangedHandler;
                OnPreviewInputName = newValue.Name;
            }
            else
            {
                OnPreviewInputName = null;
            }
        }

        [AutoProperty(SetterAccessibility = Accessibility.Private)]
        private string onPreviewInputName;

        private void onPreviewInputNameChangedHandler(MixerInput input, string oldValue, string newValue)
            => OnPreviewInputName = newValue;
        #endregion

        #region Property: GivesRedTallyToSources, GivesGreenTallyToSources
        [AutoProperty]
        [PersistAs("gives_tally/@red")]
        private bool givesRedTallyToSources;

        [AutoProperty]
        [PersistAs("gives_tally/@green")]
        private bool givesGreenTallyToSources;
        #endregion

        #region Property: State
        [AutoProperty(SetterAccessibility = Accessibility.Protected)]
        private MixerState state = MixerState.Unknown;
        #endregion

        #region Property: StateString
        [AutoProperty(SetterAccessibility = Accessibility.Protected)]
        private string stateString = "?";
        #endregion

        #region Inputs
        private ObservableList<MixerInput> inputs = new ObservableList<MixerInput>();

        public ObservableList<MixerInput> Inputs => inputs;

        [PersistAs("inputs")]
        [PersistAs("input", 1)]
        private MixerInput[] _inputs
        {
            get { return inputs.ToArray(); }
            set
            {
                inputs.Clear();
                if (value != null)
                    inputs.AddRange(value);
            }
        }

        public void AddInput()
        {
            int index = ((inputs.Count > 0) ? (inputs.Max(input => input.Index) + 1) : 1);
            inputs.Add(new MixerInput(string.Format("Input #{0}", index), this, index));
        }

        public void RemoveInput(MixerInput input)
        {
            inputs.Remove(input);
            input.RemovedFromMixer(this);
            if (input == onProgramInput)
                OnProgramInput = null;
            if (input == onPreviewInput)
                OnPreviewInput = null;
        }

        private void inputsChangedHandler()
        {
            InputsChanged?.Invoke(this);
            RaisePropertyChanged(nameof(Inputs));
        }
        private void restoreInputs()
        {
            foreach (MixerInput input in inputs)
                input.Mixer = this;
            foreach (MixerInput input in inputs)
                input.Restored();
        }

        public event PropertyChangedNoValueDelegate<Mixer> InputsChanged;
        #endregion

    }

}
