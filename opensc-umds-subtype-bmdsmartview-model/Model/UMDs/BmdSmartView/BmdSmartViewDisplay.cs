using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs.BmdSmartView
{
    [TypeLabel("BMD SmartView Display")]
    [TypeCode("bmdsmartview")]
    public class BmdSmartViewDisplay : Umd
    {

        #region Instantiation, restoration, persistence, removation
        public BmdSmartViewDisplay()
        {
            orderTalliesByPriority();
        }

        public override void RestoredOwnFields()
        {
            orderTalliesByPriority();
            base.RestoredOwnFields();
        }
        #endregion

        #region Property: Unit
        public event PropertyChangedTwoValuesDelegate<BmdSmartViewDisplay, BmdSmartViewUnit> UnitChanged;

        [PersistAs("unit")]
        private BmdSmartViewUnit unit;

#pragma warning disable CS0169
        [TempForeignKey(nameof(unit))]
        private string _unitId;
#pragma warning restore CS0169

        public BmdSmartViewUnit Unit
        {
            get => unit;
            set => this.setProperty(ref unit, value, UnitChanged);
        }
        #endregion

        #region Property: Position
        public event PropertyChangedTwoValuesDelegate<BmdSmartViewDisplay, BmdSmartViewDisplayPosition> PositionChanged;

        [PersistAs("position")]
        private BmdSmartViewDisplayPosition position = BmdSmartViewDisplayPosition.Single;

        public BmdSmartViewDisplayPosition Position
        {
            get => position;
            set => this.setProperty(ref position, value, PositionChanged);
        }
        #endregion

        #region Before & after update
        protected override void afterUpdate()
        {
            orderTalliesByPriority();
            base.afterUpdate();
        }
        #endregion

        #region Info
        public override UmdTextInfo[] TextInfo => Array.Empty<UmdTextInfo>();

        public override UmdTallyInfo[] TallyInfo => new UmdTallyInfo[]
        {
            new BmdSmartViewDisplayTallyInfo("Red", UmdTallyInfo.ColorSettingMode.Fix, Color.Red, 1, "red"),
            new BmdSmartViewDisplayTallyInfo("Green", UmdTallyInfo.ColorSettingMode.Fix, Color.Green, 2, "green"),
            new BmdSmartViewDisplayTallyInfo("Blue", UmdTallyInfo.ColorSettingMode.Fix, Color.Blue, 3, "blue"),
            new BmdSmartViewDisplayTallyInfo("White", UmdTallyInfo.ColorSettingMode.Fix, Color.White, 4, "white")
        };
        #endregion

        #region Tallies
        internal void NotifyTallyPriorityChanged(BmdSmartViewDisplayTally tally)
        {
            if (!Updating)
                orderTalliesByPriority();
        }

        private void orderTalliesByPriority()
            => talliesByPriority = Tallies.Cast<BmdSmartViewDisplayTally>().OrderBy(t => t.Priority).ToArray();

        private BmdSmartViewDisplayTally[] talliesByPriority = null;
        #endregion

        #region Sending data to hardware
        protected override void calculateTextFields() { }

        protected override void calculateTallyFields()
        {
            BmdSmartViewDisplayTally firstActiveTally = talliesByPriority.FirstOrDefault(t => t.CurrentState);
            if (firstActiveTally == null)
            {
                commandBorderValue = COMMAND_BORDER_VALUE_NONE;
                return;
            }
            commandBorderValue = ((BmdSmartViewDisplayTallyInfo)firstActiveTally.Info).ProtocolCommandBorderValue;
        }
        
        private const string COMMAND_BORDER_VALUE_NONE = "none";
        private string commandBorderValue = COMMAND_BORDER_VALUE_NONE;

        protected override void sendTextsToHardware() { }
        protected override void sendTalliesToHardware() => sendData();
        protected override void sendEverythingToHardware() => sendData();

        private const string COMMAND_BORDER = "Border";

        protected virtual Dictionary<string, string> getCommandsToSend()
            => new()
            {
                { COMMAND_BORDER, commandBorderValue }
            };

        private static char getLetterByPosition(BmdSmartViewDisplayPosition position) => position switch
        {
            BmdSmartViewDisplayPosition.Single => 'A',
            BmdSmartViewDisplayPosition.DualLeft => 'A',
            BmdSmartViewDisplayPosition.DualRight => 'B',
            _ => 'X'
        };

        private void sendData()
            => unit?.SendDisplayCommands(getLetterByPosition(position), getCommandsToSend());
        #endregion

    }

}
