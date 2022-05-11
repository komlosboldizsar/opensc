using Bespoke.Osc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSC.Model.X32Faders
{
    internal class X32FaderCommons
    {

        private const int SOURCE_PORT = 10024;
        private const int DESTINATION_PORT = 10023;

        public static async Task<float> QueryCurrentLevel(string path, string ip, int port = DESTINATION_PORT)
        {
            TaskCompletionSource<float> request = addRequest(path, ip, port);   
            sendQueryCurrentLevel(path, ip, port);
            return await request.Task;
        }

        private static void sendQueryCurrentLevel(string path, string ip, int port)
        {
            IPEndPoint sourceEndPoint = new IPEndPoint(IPAddress.Any, SOURCE_PORT);
            OscMessage message = new OscMessage(sourceEndPoint, path);
            IPAddress destinationIp = IPAddress.Parse(ip);
            IPEndPoint destinationEndPoint = new IPEndPoint(destinationIp, port);
            byte[] msgBytes = message.ToByteArray();
            udpConnection.Send(msgBytes, msgBytes.Length, destinationEndPoint);
        }

        public static void SetLevel(string path, float level, string ip, int port = DESTINATION_PORT)
        {
            IPEndPoint sourceEndPoint = new IPEndPoint(IPAddress.Any, SOURCE_PORT);
            OscMessage message = new OscMessage(sourceEndPoint, path);
            message.Append(level);
            IPAddress destinationIp = IPAddress.Parse(ip);
            IPEndPoint destinationEndPoint = new IPEndPoint(destinationIp, port);
            byte[] msgBytes = message.ToByteArray();
            udpConnection.Send(msgBytes, msgBytes.Length, destinationEndPoint);
        }

        private static Dictionary<string, List<TaskCompletionSource<float>>> requests = new();

        private static string requestDictionaryKey(string path, string ip, int port) => $"{ip}:{port}#{path}";

        private static List<TaskCompletionSource<float>> getRequestList(string path, string ip, int port)
        {
            string _requestDictionaryKey = requestDictionaryKey(path, ip, port);
            if (!requests.TryGetValue(_requestDictionaryKey, out List<TaskCompletionSource<float>> requestList))
            {
                requestList = new();
                requests.Add(_requestDictionaryKey, requestList);
            }
            return requestList;
        }

        private static TaskCompletionSource<float> getNextRequest(string path, string ip, int port)
        {
            if (!requests.TryGetValue(requestDictionaryKey(path, ip, port), out List<TaskCompletionSource<float>> requestList))
                return null;
            TaskCompletionSource<float> nextRequest = requestList.FirstOrDefault();
            if (nextRequest != null)
                requestList.RemoveAt(0);
            return nextRequest;
        }

        private static TaskCompletionSource<float> addRequest(string path, string ip, int port)
        {
            List<TaskCompletionSource<float>> requestList = getRequestList(path, ip, port);
            TaskCompletionSource<float> request = new();
            requestList.Add(request);
            return request;
        }

        private static UdpClient udpConnection;

        public static void Init() => Task.Run(_init);

        private static async Task _init()
        {
            if (udpConnection != null)
                return;
            udpConnection = new UdpClient(SOURCE_PORT);
            while (true)
            {
                UdpReceiveResult result = await udpConnection.ReceiveAsync();
                OscPacket packet = OscPacket.FromByteArray(result.RemoteEndPoint, result.Buffer);
                if (!packet.IsBundle && (packet is OscMessage message))
                    oscMessageReceived(message);
            }
        }

        private static void oscMessageReceived(OscMessage message)
        {
            try
            {
                string sourceIP = message.SourceEndPoint.Address.ToString();
                int sourcePort = message.SourceEndPoint.Port;
                getNextRequest(message.Address, sourceIP, sourcePort)?.SetResult(message.At<float>(0));
            }
            catch { }
        }


    }
}
