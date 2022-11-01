using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SerialPorts;
using OpenSC.Model.Settings;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Leitch
{

    [TypeLabel("Leitch")]
    [TypeCode("leitch")]
    public partial class LeitchRouter : Router
    {

        private new const string LOG_TAG = "Router/Leitch";

        public LeitchRouter()
        { }

        public override void Removed()
        {
            base.Removed();
            if (port != null)
                port.ReceivedDataAsciiString -= receivedLineFromPort;
        }

        #region Property: Port
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_port_beforeChange))]
        [AutoProperty.AfterChange(nameof(_port_afterChange))]
        [PersistAs("port")]
        private SerialPort port;

        private void _port_beforeChange(SerialPort oldValue, SerialPort newValue, BeforeChangePropertyArgs args)
        {
            if (oldValue != null)
            {
                oldValue.ReceivedDataAsciiLine -= receivedLineFromPort;
                oldValue.InitializedChanged -= portInitializedChangedHandler;
            }
        }

        private void _port_afterChange(SerialPort oldValue, SerialPort newValue)
        {
            if (newValue != null)
            {
                newValue.ReceivedDataAsciiLine += receivedLineFromPort;
                newValue.InitializedChanged += portInitializedChangedHandler;
                initSerial();
            }
        }

        private void portInitializedChangedHandler(SerialPort port, bool oldState, bool newState)
        {
            if (newState)
                initSerial();
        }

        private void initSerial()
        {
            enableReporting();
            queryAllStates();
        }
        #endregion

        #region Property: Level
        [AutoProperty]
        [AutoProperty.Validator(nameof(ValidateLevel))]
        [PersistAs("level")]
        private int level;

        public void ValidateLevel(int level)
        {
            if ((level < 0) || (level > 15))
                throw new ArgumentOutOfRangeException();
        }

        private readonly char[] HEX_CHARS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        private char LevelHex => HEX_CHARS[level];
        #endregion

        #region Inputs and outputs
        protected override RouterOutputCollection createOutputCollection()
            => new LeitchRouterOutputCollection(this );

        protected override Type getOutputCollectionType()
            => typeof(LeitchRouterOutputCollection);
        #endregion

        #region Setting/getting crosspoints
        protected override void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input)
            => sendSerialCommand("@ X:{0:X}/{1:X},{2:X}:I{3:X}", level, output.Index, input.Index, PanelIdSetting.Value);

        protected override void requestCrosspointUpdatesImpl(IEnumerable<RouterCrosspoint> crosspoints)
        {
            StringBuilder crosspointsStrBuilder = new StringBuilder();
            foreach (RouterCrosspoint crosspoint in crosspoints)
                crosspointsStrBuilder.Append(string.Format("/{0:X},{1:X}", crosspoint.Output.Index, crosspoint.Input.Index));
            sendSerialCommand("@ X:{0:X}{1}:I{2:X}", level, crosspointsStrBuilder.ToString(), PanelIdSetting.Value);
        }

        protected override void queryAllStates()
            => sendSerialCommand("@ S?{0:X}", level);

        protected override void requestLockOperationImpl(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType)
        {
            int protocolLockTypeCode = 0;
            if (lockOperationType == RouterOutputLockOperationType.Lock) {
                switch (lockType)
                {
                    case RouterOutputLockType.Lock:
                        protocolLockTypeCode = 1;
                        break;
                    case RouterOutputLockType.Protect:
                        protocolLockTypeCode = 2;
                        break;
                }
            }
            int panelId = (lockOperationType == RouterOutputLockOperationType.ForceUnlock) ? FORCE_UNLOCK_PANEL_ID : PanelIdSetting.Value;
            sendSerialCommand("@ W:{0:X}/{1:X},{2:X},{3}", level, output.Index, panelId, protocolLockTypeCode);
        }

        private const int FORCE_UNLOCK_PANEL_ID = 65535;
        #endregion

        #region Serial communication
        private void sendSerialCommand(string commandFormat, params object[] parameters)
        {
            if (port == null)
                return;
            string commandToSend = string.Format(commandFormat, parameters) + "\r\n";
            byte[] commandBytesToSend = Encoding.ASCII.GetBytes(commandToSend);
            DateTime validUntil = DateTime.Now + TimeSpan.FromSeconds(2);
            port.SendData(commandBytesToSend, validUntil);
        }

        private void receivedLineFromPort(SerialPort port, string asciiLine)
        {
            if (string.IsNullOrWhiteSpace(asciiLine))
                return;
            if (asciiLine.Length < 2)
                return;
            switch (asciiLine[0])
            {
                case 'S':
                    handleStatusUpdate(asciiLine);
                    break;
                case 'W':
                    handleLockUpdate(asciiLine);
                    break;
            }
        }
        #endregion

        #region Protocol implementation, receiving
        private void handleStatusUpdate(string statusString)
        {
            try
            {
                int levelIndex = int.Parse(statusString[2].ToString(), System.Globalization.NumberStyles.HexNumber);
                if (levelIndex != level)
                    return;
                string destinationSourceString = statusString[3..];
                string[] destinationSourceStringParts = destinationSourceString.Split(',');
                int sourceIndex = int.Parse(destinationSourceStringParts[1], System.Globalization.NumberStyles.HexNumber);
                int destinationIndex = int.Parse(destinationSourceStringParts[0], System.Globalization.NumberStyles.HexNumber);
                notifyCrosspointChanged(destinationIndex, sourceIndex);
            }
            catch
            {
                string logMessage = string.Format("Got an invalid (unprocessable) Leitch status update message. Router: #{0}, port: #{1}. Status message: [{2}]",
                    ID, port?.ID, statusString);
                LogDispatcher.E(LOG_TAG, logMessage);
            }
        }

        private void handleLockUpdate(string statusString)
        {
            try
            {

                string[] split = statusString.Split(':', '/', ',', '!');
                if (split.Length != 4)
                    return;
                if (split[1][0] != LevelHex)
                    return;
                int destinationIndex = int.Parse(split[1].Substring(1), System.Globalization.NumberStyles.HexNumber);
                int panelId = int.Parse(split[2], System.Globalization.NumberStyles.HexNumber);
                int lockOpCode = int.Parse(split[3]);
                LeitchRouterOutput output = Outputs[destinationIndex] as LeitchRouterOutput;
                if (output == null)
                    return;

                RouterOutputLockState lockState = (panelId == PanelIdSetting.Value) ? RouterOutputLockState.LockedLocal : RouterOutputLockState.LockedRemote;
                
                if (lockOpCode == 1)
                    notifyLockChanged(output, RouterOutputLockType.Lock, lockState);
                else if (output.Lock.State != RouterOutputLockState.Clear)
                    notifyLockChanged(output, RouterOutputLockType.Lock, RouterOutputLockState.Clear);

                if (lockOpCode == 2)
                    notifyLockChanged(output, RouterOutputLockType.Protect, lockState);
                else if (output.Protect.State != RouterOutputLockState.Clear)
                    notifyLockChanged(output, RouterOutputLockType.Protect, RouterOutputLockState.Clear);

                output.LockProtectOwnerPanelId = panelId;

            }
            catch
            {
                string logMessage = string.Format("Got an invalid (unprocessable) Leitch status update message. Router: #{0}, port: #{1}. Status message: [{2}]",
                    ID, port?.ID, statusString);
                LogDispatcher.E(LOG_TAG, logMessage);
            }
        }

        private void enableReporting() => sendSerialCommand("@ ?");
        #endregion

        #region Settings
        public static readonly IntSetting PanelIdSetting = new IntSetting(
            "routers.leitch.panelid",
            "Routers",
            "Leitch panel ID",
            "Used to track ownership of locks.",
            1,
            1,
            1024
        );
        #endregion

    }

}
