using BMD.Switcher.Exceptions;
using BMDSwitcherAPI;
using System;
using ThreadHelpers;

namespace BMD.Switcher
{

    public class MixEffectBlock : IBMDSwitcherMixEffectBlockCallback, IDisposable
    {

        public Switcher ParentSwitcher { get; private set; }

        public IBMDSwitcherMixEffectBlock ApiMixEffectBlock { get; private set; }

        public int Index { get; private set; }

        public MixEffectBlock(Switcher parentSwitcher, IBMDSwitcherMixEffectBlock apiMixEffectBlock, int index)
        {
            ParentSwitcher = parentSwitcher;
            ApiMixEffectBlock = apiMixEffectBlock;
            ApiMixEffectBlock.AddCallback(this);
            Index = index;
        }

        private void initPPsources()
        {
            InvokeHelper.Invoke(() =>
            {
                ApiMixEffectBlock.GetInt(_BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdProgramInput, out long programInput);
                ProgramSourceId = programInput;
                ApiMixEffectBlock.GetInt(_BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewInput, out long previewInput);
                PreviewSourceId = previewInput;
            });
        }

        public void PropertyChanged(_BMDSwitcherMixEffectBlockPropertyId propertyId)
        {
            switch (propertyId)
            {
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdProgramInput:
                    InvokeHelper.Invoke(() =>
                    {
                        ApiMixEffectBlock.GetInt(_BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdProgramInput, out long programInput);
                        ProgramSourceId = programInput;
                    });
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewInput:
                    InvokeHelper.Invoke(() =>
                    {
                        ApiMixEffectBlock.GetInt(_BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewInput, out long previewInput);
                        PreviewSourceId = previewInput;
                    });
                    break;
            }
        }

        public void Dispose()
        {
            ApiMixEffectBlock.RemoveCallback(this);
            ProgramInputChanged = null;
            PreviewInputChanged = null;
        }

        #region Program source
        public delegate void ProgramInputChangedDelegate(IBMDSwitcherMixEffectBlock apiMeBlock, MixEffectBlock meBlock, long sourceId);
        public event ProgramInputChangedDelegate ProgramInputChanged;

        private long programSourceId;

        public long ProgramSourceId
        {
            get => programSourceId;
            private set
            {
                if (value == programSourceId)
                    return;
                programSourceId = value;
                ProgramInputChanged?.Invoke(ApiMixEffectBlock, this, value);
            }
        }

        public void RequestSetProgramSource(long sourceId)
        {
            Source source = ParentSwitcher.GetSource(sourceId);
            if (source == null)
                throw new NotExistingSourceException(string.Format("Switcher has no source with ID #{0}!", sourceId));
            ApiMixEffectBlock.SetInt(_BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdProgramInput, sourceId);
        }

        public void RequestSetProgramSource(Source source)
            => RequestSetProgramSource(source.ID);

        public Source ProgramSource
           => ParentSwitcher.GetSource(programSourceId);
        #endregion

        #region Preview source
        public delegate void PreviewInputChangedDelegate(IBMDSwitcherMixEffectBlock apiMeBlock, MixEffectBlock meBlock, long sourceId);
        public event PreviewInputChangedDelegate PreviewInputChanged;

        private long previewSourceId;

        public long PreviewSourceId
        {
            get => previewSourceId;
            private set
            {
                if (value == previewSourceId)
                    return;
                previewSourceId = value;
                PreviewInputChanged?.Invoke(ApiMixEffectBlock, this, value);
            }
        }

        public Source PreviewSource
            => ParentSwitcher.GetSource(previewSourceId);

        public void RequestSetPreviewSource(long sourceId)
        {
            Source source = ParentSwitcher.GetSource(sourceId);
            if (source == null)
                throw new NotExistingSourceException(string.Format("Switcher has no source with ID #{0}!", sourceId));
            ApiMixEffectBlock.SetInt(_BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewInput, sourceId);
        }

        public void RequestSetPreviewSource(Source source)
            => RequestSetPreviewSource(source.ID);
        #endregion


        #region Transitions
        public void PerformAutoTransition()
        {
            ApiMixEffectBlock.PerformAutoTransition();
        }

        public void PerformCutTransition()
        {
            ApiMixEffectBlock.PerformCut();
        }
        #endregion

    }

}
