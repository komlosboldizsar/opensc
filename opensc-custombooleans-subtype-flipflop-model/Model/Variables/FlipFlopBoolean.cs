using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.Settings;
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
    public class FlipFlopBoolean : CustomBoolean
    {

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
        public event PropertyChangedTwoValuesDelegate<FlipFlopBoolean, FlipFlopType> TypeChanged;

        [PersistAs("type")]
        private FlipFlopType type;

        public FlipFlopType Type
        {
            get => type;
            set => this.setProperty(ref type, value, TypeChanged, null, (ov, nv) => {
                TypeDescriptor = GetTypeDescriptorByType(nv);
                recalculateState();
            });
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
        public event PropertyChangedTwoValuesDelegate<FlipFlopBoolean, IBoolean> Input1Changed;

        [PersistAs("input1")]
        private IBoolean input1;

        public IBoolean Input1
        {
            get => input1;
            set => this.setProperty(ref input1, value, Input1Changed, null, (_, _) => recalculateState(1));
        }
        #endregion

        #region Property: Input1Inverted
        public event PropertyChangedTwoValuesDelegate<FlipFlopBoolean, bool> Input1InvertedChanged;

        [PersistAs("input1_inverted")]
        private bool input1inverted;

        public bool Input1Inverted
        {
            get => input1inverted;
            set => this.setProperty(ref input1inverted, value, Input1InvertedChanged);
        }
        #endregion

        #region Property: Input2
        public event PropertyChangedTwoValuesDelegate<FlipFlopBoolean, IBoolean> Input2Changed;

        [PersistAs("input2")]
        private IBoolean input2;

        public IBoolean Input2
        {
            get => input2;
            set => this.setProperty(ref input2, value, Input2Changed, null, (_, _) => recalculateState(2));
        }
        #endregion

        #region Property: Input2Inverted
        public event PropertyChangedTwoValuesDelegate<FlipFlopBoolean, bool> Input2InvertedChanged;

        [PersistAs("input2_inverted")]
        private bool input2inverted;

        public bool Input2Inverted
        {
            get => input2inverted;
            set => this.setProperty(ref input2inverted, value, Input2InvertedChanged);
        }
        #endregion

        #region Property: EnableSetByUser
        public event PropertyChangedTwoValuesDelegate<FlipFlopBoolean, bool> EnableSetByUserChanged;

        [PersistAs("enable_set_by_user")]
        private bool enableSetByUser;

        public bool EnableSetByUser
        {
            get => enableSetByUser;
            set => this.setProperty(ref enableSetByUser, value, EnableSetByUserChanged);
        }
        #endregion

        #region Property: EnableResetByUser
        public event PropertyChangedTwoValuesDelegate<FlipFlopBoolean, bool> EnableResetByUserChanged;

        [PersistAs("enable_reset_by_user")]
        private bool enableResetByUser;

        public bool EnableResetByUser
        {
            get => enableResetByUser;
            set => this.setProperty(ref enableResetByUser, value, EnableResetByUserChanged);
        }
        #endregion

        #region Property: EnableToggleByUser
        public event PropertyChangedTwoValuesDelegate<FlipFlopBoolean, bool> EnableToggleByUserChanged;

        [PersistAs("enable_toggle_by_user")]
        private bool enableToggleByUser;

        public bool EnableToggleByUser
        {
            get => enableToggleByUser;
            set => this.setProperty(ref enableToggleByUser, value, EnableToggleByUserChanged);
        }
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
                input1?.CurrentState ?? false, input1inverted, (edge == 1),
                Input2?.CurrentState ?? false, input2inverted, (edge == 2));
        }
        #endregion

    }

}
