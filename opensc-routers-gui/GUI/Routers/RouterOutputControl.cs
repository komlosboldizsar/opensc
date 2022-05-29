using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenSC.Model.Routers;
using OpenSC.Model.Signals;

namespace OpenSC.GUI.Routers
{

    public partial class RouterOutputControl : RouterInputOutputControlBase
    {

        private RouterControlForm containerForm;

        public RouterOutput Output { get; private set; }

        private RouterInput _currentInput;
        private RouterInput currentInput
        {
            get => _currentInput;
            set
            {
                if (value == _currentInput)
                    return;
                if (_currentInput != null)
                    _currentInput.NameChanged -= crosspointInputNameChangedHandler;
                _currentInput = value;
                updateLabel();
                if (_currentInput != null)
                    _currentInput.NameChanged += crosspointInputNameChangedHandler;
            }
        }

        public RouterOutputControl()
        {
            InitializeComponent();
            fixLockProtectButtons();
            initLockButtonManagers();
        }

        public RouterOutputControl(RouterOutput output, RouterControlForm containerForm) : this()
        {
            this.Output = output;
            this.containerForm = containerForm;
            updateName();
            updateLabel();
        }

        private void RouterOutputControl_Load(object sender, EventArgs e)
        {
            if (Output != null)
            {
                Output.RegisteredSourceSignalNameChanged += sourceSignalNameChangedHandler;
                Output.CurrentInputChanged += currentInputChangedHandler;
                Output.NameChanged += nameChangedHandler;
                bindLockButtonManagers();
            }
        }

        private void fixLockProtectButtons()
        {
            Color buttonBorderFlatColor = button.FlatAppearance.BorderColor;
            protectButton.FlatAppearance.BorderColor = buttonBorderFlatColor;
            lockButton.FlatAppearance.BorderColor = buttonBorderFlatColor;
            protectButton.Location = new Point(button.Location.X + 2, button.Location.Y + 2);
            lockButton.Location = new Point(button.Location.X + button.Width - lockButton.Width - 2, button.Location.Y + 2);
            protectButton.Height = protectButton.Width;
            lockButton.Height = lockButton.Width;
            button.Padding = new Padding(0, protectButton.Height + 2, 0, 0);
        }

        private void currentInputChangedHandler(RouterOutput output, RouterInput newInput) => currentInput = newInput;
        private void sourceSignalNameChangedHandler(ISignalSource inputSource, string newName, List<object> recursionChain) => updateLabel();
        private void crosspointInputNameChangedHandler(RouterInput input, string oldName, string newName) => updateLabel();
        private void nameChangedHandler(RouterOutput output, string oldName, string newName) => updateName();
        
        private static readonly Color COLOR_BUTTON_BACK_SELECTED = Color.FromArgb(255, 255, 255, 128);
        private static readonly Color COLOR_BUTTON_BACK_NOTSELECTED = Color.FromArgb(255, 255, 255, 192);

        private bool selected;

        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                button.BackColor = selected ? COLOR_BUTTON_BACK_SELECTED : COLOR_BUTTON_BACK_NOTSELECTED;
            }
        }

        private void button_Click(object sender, EventArgs e) => containerForm.OutputClicked(this);

        private void updateName()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => updateName()));
                return;
            }
            button.Text = Output.Name;
        }

        private void updateLabel()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => updateLabel()));
                return;
            }
            if(Output.CurrentInput == null)
            {
                label.Text = "?";
                return;
            }
            label.Text = $"{Output.CurrentInput.Name}\r\n({Output.RegisteredSourceSignalName ?? "-"})";
        }

        private LockButtonManager lockButtonManager;
        private LockButtonManager protectButtonManager;

        private void initLockButtonManagers()
        {
            lockButtonManager = new LockButtonManager(lockButton, toolTip);
            protectButtonManager = new LockButtonManager(protectButton, toolTip);
        }

        private void bindLockButtonManagers()
        {
            lockButtonManager.BindLock(Output.Lock);
            protectButtonManager.BindLock(Output.Protect);
        }

        private class LockButtonManager
        {

            private Button button;
            private ToolTip toolTip;
            private RouterOutputLock @lock;

            public LockButtonManager(Button button, ToolTip toolTip)
            {
                this.button = button;
                this.toolTip = toolTip;
                button.Click += buttonClickHandler;
                button.MouseClick += buttonMouseClickHandler;
            }

            public void BindLock(RouterOutputLock @lock)
            {
                this.@lock = @lock;
                @lock.StateChanged += lockStateChangedHandler;
                @lock.OwnerChanged += lockOwnerChangedHandler;
                updateButton();
                updateTooltip();
            }

            private void lockStateChangedHandler(RouterOutputLock item, RouterOutputLockState oldValue, RouterOutputLockState newValue)
            {
                updateButton();
                updateTooltip();
            }

            private void lockOwnerChangedHandler(RouterOutputLock item, RouterOutputLockOwner oldValue, RouterOutputLockOwner newValue)
                => updateTooltip();

            private void updateButton()
            {
                //button.Enabled = supported;
                //button.Visible = supported;
                Color backColor = Color.LightGray;
                if (@lock.Supported)
                    backColor = (@lock.State == RouterOutputLockState.Clear) ? Color.LightGreen : Color.LightPink;
                button.BackColor = backColor;
            }

            private void updateTooltip()
            {
                if (button.InvokeRequired)
                {
                    button.Invoke(new Action(() => updateTooltip()));
                    return;
                }
                toolTip.SetToolTip(button, @lock.GetStateSentence());
            }

            private void buttonClickHandler(object sender, EventArgs e)
            {
                if (!@lock.Supported)
                    return;
                try
                {
                    if (@lock.State == RouterOutputLockState.Clear)
                        @lock.Do();
                    else if (@lock.State == RouterOutputLockState.LockedRemote)
                        RoutersGuiUtilities.ForceUndoWithPrompt(@lock);
                    else
                        @lock.Undo();
                }
                catch (RouterOutputLockOperationException ex)
                {
                    RoutersGuiUtilities.ShowLockOperationFailedAlert(ex);
                }
            }

            private void buttonMouseClickHandler(object sender, MouseEventArgs e)
            {
                if ((e.Button == MouseButtons.Right) && (@lock.State != RouterOutputLockState.Clear) && @lock.Supported)
                    RoutersGuiUtilities.ForceUndoWithPrompt(@lock);
            }

        }

    }

}
