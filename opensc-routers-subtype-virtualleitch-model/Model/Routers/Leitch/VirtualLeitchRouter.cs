﻿using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SerialPorts;
using OpenSC.Model.Settings;
using OpenSC.Model.SourceGenerators;
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
    public partial class VirtualLeitchRouter : Router
    {

        private new const string LOG_TAG = "Router/VirtualLeitch";

        public VirtualLeitchRouter()
        {
            State = RouterState.Warning;
            StateString = "initializing";
            Outputs.ItemsAdded += outputAdded;
            Outputs.ItemsRemoved += outputRemoved;
        }

        private void Outputs_ItemsAdded()
        {
            throw new NotImplementedException();
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            State = RouterState.Ok;
            StateString = "OK";
        }

        public override void Removed()
        {
            base.Removed();
            if (port != null)
                port.ReceivedDataAsciiString -= receivedLineFromPort;
        }

        private void outputAdded(IEnumerable<IObservableEnumerable<RouterOutput>.ItemWithPosition> addedOutputsData)
        {
            foreach (IObservableEnumerable<RouterOutput>.ItemWithPosition outputData in addedOutputsData)
            {
                RouterOutput output = outputData.Item;
                output.CurrentInputChanged += outputsCurrentInputChanged;
                sendOutputFullStatusReport(output);
            }
        }

        private void outputRemoved(IEnumerable<IObservableEnumerable<RouterOutput>.ItemWithPosition> removedOutputsData)
        {
            foreach (IObservableEnumerable<RouterOutput>.ItemWithPosition outputData in removedOutputsData)
                outputData.Item.CurrentInputChanged -= outputsCurrentInputChanged;
        }

        private void outputsCurrentInputChanged(RouterOutput output, RouterInput newInput)
            => sendOutputInputReport(output);

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
                oldValue.InitializedChanged -= portInitializedChanged;
            }
        }

        private void _port_afterChange(SerialPort oldValue, SerialPort newValue)
        {
            if (newValue != null)
            {
                newValue.ReceivedDataAsciiLine += receivedLineFromPort;
                newValue.InitializedChanged += portInitializedChanged;
                if (newValue.Initialized)
                    reportEverything();
            }
        }

        private void portInitializedChanged(SerialPort port, bool oldState, bool newState)
        {
            if (newState)
                reportEverything();
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

        #region Input and output instantiation
        public override RouterInput CreateInput(string name, int index) => new RouterInput(name, this, index);
        public override RouterOutput CreateOutput(string name, int index) => new VirtualLeitchRouterOutput(name, this, index);

        private static readonly Dictionary<Type, string> OUTPUT_TYPES = new Dictionary<Type, string>()
        {
            {  typeof(VirtualLeitchRouterOutput), "virtual_leitch" }
        };

        protected override Dictionary<Type, string> OutputTypesDictionaryGetter() => OUTPUT_TYPES;
        #endregion

        #region Setting/getting crosspoints
        protected override void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input)
            => output.AssignSource(input);

        protected override void requestCrosspointUpdatesImpl(IEnumerable<RouterCrosspoint> crosspoints)
        {
            foreach (RouterCrosspoint crosspoint in crosspoints)
                crosspoint.Output.AssignSource(crosspoint.Input);
        }

        protected override void queryAllStates()
        { }

        protected override void requestLockOperationImpl(RouterOutput output, RouterOutputLockType lockType, RouterOutputLockOperationType lockOperationType)
        {
            VirtualLeitchRouterOutput outputCasted = output as VirtualLeitchRouterOutput;
            if (outputCasted == null)
                return;
            outputCasted.SetLock(lockType, lockOperationType);
        }
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
            if (line.Length < 2)
                return;
            string details = line[2..];
            switch (line[..2])
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
                case "Q:":
                    handleCustomMessage(details);
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
            VirtualLeitchRouterOutput outputCasted = output as VirtualLeitchRouterOutput;
            RouterInput presetInput = outputCasted.PresetInput;
            if (presetInput == null)
                return;
            sendSerialMessage("@ S:{0:X}{1:X},{2:X}", level, output.Index, presetInput.Index);
        }

        // W!<level><destination>,<id>,<status>
        private void sendOutputLockReport(RouterOutput output)
        {
            VirtualLeitchRouterOutput outputCasted = output as VirtualLeitchRouterOutput;
            int lockState = 0;
            if (outputCasted.Lock.State != RouterOutputLockState.Clear)
                lockState = 1;
            else if (outputCasted.Protect.State != RouterOutputLockState.Clear)
                lockState = 2;
            int panelId = outputCasted.LockProtectOwnerPanelId;
            if (panelId == -1)
                panelId = FORCE_UNLOCK_PANEL_ID;
            sendSerialMessage("@ W!{0:X}{1:X},{2:X},{3}", level, output.Index, panelId, lockState);
        }

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
            if (!detailsHelper1(details, out int[,] crosspoints, out int id))
                return;
            int pairs = crosspoints.GetLength(0);
            for (int p = 0; p < pairs; p++)
            {
                VirtualLeitchRouterOutput output = Outputs.FirstOrDefault(o => (o.Index == crosspoints[p, 0])) as VirtualLeitchRouterOutput;
                RouterInput input = Inputs.FirstOrDefault(i => (i.Index == crosspoints[p, 1]));
                if ((output != null) && (input != null))
                    output.AssignInput(input, id);
            }
        }

        // P:<levels>/<destination>,<source>[/<destination>,<source][:I<id>]
        // response: none
        private void handlePresetCrosspointMessage(string details)
        {
            if (!detailsHelper1(details, out int[,] crosspoints, out int id))
                return;
            int pairs = crosspoints.GetLength(0);
            for (int p = 0; p < pairs; p++)
            {
                VirtualLeitchRouterOutput output = Outputs.FirstOrDefault(o => (o.Index == crosspoints[p, 0])) as VirtualLeitchRouterOutput;
                RouterInput input = Inputs.FirstOrDefault(i => (i.Index == crosspoints[p, 1]));
                if ((output != null) && (input != null))
                    output.AssignPreset(input, id);
            }
        }

        private bool detailsHelper1(string details, out int[,] crosspoints, out int id)
        {
            crosspoints = null;
            id = -1;
            string[] split1 = details.Split(':');
            try
            {
                if (split1.Length > 1)
                    id = int.Parse(split1[1][1..], System.Globalization.NumberStyles.HexNumber);
            }
            catch { }
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
            foreach (RouterOutput output in Outputs)
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
                if ((status < 0) || (status > 2))
                    return;
                VirtualLeitchRouterOutput output = Outputs.FirstOrDefault(o => (o.Index == destination)) as VirtualLeitchRouterOutput;
                if (output == null)
                    return;
                output.SetLock(status, id);
                sendOutputLockReport(output);
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

        // Q:<custom_command>
        // no response
        private void handleCustomMessage(string details)
        {
            LogDispatcher.I(LOG_TAG, $"Custom Leitch command ('Q:') received: [{details}]");
            CustomCommandReceived?.Invoke(this, details);
        }

        public const int FORCE_UNLOCK_PANEL_ID = 65535;
        #endregion

        #region Presets
        private void clearPresets()
            => Outputs.Foreach(ro => (ro as VirtualLeitchRouterOutput).ClearPreset());

        private void executePresets()
            => Outputs.Foreach(ro => (ro as VirtualLeitchRouterOutput).ExecutePreset());
        #endregion

        #region Custom command receiving
        public delegate void CustomCommandReceivedHandler(VirtualLeitchRouter router, string command);
        public event CustomCommandReceivedHandler CustomCommandReceived;
        #endregion

        #region Settings
        public static readonly IntSetting PanelIdSetting = new IntSetting(
            "routers.virtualleitch.panelid",
            "Routers",
            "Virtual Leitch panel ID",
            "Used to track ownership of locks.",
            1,
            1,
            1024
        );
        #endregion

    }

}
