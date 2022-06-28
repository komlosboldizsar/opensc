using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.Settings;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    [TypeLabel("Flip-flop/latch")]
    [TypeCode("flipflop")]
    public partial class FlipFlopBoolean : CustomBoolean
    {

        #region Restoration
        public override void RestoreCustomRelations()
        {
            base.RestoreCustomRelations();
            restoreBooleans();
        }

        private void restoreBooleans()
        {
            if (_input1Identifier != null)
                Input1 = BooleanRegister.Instance[_input1Identifier];
            if (_input2Identifier != null)
                Input2 = BooleanRegister.Instance[_input2Identifier];
        }
        #endregion

        #region Base properties
        public override bool IdentifierUserEditable => false;
        public override string GetIdentifierByData(CustomBooleanDataStore dataStore) => $"flipflop.{dataStore.ID}";

        public override bool ColorUserEditable => true;
        public override Color GetColorByData(CustomBooleanDataStore dataStore) => Color.White;

        public override bool DescriptionUserEditable => false;
        public override string GetDescriptionByData(CustomBooleanDataStore dataStore)
        {
            FlipFlopDataStore typedDataStore = (FlipFlopDataStore)dataStore;
            return GetTypeDescriptorByType(typedDataStore.Type).GetDescription(
                typedDataStore.Input1?.Identifier, typedDataStore.Input1Inverted,
                typedDataStore.Input2?.Identifier, typedDataStore.Input2Inverted);
        }
        #endregion

        #region Data store
        protected override CustomBooleanDataStore getDataStore(CustomBooleanDataStore dataStore = null)
        {
            FlipFlopDataStore typedDataStore = (FlipFlopDataStore)dataStore;
            if (typedDataStore == null)
                typedDataStore = new FlipFlopDataStore();
            typedDataStore.Type = Type;
            typedDataStore.Input1 = Input1;
            typedDataStore.Input2 = Input2;
            return base.getDataStore(typedDataStore);
        }
        #endregion

        #region Property: Type
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_type_afterChange))]
        [PersistAs("type")]
        private FlipFlopType type;

        private void _type_afterChange(FlipFlopType oldValue, FlipFlopType newValue)
        {
            TypeDescriptor = GetTypeDescriptorByType(newValue);
            recalculateState();
        }

        protected FlipFlopTypeDescriptor TypeDescriptor { get; private set; }

        public static FlipFlopTypeDescriptor GetTypeDescriptorByType(FlipFlopType type)
        {
            return type switch
            {
                FlipFlopType.FlipFlopD => TypeDescriptors.FlipFlopD.Instance,
                FlipFlopType.LatchSR => TypeDescriptors.LatchSR.Instance,
                FlipFlopType.LatchRS => TypeDescriptors.LatchRS.Instance,
                FlipFlopType.FlipFlopSR => TypeDescriptors.FlipFlopSR.Instance,
                FlipFlopType.LatchJK => TypeDescriptors.LatchJK.Instance,
                FlipFlopType.FlipFlopT => TypeDescriptors.FlipFlopT.Instance,
                _ => null
            };
        }
        #endregion

        #region Property: Input1
        private string _input1Identifier; // "Temp foreign key"

#pragma warning disable CS0169
        [PersistAs("input1")]
        private string input1Identifier
        {
            get => input1?.Identifier;
            set => _input1Identifier = value;
        }
#pragma warning restore CS0169

        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_input1_beforeChange))]
        [AutoProperty.AfterChange(nameof(_input1_afterChange))]
        private IBoolean input1;

        private void _input1_beforeChange(IBoolean oldValue, IBoolean newValue, BeforeChangePropertyArgs args)
        {
            if (oldValue != null)
                oldValue.StateChanged -= input1StateChanged;
        }

        private void _input1_afterChange(IBoolean oldValue, IBoolean newValue)
        {
            if (newValue != null)
                newValue.StateChanged += input1StateChanged;
            recalculateState();
        }

        private void input1StateChanged(IBoolean item, bool oldValue, bool newValue) => recalculateState(1);
        #endregion

        #region Property: Input1Inverted
        [AutoProperty]
        [PersistAs("input1_inverted")]
        private bool input1Inverted;
        #endregion

        #region Property: Input2
        private string _input2Identifier; // "Temp foreign key"

#pragma warning disable CS0169
        [PersistAs("input2")]
        private string input2Identifier
        {
            get => input2?.Identifier;
            set => _input2Identifier = value;
        }
#pragma warning restore CS0169

        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_input2_beforeChange))]
        [AutoProperty.AfterChange(nameof(_input2_afterChange))]
        private IBoolean input2;

        private void _input2_beforeChange(IBoolean oldValue, IBoolean newValue, BeforeChangePropertyArgs args)
        {
            if (oldValue != null)
                oldValue.StateChanged -= input2StateChanged;
        }

        private void _input2_afterChange(IBoolean oldValue, IBoolean newValue)
        {
            if (newValue != null)
                newValue.StateChanged += input2StateChanged;
            recalculateState();
        }

        private void input2StateChanged(IBoolean item, bool oldValue, bool newValue) => recalculateState(2);
        #endregion

        #region Property: Input2Inverted
        [AutoProperty]
        [PersistAs("input2_inverted")]
        private bool input2Inverted;
        #endregion

        #region Property: EnableSetByUser
        [AutoProperty]
        [PersistAs("enable_set_by_user")]
        private bool enableSetByUser;
        #endregion

        #region Property: EnableResetByUser
        [AutoProperty]
        [PersistAs("enable_reset_by_user")]
        private bool enableResetByUser;
        #endregion

        #region Property: EnableToggleByUser
        [AutoProperty]
        [PersistAs("enable_toggle_by_user")]
        private bool enableToggleByUser;
        #endregion

        #region  State modifier methods
        public void Set() => CurrentState = true;
        public void Reset() => CurrentState = false;
        public void Toggle() => CurrentState = !CurrentState;

        private void recalculateState(int edge = 0)
        {
            if (TypeDescriptor == null)
                return;
            TypeDescriptor.CalculateState(CurrentState,
                input1?.CurrentState ?? false, input1Inverted, (edge == 1),
                Input2?.CurrentState ?? false, input2Inverted, (edge == 2));
        }
        #endregion

    }

}
