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

        public MixEffectBlock(Switcher parentSwitcher, int index)
        {
            ParentSwitcher = parentSwitcher;
            ApiMixEffectBlock = parentSwitcher.ApiSwitcher.GetMixEffectBlock(index);
            ApiMixEffectBlock.AddCallback(this);
            Index = index;
        }


        public MixEffectBlock(Switcher parentSwitcher, IBMDSwitcherMixEffectBlock apiMixEffectBlock, int index)
        {
            ParentSwitcher = parentSwitcher;
            ApiMixEffectBlock = apiMixEffectBlock;
            ApiMixEffectBlock.AddCallback(this);
            Index = index;
        }

        public void PropertyChanged(_BMDSwitcherMixEffectBlockPropertyId propertyId)
        {
            switch (propertyId)
            {
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdProgramInput:
                    InvokeHelper.Invoke(() =>
                    {
                        ApiMixEffectBlock.GetInt(_BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdProgramInput, out long programInput);
                        ProgramSource = programInput;
                    });
                    break;
                case _BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewInput:
                    InvokeHelper.Invoke(() =>
                    {
                        ApiMixEffectBlock.GetInt(_BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewInput, out long previewInput);
                        PreviewSource = previewInput;
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

        private long programSource;

        public long ProgramSource
        {
            get => programSource;
            private set
            {
                if (value == programSource)
                    return;
                programSource = value;
                ProgramInputChanged?.Invoke(ApiMixEffectBlock, this, value);
            }
        }

        public void RequestSetProgramSource(long inputId)
        {
            Source source = ParentSwitcher.GetSource(inputId);
            if (source == null)
                throw new NotExistingSourceException(string.Format("Switcher has no source with ID #{0}!", inputId));
            ApiMixEffectBlock.SetInt(_BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdProgramInput, inputId);
        }
        #endregion

        #region Preview source
        public delegate void PreviewInputChangedDelegate(IBMDSwitcherMixEffectBlock apiMeBlock, MixEffectBlock meBlock, long sourceId);
        public event PreviewInputChangedDelegate PreviewInputChanged;

        private long previewSource;

        public long PreviewSource
        {
            get => previewSource;
            private set
            {
                if (value == previewSource)
                    return;
                previewSource = value;
                PreviewInputChanged?.Invoke(ApiMixEffectBlock, this, value);
            }
        }

        public void RequestSetPreviewSource(long inputId)
        {
            Source source = ParentSwitcher.GetSource(inputId);
            if (source == null)
                throw new NotExistingSourceException(string.Format("Switcher has no source with ID #{0}!", inputId));
            ApiMixEffectBlock.SetInt(_BMDSwitcherMixEffectBlockPropertyId.bmdSwitcherMixEffectBlockPropertyIdPreviewInput, inputId);
        }
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
