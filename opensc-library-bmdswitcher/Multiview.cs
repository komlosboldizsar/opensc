using BMD.Switcher.Exceptions;
using BMDSwitcherAPI;
using System;
using ThreadHelpers;

namespace BMD.Switcher
{

    public class Multiview : IBMDSwitcherMultiViewCallback, IDisposable
    {

        public readonly Switcher ParentSwitcher;
        public readonly IBMDSwitcherMultiView ApiMultiview;
        public readonly int Index;

        public Multiview(Switcher parentSwitcher, IBMDSwitcherMultiView apiMultiviewer, int index)
        {
            ParentSwitcher = parentSwitcher;
            ApiMultiview = apiMultiviewer;
            ApiMultiview.AddCallback(this);
            Index = index;
            initData();
        }

        private void initData()
        {
            InvokeHelper.Invoke(() =>
            {
                queryWindowCount();
                queryWindowSourceIds();
            });
        }


        public void Notify(_BMDSwitcherMultiViewEventType eventType, int window)
        {
            switch (eventType)
            {
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeWindowChanged:
                    InvokeHelper.Invoke(() =>
                    {
                        queryWindowSourceId((uint)window);
                    });
                    break;
            }
        }

        public void Dispose()
        {
            ApiMultiview.RemoveCallback(this);
            WindowCountChanged = null;
        }

        #region Window count
        public delegate void WindowCountChangedDelegate(IBMDSwitcherMultiView apiMultiview, Multiview multiview, long sourceId);
        public event WindowCountChangedDelegate WindowCountChanged;
        
        private uint windowCount;

        public uint WindowCount
        {
            get => windowCount;
            private set
            {
                if (value == windowCount)
                    return;
                windowCount = value;
                WindowCountChanged?.Invoke(ApiMultiview, this, value);
            }
        }

        private void queryWindowCount()
        {
            ApiMultiview.GetWindowCount(out uint windowCount);
            WindowCount = windowCount;
            windowSourceIds = new long[WindowCount];
        }
        #endregion

        #region Window source
        public delegate void WindowSourceChangedDelegate(IBMDSwitcherMultiView apiMultiview, Multiview multiview, uint windowIndex, long sourceId);
        public event WindowSourceChangedDelegate WindowSourceChanged;

        private long[] windowSourceIds;

        private void queryWindowSourceIds()
        {
            for (uint windowIndex = 0; windowIndex < windowCount; windowIndex++)
                queryWindowSourceId(windowIndex);
        }

        private void queryWindowSourceId(uint windowIndex)
        {
            ApiMultiview.GetWindowInput(windowIndex, out long windowSourceId);
            updateWindowSourceId(windowIndex, windowSourceId);
        }

        private void updateWindowSourceId(uint windowIndex, long sourceId)
        {
            checkWindowIndex(windowIndex);
            if (sourceId == windowSourceIds[windowIndex])
                return;
            windowSourceIds[windowIndex] = sourceId;
            WindowSourceChanged?.Invoke(ApiMultiview, this, windowIndex, sourceId);
        }

        public long GetWindowSourceId(uint windowIndex)
        {
            checkWindowIndex(windowIndex);
            return windowSourceIds[windowIndex];
        }

        public long[] GetAllWindowSourceIds() => windowSourceIds;

        public void RequestSetWindowSourceId(uint windowIndex, long sourceId)
        {
            checkWindowIndex(windowIndex);
            Source source = ParentSwitcher.GetSource(sourceId);
            if (source == null)
                throw new NotExistingSourceException($"Switcher has no source with ID #{sourceId}!");
            ApiMultiview.SetWindowInput(windowIndex, sourceId);
        }

        private void checkWindowIndex(uint windowIndex)
        {
            if ((windowIndex < 0) || (windowIndex >= windowCount))
                throw new ArgumentException($"Window index must be in [0, {windowCount - 1}[ (window count).", nameof(windowIndex));
        }
        #endregion

    }

}
