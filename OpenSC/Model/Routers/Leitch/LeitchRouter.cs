using OpenSC.Logger;
using OpenSC.Model.Persistence;
using OpenSC.Model.SerialPorts;
using OpenSC.Model.Settings;
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
    public class LeitchRouter : Router
    {

        private new const string LOG_TAG = "Router/Leitch";

        public LeitchRouter()
        { }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            if (port != null)
            {
                port.ReceivedDataAsciiLine += receivedLineFromPort;
                port.InitializedChanged += portInitializedChangedHandler;
                if (port.Initialized)
                    initSerial();
            }
        }

        public override void Removed()
        {
            base.Removed();
            if (port != null)
                port.ReceivedDataAsciiString -= receivedLineFromPort;
        }

        #region Property: Port
        PropertyChangedTwoValuesDelegate<LeitchRouter, SerialPort> PortChanged;
        
        [PersistAs("port")]
        private SerialPort port;

#pragma warning disable CS0169
        [TempForeignKey(SerialPortDatabase.DBNAME, nameof(port))]
        private int _portId;
#pragma warning restore CS0169

        public SerialPort Port
        {
            get => port;
            set
            {
                BeforeChangePropertyDelegate<SerialPort> beforeChangeDelegate = (ov, nv) => {
                    ov.ReceivedDataAsciiLine -= receivedLineFromPort;
                    ov.InitializedChanged -= portInitializedChangedHandler;
                };
                AfterChangePropertyDelegate<SerialPort> afterChangeDelegate = (ov, nv) => {
                    port.ReceivedDataAsciiString += receivedLineFromPort;
                };
                setProperty(this, ref port, value, PortChanged, beforeChangeDelegate, afterChangeDelegate);
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
        public event PropertyChangedTwoValuesDelegate<LeitchRouter, int> LevelChanged;

        private int level;

        public int Level
        {
            get => level;
            set
            {
                ValidateLevel(level);
                setProperty(this, ref level, value, LevelChanged);
            }
        }

        public void ValidateLevel(int level)
        {
            if ((level < 0) || (level > 15))
                throw new ArgumentOutOfRangeException();
        }

        private readonly char[] HEX_CHARS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        private char LevelHex => HEX_CHARS[level];
        #endregion

        #region Input and output instantiation
        public override RouterInput CreateInput(string name, int index) => new RouterInput(name, this, index);
        public override RouterOutput CreateOutput(string name, int index) => new LeitchRouterOutput(name, this, index);

        private static readonly Dictionary<Type, string> OUTPUT_TYPES = new Dictionary<Type, string>()
        {
            { typeof(LeitchRouterOutput), "leitch" }
        };

        protected override Dictionary<Type, string> OutputTypesDictionaryGetter() => OUTPUT_TYPES;
        #endregion

        #region Setting/getting crosspoints
        protected override void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input)
            => sendSerialCommand("@ X:{0:X}/{1:X},{2:X}:I{3:X}", level, output.Index, input.Index, PanelIdSetting.Value);

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
                string destinationSourceString = statusString.Substring(3);
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
                LeitchRouterOutput output = GetOutput(destinationIndex) as LeitchRouterOutput;
                if (output == null)
                    return;

                RouterOutputLockState lockState = (panelId == PanelIdSetting.Value) ? RouterOutputLockState.LockedLocal : RouterOutputLockState.LockedRemote;
                
                if (lockOpCode == 1)
                    notifyLockChanged(output, RouterOutputLockType.Lock, lockState);
                else if (output.LockState != RouterOutputLockState.Clear)
                    notifyLockChanged(output, RouterOutputLockType.Lock, RouterOutputLockState.Clear);

                if (lockOpCode == 2)
                    notifyLockChanged(output, RouterOutputLockType.Protect, lockState);
                else if (output.ProtectState != RouterOutputLockState.Clear)
                    notifyLockChanged(output, RouterOutputLockType.Protect, RouterOutputLockState.Clear);

                output.LockProtectOwner = panelId;

            }
            catch
            {
                string logMessage = string.Format("Got an invalid (unprocessable) Leitch status update message. Router: #{0}, port: #{1}. Status message: [{2}]",
                    ID, port?.ID, statusString);
                LogDispatcher.E(LOG_TAG, logMessage);
            }
        }

        private void enableReporting()
        {
            sendSerialCommand("@ ?");
        }
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
