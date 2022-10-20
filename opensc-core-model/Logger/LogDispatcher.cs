using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Logger
{

    public class LogDispatcher
    {

        public static void Log(LogMessageType messageType, string tag, string message, params string[] args)
        {
            try
            {
                DateTime timestamp = DateTime.Now;
                message = string.Format(message, args);
                foreach (ReceiverSubscription subscription in Subscriptions)
                    if ((int)messageType >= (int)subscription.MinimumLevel)
                        try
                        {
                            subscription.Receiver.ReceiveLogMessage(messageType, timestamp, tag, message);
                        }
                        catch { }
            }
            catch { }
        }

        public static void V(string tag, string message, params string[] args)
            => Log(LogMessageType.Verbose, tag, message, args);

        public static void I(string tag, string message, params string[] args)
            => Log(LogMessageType.Info, tag, message, args);

        public static void W(string tag, string message, params string[] args)
            => Log(LogMessageType.Warn, tag, message, args);

        public static void E(string tag, string message, params string[] args)
            => Log(LogMessageType.Error, tag, message, args);

        private static List<ReceiverSubscription> Subscriptions = new List<ReceiverSubscription>();

        public static void Subscribe(ILogReceiver receiver, LogMessageType minimumLevel)
        {
            Subscriptions.Add(new ReceiverSubscription()
            {
                Receiver = receiver,
                MinimumLevel = minimumLevel
            });
        }

        public static void Unsubscribe(ILogReceiver receiver)
        {
            Subscriptions.RemoveAll(subscription => (subscription.Receiver == receiver));
        }

        private class ReceiverSubscription
        {
            public ILogReceiver Receiver;
            public LogMessageType MinimumLevel;
        }

    }

}
