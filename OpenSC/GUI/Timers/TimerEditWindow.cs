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

namespace OpenSC.GUI.Timers
{
    [WindowTypeName("timers.timereditwindow")]
    public partial class TimerEditWindow : ChildWindowWithTitle
    {

        private const string TITLE_NEW = "New timer";
        private const string TITLE_EDIT = "Edit timer: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New timer";
        private const string HEADER_TEXT_EDIT = "Edit timer";

        private Model.Timers.Timer timer;
        bool addingNew = false;

        public TimerEditWindow(Model.Timers.Timer timer)
        {

            InitializeComponent();

            if (timer == null)
            {
                this.timer = new Model.Timers.Timer();
                addingNew = true;
                HeaderText = HEADER_TEXT_NEW;
                Text = string.Format(TITLE_NEW, 0, "");
            }
            else
            {
                this.timer = timer;
                HeaderText = HEADER_TEXT_EDIT;
                Text = string.Format(TITLE_EDIT, timer.ID, timer.Title);
            }

        }

        public TimerEditWindow()
        { }

        private void TimerEditWindow_Load(object sender, EventArgs e)
        {
            loadTimer();
        }

        private void loadTimer()
        {
            idNumericField.Value = timer.ID;
            titleTextBox.Text = timer.Title;
            modeForwardsRadio.Checked = (timer.Mode == Model.Timers.TimerMode.Forwards);
            modeBackwardsRadio.Checked = (timer.Mode == Model.Timers.TimerMode.Backwards);
            modeClockRadio.Checked = (timer.Mode == Model.Timers.TimerMode.Clock);
            countdownStartNumericField.Value = timer.CountdownSeconds;
        }

        private bool saveTimer()
        {

            try
            {
                timer.ValidateId((int)idNumericField.Value);
                timer.ValidateTitle(titleTextBox.Text);
                timer.ValidateCountdownSeconds((int)countdownStartNumericField.Value);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            timer.ID = (int)idNumericField.Value;
            timer.Title = titleTextBox.Text;
            timer.Mode = getMode();
            timer.CountdownSeconds = (int)countdownStartNumericField.Value;

            if (addingNew)
            {
                TimerDatabase.Instance.Add(timer);
                addingNew = false;
                HeaderText = HEADER_TEXT_EDIT;
                
            }

            Text = string.Format(TITLE_EDIT, timer.ID, timer.Title);

            return true;

        }

        private Model.Timers.TimerMode getMode()
        {
            if (modeForwardsRadio.Checked)
                return Model.Timers.TimerMode.Forwards;
            if (modeBackwardsRadio.Checked)
                return Model.Timers.TimerMode.Backwards;
            return Model.Timers.TimerMode.Clock;
        }

        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            if (saveTimer())
                Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveTimer();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
