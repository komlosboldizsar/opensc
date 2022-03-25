using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Timers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = OpenSC.Model.Timers.Timer;

namespace OpenSC.GUI.Timers
{

    public partial class TimerEditorForm : ModelEditorFormBase, IModelEditorForm<Timer>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Timer);
        public IModelEditorForm<Timer> GetInstanceT(Timer modelInstance) => new TimerEditorForm(modelInstance);

        public TimerEditorForm() : base() => InitializeComponent();
        public TimerEditorForm(Timer timer) : base(timer) => InitializeComponent();

        protected override IModelEditorFormDataManager createManager()
           => new ModelEditorFormDataManager<Timer, Timer>(this, TimerDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            Timer timer = (Timer)EditedModel;
            if (timer == null)
                return;
            modeForwardsRadio.Checked = (timer.Mode == TimerMode.Forwards);
            modeBackwardsRadio.Checked = (timer.Mode == TimerMode.Backwards);
            modeClockRadio.Checked = (timer.Mode == TimerMode.Clock);
            countdownStartNumericField.Value = timer.CountdownSeconds;
        }

        protected override void validateFields()
        {
            base.validateFields();
            Timer timer = (Timer)EditedModel;
            if (timer == null)
                return;
            timer.ValidateCountdownSeconds((int)countdownStartNumericField.Value);
        }

        protected override void writeFields()
        {
            base.writeFields();
            Timer timer = (Timer)EditedModel;
            if (timer == null)
                return;
            timer.Mode = getMode();
            timer.CountdownSeconds = (int)countdownStartNumericField.Value;
        }

        private TimerMode getMode()
        {
            if (modeForwardsRadio.Checked)
                return TimerMode.Forwards;
            if (modeBackwardsRadio.Checked)
                return TimerMode.Backwards;
            return TimerMode.Clock;
        }

    }

}
