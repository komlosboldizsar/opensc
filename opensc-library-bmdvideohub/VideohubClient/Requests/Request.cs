using OpenSC.Library.TaskSchedulerQueue;
using System;
using System.Collections.Generic;

namespace OpenSC.Library.BmdVideohub
{

    internal abstract class Request : QueuedTask<bool>
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

        protected override void _ready(bool result)
        {
            base._ready(result);
            if (result)
                _ack();
            else
                _nak();
        }

        protected virtual void _ack() { }
        protected virtual void _nak() { }

        public virtual TimeSpan ValidTime { get; } = new(0, 0, 2);
        public DateTime ValidUntil { get; init; }
        protected override bool IsValid => (ValidUntil >= DateTime.Now);

    }

}
