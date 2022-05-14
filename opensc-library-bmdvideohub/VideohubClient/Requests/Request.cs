using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Library.BmdVideohub
{
    internal abstract class Request
    {

        public Request() => ValidUntil = DateTime.Now + ValidTime;

        private VideohubClient videohubClient;
        public void Send(VideohubClient videohubClient)
        {
            this.videohubClient = videohubClient;
            _send();
        }
        protected abstract void _send();

        protected void sendBlock(string header, IEnumerable<string> lines) => videohubClient.SendBlock(header, lines);

        public void ACK()
        {
            AckOrNakReceived.SetResult(true);
            _ack();
        }
        protected virtual void _ack() { }

        public void NAK()
        {
            AckOrNakReceived.SetResult(false);
            _nak();
        }
        protected virtual void _nak() { }

        public virtual TimeSpan ValidTime { get; } = new(0, 0, 2);
        public DateTime ValidUntil { get; init; }

        public readonly TaskCompletionSource<bool> AckOrNakReceived = new();

    }
}
