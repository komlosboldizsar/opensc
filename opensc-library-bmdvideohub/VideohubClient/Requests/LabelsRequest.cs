using System.Collections.Generic;
using System.Linq;

namespace OpenSC.Library.BmdVideohub
{
    internal abstract class LabelsRequest : Request
    {
        protected abstract string Header { get; }
        private List<Label> labels;
        public LabelsRequest(Label label) => this.labels = new() { label };
        public LabelsRequest(IEnumerable<Label> labels) => this.labels = new(labels);
        protected override void _send() => sendBlock(Header, labels.Select(l => l.ToProtocolStr()));
    }
}
