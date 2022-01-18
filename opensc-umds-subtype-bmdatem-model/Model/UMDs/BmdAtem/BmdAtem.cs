using BMD.Switcher;
using OpenSC.Model.General;
using OpenSC.Model.Mixers;
using OpenSC.Model.Mixers.BlackMagicDesign;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.BmdAtem
{
    [TypeLabel("BMD ATEM")]
    [TypeCode("bmdatem")]
    public class BmdAtem : UMD
    {

        public override IUMDType Type => new BmdAtemType();

        public override void RestoreCustomRelations()
        {
            base.RestoreCustomRelations();
            restoreInput();
        }

        #region Property: Input
        public event PropertyChangedTwoValuesDelegate<BmdAtem, MixerInput> InputChanged;

        private MixerInput input;

        public MixerInput Input
        {
            get => input;
            set => this.setProperty(ref input, value, InputChanged, null, (ov, nv) => updateInputsSource(), ValidateInputIndex);
        }

        [PersistAs("input")]
        private string _inputId
        {
            get
            {
                if ((input == null) || (input.Mixer == null))
                    return null;
                return $"mixer.{input.Mixer.ID}.input.{input.Index}";
            }
            set => __inputId = value;
        }

        private string __inputId;

        private void restoreInput()
        {
            // Will be easier later when MixerInput implements ISystemObject
            if (__inputId == null)
            {
                Input = null;
                return;
            }
            string[] inputIdParts = __inputId.Split('.');
            if (inputIdParts.Length != 4)
            {
                Input = null;
                return;
            }
            if (!int.TryParse(inputIdParts[1], out int mixerId))
            {
                Input = null;
                return;
            }
            if (!int.TryParse(inputIdParts[3], out int inputIndex))
            {
                Input = null;
                return;
            }
            Mixer mixer = MixerDatabase.Instance.GetTById(mixerId);
            Input = mixer.Inputs.FirstOrDefault(i => i.Index == inputIndex);
        }
        #endregion

        #region Property: NameType
        public event PropertyChangedTwoValuesDelegate<BmdAtem, BmdAtemInputNameType> NameTypeChanged;

        [PersistAs("name_type")]
        private BmdAtemInputNameType nameType = BmdAtemInputNameType.Long;

        public BmdAtemInputNameType NameType
        {
            get => nameType;
            set => this.setProperty(ref nameType, value, NameTypeChanged, null, (ov, nv) => update());
        }
        #endregion

        private Source inputsSource;

        private void updateInputsSource()
        {
            if (input != null)
            {
                BmdMixer bmdMixer = input.Mixer as BmdMixer;
                inputsSource = bmdMixer.ApiSwitcher?.GetSource(input.Index);
            }
            else
            {
                inputsSource = null;
            }
            update();
        }

        public override Color[] TallyColors => new Color[] { };

        protected override void update()
        {
            switch (nameType)
            {
                case BmdAtemInputNameType.Short:
                    inputsSource?.UpdateShortName(CurrentText);
                    break;
                case BmdAtemInputNameType.Long:
                    inputsSource?.UpdateLongName(CurrentText);
                    break;
            }
        }

    }
}
