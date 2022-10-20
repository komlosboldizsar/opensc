using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Library.BmdVideohub
{
    internal class VideoOutputRoutingRequest : Request
    {
        private List<Crosspoint> crosspoints;
        public VideoOutputRoutingRequest(Crosspoint crosspoint) => this.crosspoints = new() { crosspoint };
        public VideoOutputRoutingRequest(IEnumerable<Crosspoint> crosspoints) => this.crosspoints = new(crosspoints);
        protected override void _send()
            => sendBlock(ProtocolStrings.BLOCK__VIDEO_OUTPUT_ROUTING, crosspoints.Select(cp => cp.ToProtocolStr()));
    }
}
