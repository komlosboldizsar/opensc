using OpenSC.Library.TaskSchedulerQueue;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace OpenSC.Library.BmdVideohub
{

    public class VideohubClient
    {

        public VideohubClient(string ipAddress)
        {
            requestScheduler = new(sendRequest, invalidRequest);
            createInterpreters();
            this.ipAddress = ipAddress;
        }

        #region IP address
        private string ipAddress;

        public string IpAddress
        {
            get => ipAddress;
            set
            {
                if (value == ipAddress)
                    return;
                bool wasConnected = connected;
                if (wasConnected)
                    Disconnect();
                ipAddress = value;
                if (wasConnected)
                    Connect();
            }
        }
        #endregion

        #region Videohub model properties
        private int inputCount;

        public int InputCount
        {
            get => inputCount;
            internal set
            {
                if (value == inputCount)
                    return;
                inputCount = value;
                InputCountChanged?.Invoke(inputCount);
            }
        }

        public delegate void InputCountChangedDelegate(int inputCount);
        public event InputCountChangedDelegate InputCountChanged;

        private int outputCount;

        public int OutputCount
        {
            get => outputCount;
            internal set
            {
                if (value == outputCount)
                    return;
                outputCount = value;
                crosspoints = new int?[outputCount];
                locks = new LockState?[outputCount];
                OutputCountChanged?.Invoke(outputCount);
            }
        }

        public delegate void OutputCountChangedDelegate(int outputCount);
        public event OutputCountChangedDelegate OutputCountChanged;

        private string modelName;

        public string ModelName
        {
            get => modelName;
            internal set
            {
                if (value == modelName)
                    return;
                modelName = value;
                ModelNameChanged?.Invoke(modelName);
            }
        }

        public delegate void ModelNameChangedDelegate(string modelName);
        public event ModelNameChangedDelegate ModelNameChanged;
        #endregion

        #region Connecting, disconnecting, connection state
        private bool connected;

        public bool Connected
        {
            get => connected;
            internal set
            {
                if (value == connected)
                    return;
                connected = value;
                if (value)
                    requestScheduler.Start();
                else
                    requestScheduler.Stop();
                ConnectionStateChanged?.Invoke(connected);
            }
        }

        public delegate void ConnectionStateChangedDelegate(bool state);
        public event ConnectionStateChangedDelegate ConnectionStateChanged;

        public void Connect()
        {
            createReceiver();
            socketReceiver.Connect();
        }

        public void Disconnect() => socketReceiver?.Disconnect();

        public class NotConnectedException : Exception
        {
            public NotConnectedException()  { }
            public NotConnectedException(string message) : base(message) { }
            public NotConnectedException(string message, Exception innerException) : base(message, innerException) { }
        }
        #endregion

        #region Crosspoints
        private int?[] crosspoints = null;

        internal void NotifyCrosspointChanged(Crosspoint crosspoint)
        {
            if ((crosspoint.Output == null) || (crosspoint.Output < 0) || (crosspoint.Output >= outputCount))
                return;
            if ((crosspoint.Input < 0) || (crosspoint.Input >= inputCount))
                crosspoint = crosspoint with { Input = null };
            crosspoints[(int)crosspoint.Output] = crosspoint.Input;
            CrosspointChanged?.Invoke(crosspoint);
        }

        public delegate void CrosspointChangedDelegate(Crosspoint crosspoint);
        public event CrosspointChangedDelegate CrosspointChanged;

        public int? GetCrosspoint(int output)
        {
            if ((output < 0) || (output >= OutputCount))
                throw new ArgumentOutOfRangeException();
            return crosspoints[output];
        }

        public void SetCrosspoint(int output, int input) => SetCrosspoint(new Crosspoint(output, input));

        public void SetCrosspoint(Crosspoint crosspoint)
        {
            checkCrosspointBeforeSet(crosspoint);
            scheduleRequest(new VideoOutputRoutingRequest(crosspoint));
        }

        public void SetCrosspoints(IEnumerable<Crosspoint> crosspoints)
        {
            foreach (Crosspoint crosspoint in crosspoints)
                checkCrosspointBeforeSet(crosspoint);
            scheduleRequest(new VideoOutputRoutingRequest(crosspoints));
        }

        private void checkCrosspointBeforeSet(Crosspoint crosspoint)
        {
            if ((crosspoint.Output == null) || (crosspoint.Output < 0) || (crosspoint.Output >= OutputCount))
                throw new ArgumentOutOfRangeException();
            if ((crosspoint.Input == null) || (crosspoint.Input < 0) || (crosspoint.Input >= InputCount))
                throw new ArgumentOutOfRangeException();
        }

        public void QueryAllCrosspoints() => scheduleRequest(new VideoOutputRoutingRequest(Array.Empty<Crosspoint>()));
        #endregion

        #region Locks
        private LockState?[] locks = null;

        internal void NotifyLockStateChanged(LockStateData lockStateData)
        {
            if ((lockStateData.Output == null) || (lockStateData.Output < 0) || (lockStateData.Output >= outputCount))
                return;
            locks[(int)lockStateData.Output] = lockStateData.State;
            LockChanged?.Invoke(lockStateData);
        }

        public delegate void LockChangedDelegate(LockStateData lockStateData);
        public event LockChangedDelegate LockChanged;

        public LockState? GetLockState(int output)
        {
            if ((output < 0) || (output >= OutputCount))
                throw new ArgumentOutOfRangeException();
            return locks[output];
        }

        public void DoLockOperation(int output, LockOperation operation) => DoLockOperation(new LockOperationData(output, operation));

        public void DoLockOperation(LockOperationData lockOperationData)
        {
            checkLockOperationDataBeforeDo(lockOperationData);
            scheduleRequest(new VideoOutputLocksRequest(lockOperationData));
        }

        public void DoLockOperations(IEnumerable<LockOperationData> lockOperationDatas)
        {
            foreach (LockOperationData lockOperationData in lockOperationDatas)
                checkLockOperationDataBeforeDo(lockOperationData);
            scheduleRequest(new VideoOutputLocksRequest(lockOperationDatas));
        }

        private void checkLockOperationDataBeforeDo(LockOperationData lockOperationData)
        {
            if ((lockOperationData.Output == null) || (lockOperationData.Output < 0) || (lockOperationData.Output >= OutputCount))
                throw new ArgumentOutOfRangeException();
        }

        public void QueryAllLockStates() => scheduleRequest(new VideoOutputLocksRequest(Array.Empty<LockOperationData>()));
        #endregion

        #region Request scheduler
        private readonly TaskQueue<Request, bool> requestScheduler;
        private void sendRequest(Request request) => request.Send(this);
        private void invalidRequest(Request request) => Debug.WriteLine($"Dropped an invalid request for BMD Videohub [{IpAddress}]");
        private void scheduleRequest(Request request) => requestScheduler.Enqueue(request);
        internal void AckLastRequest() => requestScheduler.LastDequeuedTaskReady(true);
        internal void NakLastRequest() => requestScheduler.LastDequeuedTaskReady(false);
        #endregion

        #region TCP receiver/sender socket
        private const int BMD_VIDEOHUB_PROTOCOL_TCP_PORT = 9990;

        private TcpSocketLineByLineReceiver socketReceiver = null;

        private void createReceiver()
        {
            if (socketReceiver != null)
                disposeReceiver();
            socketReceiver = new TcpSocketLineByLineReceiver(ipAddress, BMD_VIDEOHUB_PROTOCOL_TCP_PORT);
            socketReceiver.LineReceived += lineReceivedOnTcpSocket;
            socketReceiver.ConnectedStateChanged += connectionStateOfTcpSocketChanged;
        }

        private void disposeReceiver()
        {
            socketReceiver.Dispose();
            socketReceiver = null;
        }
        private void connectionStateOfTcpSocketChanged(bool state) => Connected = state;
        #endregion

        #region Protocol implementation
        private IMessageInterpreter currentInterpreter = null;
        private IMessageInterpreter[] knownInterpeters = null;

        private void createInterpreters()
        {
            knownInterpeters = new IMessageInterpreter[]
            {
                new AckNotificationInterpreter(this),
                new NakNotificationInterpreter(this),
                new ProtocolPreambleNotificationInterpreter(this),
                new VideohubDeviceNotificationInterpreter(this),
                new VideoOutputRoutingNotificationInterpreter(this),
                new VideoOutputLocksStateNotificationInterpreter(this),
                new AnyMessageInterpreter()
            };
        }

        private void lineReceivedOnTcpSocket(string line)
        {
            if (line == string.Empty)
            {
                if (currentInterpreter != null)
                {
                    currentInterpreter.BlockEnd();
                    currentInterpreter = null;
                }
                return;
            }
            if (currentInterpreter == null)
            {
                currentInterpreter = knownInterpeters.FirstOrDefault(mi => mi.CanInterpret(line));
                return;
            }
            if (currentInterpreter != null)
            {
                try
                {
                    currentInterpreter.InterpretLine(line);
                } 
                catch (MessageInterpreterException) { }
            }
        }

        internal void SendBlock(string header, IEnumerable<string> lines)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine(header + ":");
            foreach (string line in lines)
                stringBuilder.AppendLine(line);
            stringBuilder.AppendLine();
            socketReceiver.Send(stringBuilder.ToString());
        }
        #endregion

    }

}
