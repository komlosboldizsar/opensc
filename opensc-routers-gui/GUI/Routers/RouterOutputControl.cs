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
        }

        public RouterOutputControl(RouterOutput output, RouterControlForm containerForm) : this()
        {
            this.Output = output;
            this.containerForm = containerForm;
            updateName();
            updateLabel();
            updateLockButton();
            updateProtectButton();
        }

        private void RouterOutputControl_Load(object sender, EventArgs e)
        {
            if (Output != null)
            {
                Output.RegisteredSourceSignalNameChanged += sourceSignalNameChangedHandler;
                Output.CurrentInputChanged += currentInputChangedHandler;
                Output.NameChanged += nameChangedHandler;
                Output.LockStateChanged += lockStateChangedHandler;
                Output.LockOwnerChanged += lockOwnerChangedHandler;
                Output.ProtectStateChanged += protectStateChangedHandler;
                Output.ProtectOwnerChanged += protectOwnerChangedHandler;
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
        private void lockStateChangedHandler(RouterOutput item, RouterOutputLockState oldValue, RouterOutputLockState newValue) => updateLockButton();
        private void lockOwnerChangedHandler(RouterOutput item, RouterOutputLockOwner oldValue, RouterOutputLockOwner newValue) => updateLockButtonTooltip();
        private void protectStateChangedHandler(RouterOutput item, RouterOutputLockState oldValue, RouterOutputLockState newValue) => updateProtectButton();
        private void protectOwnerChangedHandler(RouterOutput item, RouterOutputLockOwner oldValue, RouterOutputLockOwner newValue) => updateProtectButtonTooltip();

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
            label.Text = $"{Output.CurrentInput.Name}\r\n({Output.RegisteredSourceSignalName ?? "-"}";
        }

        #region Update buttons
        private void updateButton(Button button, bool supported, RouterOutputLockState state)
        {
            //button.Enabled = supported;
            //button.Visible = supported;
            Color backColor = Color.LightGray;
            if (supported)
                backColor = (state == RouterOutputLockState.Clear) ? Color.LightGreen : Color.LightPink;
            button.BackColor = backColor;
        }

        private void updateLockButton()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => updateLockButton()));
                return;
            }
            updateButton(lockButton, Output.LocksSupported, Output.LockState);
            updateLockButtonTooltip();
        }

        private void updateProtectButton()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => updateProtectButton()));
                return;
            }
            updateButton(protectButton, Output.ProtectsSupported, Output.ProtectState);
            updateProtectButtonTooltip();
        }

        private string getLockProtectStateString(RouterOutputLockState state, RouterOutputLockOwnerKnowLevel knowLevel, RouterOutputLockOwner owner, string operationString, string inStateString)
        {
            string outputIdentifier = Output.ToString();
            string fullStateString;
            if (Output.LocksSupported)
            {
                string stateString;
                stateString = state switch
                {
                    RouterOutputLockState.Clear => $"not {inStateString}",
                    RouterOutputLockState.Locked => $"{inStateString}",
                    RouterOutputLockState.LockedLocal => $"{inStateString} locally (by this application)",
                    RouterOutputLockState.LockedRemote => $"{inStateString} remotely (by another user)",
                    _ => $"{operationString} by unknown"
                };
                if ((knowLevel == RouterOutputLockOwnerKnowLevel.Detailed) && (owner != null))
                    stateString = $"{inStateString} by {owner}";
                fullStateString = $"Output {outputIdentifier} is {stateString}.";
            }
            else
            {
                fullStateString = $"{operationString} output {outputIdentifier} is not supported.";
            }
            return fullStateString;
        }
        #endregion

        #region Tooltips
        private void updateLockProtectButtonTooltip(Button button, RouterOutputLockState state, RouterOutputLockOwnerKnowLevel knowLevel, RouterOutputLockOwner owner, string operationString, string inStateString)
        {
            string fullStateString = getLockProtectStateString(state, knowLevel, owner, operationString, inStateString);
            toolTip.SetToolTip(button, fullStateString);
        }

        private void updateLockButtonTooltip()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => updateLockButtonTooltip()));
                return;
            }
            updateLockProtectButtonTooltip(lockButton, Output.LockState, Output.LockOwnerKnowLevel, Output.LockOwner, "Locking", "locked");
        }

        private void updateProtectButtonTooltip()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => updateLockButtonTooltip()));
                return;
            }
            updateLockProtectButtonTooltip(protectButton, Output.ProtectState, Output.ProtectOwnerKnowLevel, Output.ProtectOwner, "Protecting", "protected");
        }
        #endregion

        #region Button clicks
        private void forceUnoperateWithPrompt(RouterOutputLockType type, RouterOutputLockState state, RouterOutputLockOwnerKnowLevel knowLevel, RouterOutputLockOwner owner, string operationString, string inStateString, string unoperateString, string unoperateStringCap)
        {
            string promptString = getLockProtectStateString(state, knowLevel, owner, operationString, inStateString);
            promptString += $"\r\nDo you want to force {unoperateString}?";
            DialogResult promptResult = MessageBox.Show(promptString, $"{unoperateStringCap} output", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (promptResult == DialogResult.OK)
            {
                switch (type)
                {
                    case RouterOutputLockType.Lock:
                        Output.RequestForceUnlock();
                        break;
                    case RouterOutputLockType.Protect:
                        Output.RequestForceUnprotect();
                        break;
                }
            }
        }

        private void handleLockProtectButtonClick(bool supported, RouterOutputLockType type, RouterOutputLockState state, RouterOutputLockOwnerKnowLevel knowLevel, RouterOutputLockOwner owner, string operationString, string inStateString, string unoperateString, string unoperateStringCap)
        {
            if (!supported)
                return;
            if (state == RouterOutputLockState.Clear)
            {
                switch (type)
                {
                    case RouterOutputLockType.Lock:
                        Output.RequestLock();
                        break;
                    case RouterOutputLockType.Protect:
                        Output.RequestProtect();
                        break;
                }
            }
            else if (state == RouterOutputLockState.LockedRemote)
            {
                forceUnoperateWithPrompt(type, state, knowLevel, owner, operationString, inStateString, unoperateString, unoperateStringCap);
            }
            else
            {
                switch (type)
                {
                    case RouterOutputLockType.Lock:
                        Output.RequestUnlock();
                        break;
                    case RouterOutputLockType.Protect:
                        Output.RequestUnprotect();
                        break;
                }
            }
        }

        private void protectButton_Click(object sender, EventArgs e)
            => handleLockProtectButtonClick(Output.LocksSupported, RouterOutputLockType.Lock, Output.LockState, Output.LockOwnerKnowLevel, Output.LockOwner, "Locking", "locked", "unlock", "Unlock");

        private void lockButton_Click(object sender, EventArgs e)
            => handleLockProtectButtonClick(Output.ProtectsSupported, RouterOutputLockType.Protect, Output.ProtectState, Output.ProtectOwnerKnowLevel, Output.ProtectOwner, "Protecting", "protected", "unprotect", "Unprotect");
        #endregion

        #region Button right clicks
        private void lockButton_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (Output.LockState != RouterOutputLockState.Clear) && Output.LocksSupported)
                forceUnoperateWithPrompt(RouterOutputLockType.Lock, Output.LockState, Output.LockOwnerKnowLevel, Output.LockOwner, "Locking", "locked", "unlock", "Unlock");
        }

        private void protectButton_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (Output.ProtectState != RouterOutputLockState.Clear) && Output.ProtectsSupported)
                forceUnoperateWithPrompt(RouterOutputLockType.Protect, Output.ProtectState, Output.ProtectOwnerKnowLevel, Output.ProtectOwner, "Protecting", "protected", "unprotect", "Unprotect");
        }
        #endregion

    }

}
