using OpenSC.Logger;
using OpenSC.Model.Persistence;
using OpenSC.Model.SerialPorts;
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
                if (port != null)
                {
                    port.ReceivedDataAsciiLine -= receivedLineFromPort;
                    port.InitializedChanged -= portInitializedChangedHandler;
                }
                port = value;
                if (port != null)
                    port.ReceivedDataAsciiString += receivedLineFromPort;
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
            queryAllCrosspoints();
        }
        #endregion

        #region Property: Level
        private int level;

        public int Level
        {
            get => level;
            set
            {
                if ((value < 0) || (value > 9))
                    throw new ArgumentOutOfRangeException();
                level = value;
            }
        }
        #endregion

        #region Input and output instantiation
        public override RouterInput CreateInput(string name, int index) => new RouterInput(name, this, index);
        public override RouterOutput CreateOutput(string name, int index) => new RouterOutput(name, this, index);
        #endregion

        #region Setting/getting crosspoints
        protected override void requestCrosspointUpdateImpl(RouterOutput output, RouterInput input)
        {
            sendSerialCommand("@ X:{0}/{1:X},{2:X}\r\n", level, output.Index, input.Index);
        }

        protected override void queryAllCrosspoints()
        {
            sendSerialCommand("@ S?{0}\r\n", level);
        }
        #endregion

        #region Serial communication
        private void sendSerialCommand(string commandFormat, params object[] parameters)
        {
            if (port == null)
                return;
            string commandToSend = string.Format(commandFormat, parameters);
            byte[] commandBytesToSend = Encoding.ASCII.GetBytes(commandToSend);
            DateTime validUntil = DateTime.Now + TimeSpan.FromSeconds(2);
            port.SendData(commandBytesToSend, validUntil);
        }

        private void receivedLineFromPort(SerialPort port, string asciiLine)
        {
            if (string.IsNullOrWhiteSpace(asciiLine))
                return;
            if ((asciiLine.Length < 2) || (asciiLine[1] != ':'))
                return;
            switch (asciiLine[0])
            {
                case 'S':
                    handleStatusUpdate(asciiLine);
                    break;
            }
        }
        #endregion

        #region Protocol implementation, receiving
        private void handleStatusUpdate(string statusString)
        {
            try
            {
                Console.WriteLine(statusString);
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

        private void enableReporting()
        {
            sendSerialCommand("@ ?\r\n");
        }
        #endregion

    }

}
