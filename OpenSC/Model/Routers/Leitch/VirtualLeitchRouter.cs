using OpenSC.Logger;
using OpenSC.Model.Persistence;
using OpenSC.Model.SerialPorts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Leitch
{

    [TypeLabel("Virtual Leitch")]
    [TypeCode("virtual_leitch")]
    public class VirtualLeitchRouter : Router
    {

        private new const string LOG_TAG = "Router/VirtualLeitch";

        public VirtualLeitchRouter()
        {
            Outputs.ItemAdded += outputAdded;
            Outputs.ItemRemoved += outputRemoved;
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            if (port != null)
                port.ReceivedDataAsciiLine += receivedLineFromPort;
            RouterInput firstInput = Inputs.FirstOrDefault();
            foreach (RouterOutput output in Outputs)
                output.AssignSource(firstInput); // TODO: store last crosspoint
        }

        public override void Removed()
        {
            base.Removed();
            if (port != null)
                port.ReceivedDataAsciiString -= receivedLineFromPort;
        }

        private void outputAdded(IEnumerable addedOutputs)
        {
            foreach (RouterOutput output in addedOutputs)
            {
                output.CurrentInputChanged += outputsCurrentInputChanged;
                sendOutputFullStatusReport(output);
            }
        }

        private void outputRemoved(IEnumerable removedOutputs)
        {
            foreach (RouterOutput output in removedOutputs)
                output.CurrentInputChanged -= outputsCurrentInputChanged;
        }

        private void outputsCurrentInputChanged(RouterOutput output, RouterInput newInput)
            => sendOutputInputReport(output);

        #region Property: Port
        public delegate void PortChangedDelegate(VirtualLeitchRouter router, SerialPort oldPort, SerialPort newPort);
        public event PortChangedDelegate PortChanged;

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
                if (value == port)
                    return;
                SerialPort oldValue = port;
                if (port != null)
                    port.ReceivedDataAsciiString -= receivedLineFromPort;
                port = value;
                if (port != null)
                    port.ReceivedDataAsciiString += receivedLineFromPort;
                PortChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(Port));
            }
        }

        private void portInitializedChanged(SerialPort port, bool oldState, bool newState)
        {
            if (newState)
                reportEverything();
        }
        #endregion

        #region Property: Level
        public delegate void LevelChangedDelegate(VirtualLeitchRouter router, int oldLevel, int newLevel);
        public event LevelChangedDelegate LevelChanged;

        [PersistAs("level")]
        private int level;

        public int Level
        {
            get => level;
            set
            {
                if (value == level)
                    return;
                int oldValue = level;
                if ((value < 0) || (value > 9))
                    throw new ArgumentOutOfRangeException();
                level = value;
                LevelChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(Level));
            }
        }

        private readonly char[] HEX_CHARS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        private char LevelHex => HEX_CHARS[level];
        #endregion

        #region Input and output instantiation
        public override RouterInput CreateInput(string name, int index) => new RouterInput(name, this, index);
        public override RouterOutput CreateOutput(string name, int index) => new RouterOutput(name, this, index);
        #endregion

        #region Setting/getting crosspoints
        protected override void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input)
            => output.AssignSource(input);

        protected override void queryAllCrosspoints()
        { }
        #endregion

        #region Serial communication
        private void sendSerialMessage(string commandFormat, params object[] parameters)
        {
            if (port == null)
                return;
            string commandToSend = string.Format(commandFormat, parameters) + "\r\n";
            byte[] commandBytesToSend = Encoding.ASCII.GetBytes(commandToSend);
            DateTime validUntil = DateTime.Now + TimeSpan.FromSeconds(2);
            port.SendData(commandBytesToSend, validUntil);
        }

        private void receivedLineFromPort(SerialPort port, string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return;
            if ((line.Length < 2) || (line[1] != ':'))
                return;
            string details = line.Substring(2);
            switch (line.Substring(0, 2))
            {
                case "z:":
                    handleResetLevelsMessage(details);
                    break;
                case "X:":
                    handleDirectCrosspointTakeMessage(details);
                    break;
                case "P:":
                    handlePresetCrosspointMessage(details);
                    break;
                case "S?":
                    handleRequestCrosspointStatusOfEntireLevelMessage(details);
                    break;
                case "X?":
                    handleRequestCrosspointStatusOfSingleDestinationMessage(details);
                    break;
                case "P?":
                    handleRequestPresetCrosspointStatusMessage(details);
                    break;
                case "V?":
                    handleRequestPresetCrosspointStatusOnLevelMessage(details);
                    break;
                case "W:":
                    handleLockProtectMessage(details);
                    break;
                case "B:":
                    handleBufferMessage(details);
                    break;
            }
        }
        #endregion

        #region Protocol implementation, receiving
        private void handleStatusUpdate(string statusString)
        {
            try
            {
                int levelIndex = int.Parse(statusString[2].ToString());
                if (levelIndex != level)
                    return;
                string destinationSourceString = statusString.Substring(3);
                string[] destinationSourceStringParts = destinationSourceString.Split(',');
                int sourceIndex = int.Parse(destinationSourceStringParts[0], System.Globalization.NumberStyles.HexNumber);
                int destinationIndex = int.Parse(destinationSourceStringParts[1], System.Globalization.NumberStyles.HexNumber);
                notifyCrosspointChanged(destinationIndex, sourceIndex);
            }
            catch
            {
                string logMessage = string.Format("Got an invalid (unprocessable) Leitch status update message. Router: #{0}, port: #{1}. Status message: [{2}]",
                    ID, port?.ID, statusString);
                LogDispatcher.E(LOG_TAG, logMessage);
            }
        }

        // S:<level><destination>,<source>
        private void sendOutputInputReport(RouterOutput output)
            => sendSerialMessage("@ S:{0:X}{1:X},{2:X}", level, output.Index, output.CurrentInput?.Index ?? 0);

        // V:<level><destination>,<source>
        private void sendOutputPresetReport(RouterOutput output)
        {
            if (!presets.TryGetValue(output, out RouterInput presetInput))
                return;
            if (presetInput == null)
                return;
            sendSerialMessage("@ S:{0:X}{1:X},{2:X}", level, output.Index, presetInput.Index);
        }

        // W!<level><destination>,<id>,<status>
        private void sendOutputLockReport(RouterOutput output)
            => sendSerialMessage("@ W!{0:X}{1:X},0,0", level, output.Index); // TODO: handle locks

        private void sendOutputFullStatusReport(RouterOutput output)
        {
            sendOutputInputReport(output);
            sendOutputLockReport(output);
        }

        private void reportEverything()
        {
            foreach (RouterOutput output in Outputs)
                sendOutputFullStatusReport(output);
        }

        // Z:<level>
        // response: none (?)
        private void handleResetLevelsMessage(string details)
        {
            if (details.IndexOf(LevelHex) == -1)
                return;
            RouterInput firstInput = Inputs.FirstOrDefault();
            if (firstInput == null)
                return;
            foreach (RouterOutput output in Outputs)
                output.AssignSource(firstInput);
        }

        // X:<levels>/<destination>,<source>[/<destination>,<source][:I<id>]
        // response: S:<level><destination>,<source> (many lines)
        private void handleDirectCrosspointTakeMessage(string details)
        {
            if (!detailsHelper1(details, out int[,] crosspoints, out string id))
                return;
            int pairs = crosspoints.GetLength(0);
            for (int p = 0; p < pairs; p++)
            {
                RouterOutput output = Outputs.FirstOrDefault(o => (o.Index == crosspoints[p, 0]));
                RouterInput input = Inputs.FirstOrDefault(i => (i.Index == crosspoints[p, 1]));
                if ((output != null) && (input != null))
                    output.AssignSource(input);
            }
        }

        // P:<levels>/<destination>,<source>[/<destination>,<source][:I<id>]
        // response: none
        private void handlePresetCrosspointMessage(string details)
        {
            if (!detailsHelper1(details, out int[,] crosspoints, out string id))
                return;
            int pairs = crosspoints.GetLength(0);
            for (int p = 0; p < pairs; p++)
            {
                RouterOutput output = Outputs.FirstOrDefault(o => (o.Index == crosspoints[p, 0]));
                RouterInput input = Inputs.FirstOrDefault(i => (i.Index == crosspoints[p, 1]));
                if ((output != null) && (input != null))
                    presets[output] = input;
            }
        }

        private bool detailsHelper1(string details, out int[,] crosspoints, out string id)
        {
            crosspoints = null;
            id = null;
            string[] split1 = details.Split(':');
            if (split1.Length > 1)
                id = split1[1];
            string[] split2 = split1[0].Split('/');
            if (split2.Length < 2)
                return false;
            if (split2[0].IndexOf(LevelHex) == -1)
                return false;
            crosspoints = new int[split2.Length - 1, 2];
            try
            {
                for (int i = 1; i < split2.Length; i++)
                {
                    string[] split3 = split2[i].Split(',');
                    if (split3.Length != 2)
                        return false;
                    for (int j = 0; j < 2; j++)
                        crosspoints[i - 1, j] = int.Parse(split3[j], System.Globalization.NumberStyles.HexNumber);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // S?<level>
        // response: S:<level><destination>,<source> (many lines)
        private void handleRequestCrosspointStatusOfEntireLevelMessage(string details)
        {
            if (details.IndexOf(LevelHex) == -1)
                return;
            foreach (RouterOutput output in Outputs)
                sendOutputInputReport(output);
        }

        // X?<level><destination>
        // response: S:<level><destination>,<source>
        private void handleRequestCrosspointStatusOfSingleDestinationMessage(string details)
        {
            if (!detailsHelper2(details, out int destination))
                return;
            RouterOutput output = Outputs.FirstOrDefault(o => (o.Index == destination));
            if (output == null)
                return;
            sendOutputInputReport(output);
        }

        // P?<level><destination>
        // response: V:<level><destination>,<source>
        private void handleRequestPresetCrosspointStatusMessage(string details)
        {
            if (!detailsHelper2(details, out int destination))
                return;
            RouterOutput output = Outputs.FirstOrDefault(o => (o.Index == destination));
            if (output == null)
                return;
            sendOutputPresetReport(output);
        }

        private bool detailsHelper2(string details, out int destination)
        {
            destination = -1;
            if (details.Length < 2)
                return false;
            try
            {
                int level = int.Parse(details.Substring(0, 1), System.Globalization.NumberStyles.HexNumber);
                if (level != this.level)
                    return false;
                destination = int.Parse(details.Substring(1), System.Globalization.NumberStyles.HexNumber);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // V?<level>
        // response: V:<level><destination>,<source> (many lines)
        private void handleRequestPresetCrosspointStatusOnLevelMessage(string details)
        {
            if (details.IndexOf(LevelHex) == -1)
                return;
            foreach (RouterOutput output in presets.Keys)
                sendOutputPresetReport(output);
        }

        // W:<levels>/<destination>,<id>,<status>
        // response: W!<level><destination>,<id>,<status>
        private void handleLockProtectMessage(string details)
        {
            string[] split = details.Split('/', ',');
            if (split.Length != 4)
                return;
            if (split[0].IndexOf(LevelHex) == -1)
                return;
            try
            {
                int destination = int.Parse(split[1], System.Globalization.NumberStyles.HexNumber);
                int id = int.Parse(split[2], System.Globalization.NumberStyles.HexNumber);
                int status = int.Parse(split[3], System.Globalization.NumberStyles.HexNumber);
                RouterOutput output = Outputs.FirstOrDefault(o => (o.Index == destination));
                if (output == null)
                    return;
                // TODO: handle locks
                // TODO: sendOutputLockReport
                sendSerialMessage("@ W!{0:X}/{1:X},{2:X},0", level, destination, id);
            }
            catch { }
        }

        // B:E
        // reponse: S:<level><destination>,<source> (many lines)
        private void handleBufferMessage(string details)
        {
            switch (details)
            {
                case "C":
                    clearPresets();
                    break;
                case "E":
                    executePresets();
                    break;
                case "R":
                    clearPresets();
                    break;
            }
        }
        #endregion

        #region Presets
        private Dictionary<RouterOutput, RouterInput> presets = new Dictionary<RouterOutput, RouterInput>();

        private void clearPresets() => presets.Clear();

        private void executePresets()
        {
            foreach (KeyValuePair<RouterOutput, RouterInput> preset in presets)
                preset.Key.AssignSource(preset.Value);
            presets.Clear();
        }
        #endregion

    }

}
