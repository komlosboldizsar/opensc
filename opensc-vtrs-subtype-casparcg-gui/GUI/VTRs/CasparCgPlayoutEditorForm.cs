using OpenSC.Model.VTRs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.VTRs
{

    public partial class CasparCgPlayoutEditorForm : VtrEditorFormBase, IModelEditorForm<Vtr>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Vtr);
        public IModelEditorForm<Vtr> GetInstanceT(Vtr modelInstance) => new CasparCgPlayoutEditorForm(modelInstance);

        public CasparCgPlayoutEditorForm() : base()
        {
            InitializeComponent();
        }

        public CasparCgPlayoutEditorForm(Vtr vtr) : base(vtr)
        {
            InitializeComponent();
            if (vtr == null)
                this.vtr = new CasparCgPlayout();
            else if (!(vtr is CasparCgPlayout))
                throw new ArgumentException();
        }

        protected override void loadData()
        {
            base.loadData();
            CasparCgPlayout casparVtr = vtr as CasparCgPlayout;
            if (casparVtr == null)
                return;
            ipTextBox.Text = casparVtr.ListenedIp;
            channelNumericField.Value = casparVtr.WatchedChannel;
            layerNumericField.Value = casparVtr.WatchedLayer;
        }

        protected override void writeFields()
        {
            base.writeFields();
            CasparCgPlayout casparVtr = vtr as CasparCgPlayout;
            if (casparVtr == null)
                return;
            casparVtr.ListenedIp = ipTextBox.Text;
            casparVtr.WatchedChannel = Convert.ToInt32(channelNumericField.Value);
            casparVtr.WatchedLayer = Convert.ToInt32(layerNumericField.Value);
        }

        protected override void validateFields()
        {
            base.validateFields();
            CasparCgPlayout casparVtr = vtr as CasparCgPlayout;
            if (casparVtr == null)
                return;
            // TODO: Validation
        }

    }

}
