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

        public CasparCgPlayoutEditorForm() : base() => InitializeComponent();

        public CasparCgPlayoutEditorForm(Vtr vtr) : base(vtr)
        {
            InitializeComponent();
            if ((vtr != null) && !(vtr is CasparCgPlayout))
                throw new ArgumentException($"Type of VTR should be {nameof(CasparCgPlayout)}.", nameof(vtr));
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Vtr, CasparCgPlayout>(this, VtrDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            CasparCgPlayout casparCgPlayout = (CasparCgPlayout)EditedModel;
            if (casparCgPlayout == null)
                return;
            ipAddressInput.Text = casparCgPlayout.ListenedIp;
            channelNumericField.Value = casparCgPlayout.WatchedChannel;
            layerNumericField.Value = casparCgPlayout.WatchedLayer;
        }

        protected override void writeFields()
        {
            base.writeFields();
            CasparCgPlayout casparCgPlayout = (CasparCgPlayout)EditedModel;
            if (casparCgPlayout == null)
                return;
            casparCgPlayout.ListenedIp = ipAddressInput.Text;
            casparCgPlayout.WatchedChannel = (int)channelNumericField.Value;
            casparCgPlayout.WatchedLayer = (int)layerNumericField.Value;
        }

        protected override void validateFields()
        {
            base.validateFields();
            CasparCgPlayout casparCgPlayout = (CasparCgPlayout)EditedModel;
            if (casparCgPlayout == null)
                return;
        }

    }

}
