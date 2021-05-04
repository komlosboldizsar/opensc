using BMDSwitcherAPI;
using System;
using ThreadHelpers;

namespace BMD.Switcher
{

    public class AuxOutput : IBMDSwitcherInputAuxCallback, IDisposable
    {

        public Switcher ParentSwitcher { get; private set; }

        public IBMDSwitcherInputAux ApiAuxOutput { get; private set; }

        public int Index { get; private set; }

        public AuxOutput(Switcher parentSwitcher, IBMDSwitcherInputAux apiAuxOutput, int index)
        {
            ParentSwitcher = parentSwitcher;
            ApiAuxOutput = apiAuxOutput;
            ApiAuxOutput.AddCallback(this);
            Index = index;
        }

        private void initSource()
        {
            InvokeHelper.Invoke(() =>
            {
                ApiAuxOutput.GetInputSource(out long sourceId);
                SourceId = sourceId;
            });
        }

        void IBMDSwitcherInputAuxCallback.Notify(_BMDSwitcherInputAuxEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherInputAuxEventType.bmdSwitcherInputAuxEventTypeInputSourceChanged:
                    InvokeHelper.Invoke(() =>
                    {
                        ApiAuxOutput.GetInputSource(out long sourceId);
                        SourceId = sourceId;
                    });
                    break;
            }

        }

        public void Dispose()
        {
            ApiAuxOutput.RemoveCallback(this);
            SourceChanged = null;
        }

        #region Source
        public delegate void SourceChangedDelegate(IBMDSwitcherInputAux apiAuxOutput, AuxOutput auxOutput, long sourceId, Source source);
        public event SourceChangedDelegate SourceChanged;

        private long sourceId;

        public long SourceId
        {
            get => sourceId;
            private set
            {
                if (value == sourceId)
                    return;
                sourceId = value;
                SourceChanged?.Invoke(ApiAuxOutput, this, value, ParentSwitcher.GetSource(value));
            }
        }

        public Source Source
            => ParentSwitcher.GetSource(sourceId);

        public void RequestSourceChange(long sourceId)
        {
            ApiAuxOutput.SetInputSource(sourceId);
        }

        public void RequestSourceChange(Source source)
            => RequestSourceChange(source.ID);
        #endregion

    }

}
