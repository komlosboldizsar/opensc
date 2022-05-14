using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Library.BmdVideohub
{
    internal class VideoOutputLocksRequest : Request
    {
        private List<LockOperationData> lockOperationDatas;
        public VideoOutputLocksRequest(LockOperationData lockOperationData) => this.lockOperationDatas = new() { lockOperationData };
        public VideoOutputLocksRequest(IEnumerable<LockOperationData> lockOperationsDatas) => this.lockOperationDatas = new(lockOperationsDatas);
        protected override void _send()
            => sendBlock(ProtocolStrings.BLOCK__VIDEO_OUTPUT_LOCKS, lockOperationDatas.Select(cp => cp.ToProtocolStr()));
    }
}
