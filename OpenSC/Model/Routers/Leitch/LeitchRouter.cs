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

        public override void Removed()
        {
            base.Removed();
            if (port != null)
                port.ReceivedDataAsciiString -= receivedDataFromPort;
        }

        #region Property: Port
        private SerialPort port;
        
        public SerialPort Port
        {
            get => port;
            set
            {
                if (value == port)
                    return;
                if(port != null)
                    port.ReceivedDataAsciiString -= receivedDataFromPort;
                port = value;
                if (port != null)
                    port.ReceivedDataAsciiString += receivedDataFromPort;
            }
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

        private void receivedDataFromPort(SerialPort port, string asciiString)
        {
            string[] lines = asciiString.Split(new char[] { '\r', '\n' });
            foreach (string line in lines)
                if (line != string.Empty)
                    receivedLineFromPort(line);
        }

        private void receivedLineFromPort(string line)
        {
            if ((line.Length < 2) || (line[1] != ':'))
                return;
            switch (line[0])
            {
                case 'S':
                    handleStatusUpdate(line);
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
        #endregion

    }

}
