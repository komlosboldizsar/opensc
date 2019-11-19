using BMDSwitcherAPI;
using System;
using ThreadHelpers;

namespace BMD.Switcher
{

    public class InputMonitor : IBMDSwitcherInputCallback, IDisposable
    {

        public IBMDSwitcherInput Input { get; private set; }

        public InputMonitor(IBMDSwitcherInput input)
        {
            Input = input;
            Input.AddCallback(this);
        }

        void IBMDSwitcherInputCallback.Notify(_BMDSwitcherInputEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeIsProgramTalliedChanged:
                    InvokeHelper.Invoke(() =>
                    {
                        Input.IsProgramTallied(out int isProgramTallied);
                        IsProgramTallied = (isProgramTallied != 0);
                    });
                    break;
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeIsPreviewTalliedChanged:
                    InvokeHelper.Invoke(() =>
                    {
                        Input.IsPreviewTallied(out int isPreviewTallied);
                        IsPreviewTallied = (isPreviewTallied != 0);
                    });
                    break;
            }

        }

        public void Dispose()
        {
            Input.RemoveCallback(this);
            IsProgramTalliedChanged = null;
            IsPreviewTalliedChanged = null;
        }

        #region Program tally
        public delegate void IsProgramTalliedChangedDelegate(IBMDSwitcherInput input, InputMonitor monitor, bool isTallied);
        public event IsProgramTalliedChangedDelegate IsProgramTalliedChanged;

        private bool isProgramTallied;

        public bool IsProgramTallied
        {
            get => isProgramTallied;
            set
            {
                if (value == isProgramTallied)
                    return;
                isProgramTallied = value;
                IsProgramTalliedChanged?.Invoke(Input, this, value);
            }
        }
        #endregion

        #region Preview tally
        public delegate void IsPreviewTalliedChangedDelegate(IBMDSwitcherInput input, InputMonitor monitor, bool isTallied);
        public event IsPreviewTalliedChangedDelegate IsPreviewTalliedChanged;

        private bool isPreviewTallied;

        public bool IsPreviewTallied
        {
            get => isPreviewTallied;
            set
            {
                if (value == isPreviewTallied)
                    return;
                isPreviewTallied = value;
                IsPreviewTalliedChanged?.Invoke(Input, this, value);
            }
        }
        #endregion

    }

}
