using BMDSwitcherAPI;
using System;
using ThreadHelpers;

namespace BMD.Switcher
{

    public class Source : IBMDSwitcherInputCallback, IDisposable
    {

        public Switcher ParentSwitcher { get; private set; }

        public IBMDSwitcherInput ApiSource { get; private set; }

        public long ID { get; private set; }

        public Source(Switcher parentSwitcher, IBMDSwitcherInput apiSource)
        {
            ParentSwitcher = parentSwitcher;
            ApiSource = apiSource;
            ApiSource.AddCallback(this);
            ID = ApiSource.GetSourceId();
            initTallies();
        }

        private void initTallies()
        {
            InvokeHelper.Invoke(() =>
            {
                ApiSource.IsProgramTallied(out int isProgramTallied);
                IsProgramTallied = (isProgramTallied != 0);
                ApiSource.IsPreviewTallied(out int isPreviewTallied);
                IsPreviewTallied = (isPreviewTallied != 0);
            });
        }

        void IBMDSwitcherInputCallback.Notify(_BMDSwitcherInputEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeIsProgramTalliedChanged:
                    InvokeHelper.Invoke(() =>
                    {
                        ApiSource.IsProgramTallied(out int isProgramTallied);
                        IsProgramTallied = (isProgramTallied != 0);
                    });
                    break;
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeIsPreviewTalliedChanged:
                    InvokeHelper.Invoke(() =>
                    {
                        ApiSource.IsPreviewTallied(out int isPreviewTallied);
                        IsPreviewTallied = (isPreviewTallied != 0);
                    });
                    break;
            }

        }

        public void Dispose()
        {
            ApiSource.RemoveCallback(this);
            IsProgramTalliedChanged = null;
            IsPreviewTalliedChanged = null;
        }

        #region Program tally
        public delegate void IsProgramTalliedChangedDelegate(IBMDSwitcherInput apiSource, Source source, bool isTallied);
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
                IsProgramTalliedChanged?.Invoke(ApiSource, this, value);
            }
        }
        #endregion

        #region Preview tally
        public delegate void IsPreviewTalliedChangedDelegate(IBMDSwitcherInput apiSource, Source source, bool isTallied);
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
                IsPreviewTalliedChanged?.Invoke(ApiSource, this, value);
            }
        }
        #endregion

    }

}
