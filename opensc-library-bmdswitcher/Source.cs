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
            initNames();
            initTallies();
        }

        void IBMDSwitcherInputCallback.Notify(_BMDSwitcherInputEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeShortNameChanged:
                    InvokeHelper.Invoke(() =>
                    {
                        ApiSource.GetShortName(out string _shortName);
                        ShortName = _shortName;
                    });
                    break;
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeLongNameChanged:
                    InvokeHelper.Invoke(() =>
                    {
                        ApiSource.GetShortName(out string _longName);
                        LongName = _longName;
                    });
                    break;
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
            ShortNameChanged = null;
            LongNameChanged = null;
            IsProgramTalliedChanged = null;
            IsPreviewTalliedChanged = null;
        }

        #region Names
        private void initNames()
        {
            InvokeHelper.Invoke(() =>
            {
                ApiSource.GetShortName(out string _shortName);
                ShortName = _shortName;
                ApiSource.GetLongName(out string _longName);
                LongName = _longName;
            });
        }

        public delegate void NameChangedDelegate(Source source, string newName);

        #region Short name
        public event NameChangedDelegate ShortNameChanged;

        private string shortName;
        public string ShortName
        {
            get => shortName;
            private set
            {
                shortName = value;
                ShortNameChanged?.Invoke(this, value);
            }
        }

        public void UpdateShortName(string newShortName)
            => InvokeHelper.Invoke(() => ApiSource.SetShortName(newShortName));
        #endregion

        #region Long name
        public event NameChangedDelegate LongNameChanged;

        private string longName;
        public string LongName
        {
            get => longName;
            private set
            {
                longName = value;
                LongNameChanged?.Invoke(this, value);
            }
        }

        public void UpdateLongName(string newLongName)
            => InvokeHelper.Invoke(() => ApiSource.SetLongName(newLongName));
        #endregion
        #endregion

        #region Tallies
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
        #endregion

    }

}
