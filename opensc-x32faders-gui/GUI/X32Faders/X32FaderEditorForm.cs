using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model;
using OpenSC.Model.X32Faders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenSC.GUI.X32Faders
{

    public partial class X32FaderEditorForm : ModelEditorFormBase, IModelEditorForm<X32Fader>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as X32Fader);
        public IModelEditorForm<X32Fader> GetInstanceT(X32Fader modelInstance) => new X32FaderEditorForm(modelInstance);

        public X32FaderEditorForm() : base() => InitializeComponent();
        public X32FaderEditorForm(X32Fader x32fader) : base(x32fader) => InitializeComponent();

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<X32Fader, X32Fader>(this, X32FaderDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            X32Fader x32fader = (X32Fader)EditedModel;
            if (x32fader == null)
                return;
            oscPathTextBox.Text = x32fader.OscPath;
            targetLevelNumericField.Value = x32fader.TargetLevel;
            referenceLevelNumericField.Value = x32fader.ReferenceLevelForTime;
            referenceLevelNumericField.Enabled = x32fader.UseReferenceLevelForTime;
            bindTimeToReferenceCheckBox.Checked = x32fader.UseReferenceLevelForTime;
            timeNumericField.Value = x32fader.Time;
            stepTimeNumericField.Value = x32fader.TimeStep;
        }

        protected override void validateFields()
        {
            base.validateFields();
            X32Fader x32fader = (X32Fader)EditedModel;
            if (x32fader == null)
                return;
            x32fader.OscPath = oscPathTextBox.Text;
            x32fader.TargetLevel = targetLevelNumericField.Value;
            x32fader.ReferenceLevelForTime = referenceLevelNumericField.Value;
            x32fader.UseReferenceLevelForTime = bindTimeToReferenceCheckBox.Checked;
            x32fader.Time = (int)timeNumericField.Value;
            x32fader.TimeStep = (int)stepTimeNumericField.Value;
        }

        protected override void writeFields()
        {
            base.writeFields();
            X32Fader x32fader = (X32Fader)EditedModel;
            if (x32fader == null)
                return;
            x32fader.ValidateTargetLevel(targetLevelNumericField.Value);
            x32fader.ValidateReferenceLevelForTime(referenceLevelNumericField.Value);
            x32fader.ValidateTime((int)timeNumericField.Value);
            x32fader.ValidateTimeStep((int)stepTimeNumericField.Value);
        }

        private void bindTimeToReferenceCheckBox_CheckedChanged(object sender, EventArgs e)
            => referenceLevelNumericField.Enabled = bindTimeToReferenceCheckBox.Checked;

    }

}
