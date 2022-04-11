using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{

    public abstract class Umd : ModelBase
    {

        #region Instantiation, restoration, persistence, removation
        public Umd()
        {
            alignmentWithFullStaticText = DefaultAlignmentWithFullStaticText;
            initTextThings();
            initTallyThings();
        }

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            handleRestoredOwnFieldsTexts();
            handleRestoredOwnFieldsTallies();
        }

        public override void RestoreCustomRelations()
        {
            base.RestoreCustomRelations();
            Texts.ForEach(t => t.RestoreCustomRelations());
            Tallies.ForEach(t => t.RestoreCustomRelations());
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            updateTotalToHardware();
        }

        public override void Removed()
        {
            base.Removed();
            FullStaticTextChanged = null;
            UseFullStaticTextChanged = null;
            Texts.ForEach(t => t.Removed());
            Tallies.ForEach(t => t.Removed());
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = UmdDatabase.Instance;
        #endregion

        #region Property: DisplayableRawText
        public event PropertyChangedTwoValuesDelegate<Umd, string> DisplayableRawTextChanged;

        protected string displayableRawText = "";

        public string DisplayableRawText
        {
            get => displayableRawText;
            set => this.setProperty(ref displayableRawText, value, DisplayableRawTextChanged);
        }
        #endregion

        #region Property: DisplayableCompactText
        public event PropertyChangedTwoValuesDelegate<Umd, string> DisplayableCompactTextChanged;

        protected string displayableCompactText = "";

        public string DisplayableCompactText
        {
            get => displayableCompactText;
            set => this.setProperty(ref displayableCompactText, value, DisplayableCompactTextChanged);
        }
        #endregion

        #region Property: FullStaticText
        public event PropertyChangedTwoValuesDelegate<Umd, string> FullStaticTextChanged;

        [PersistAs("full_static_text")]
        private string fullStaticText;

        public string FullStaticText
        {
            get => fullStaticText;
            set => this.setProperty(ref fullStaticText, value, FullStaticTextChanged, null, (_, _) =>
            {
                if (useFullStaticText)
                    updateTextsToHardware();
            });
        }
        #endregion

        #region Property: UseFullStaticText
        public event PropertyChangedTwoValuesDelegate<Umd, bool> UseFullStaticTextChanged;

        [PersistAs("use_full_static_text")]
        private bool useFullStaticText = false;

        public bool UseFullStaticText
        {
            get => useFullStaticText;
            set => this.setProperty(ref useFullStaticText, value, UseFullStaticTextChanged, null, (_, _) => updateTextsToHardware());
        }
        #endregion

        #region Property: AlignmentWithFullStaticText
        public event PropertyChangedTwoValuesDelegate<Umd, UmdTextAlignment> AlignmentWithFullStaticTextChanged;

        [PersistAs("alignment_with_full_static_text")]
        private UmdTextAlignment alignmentWithFullStaticText = UmdTextAlignment.Left;

        public UmdTextAlignment AlignmentWithFullStaticText
        {
            get => alignmentWithFullStaticText;
            set
            {
                if (!AlignableFullStaticText)
                    return;
                this.setProperty(ref alignmentWithFullStaticText, value, AlignmentWithFullStaticTextChanged, null, (_, _) =>
                {
                    if (useFullStaticText)
                        updateTextsToHardware();
                });
            }
        }
        #endregion

        #region Read-only full static text settings
        public virtual bool AlignableFullStaticText { get; } = false;
        public virtual UmdTextAlignment DefaultAlignmentWithFullStaticText { get; } = UmdTextAlignment.Left;
        #endregion

        #region Texts
        public abstract UmdTextInfo[] TextInfo { get; }
        [PersistAs("texts")]
        [PersistAs(null, 1)]
        public readonly List<UmdText> Texts = new();
        private List<UmdText> textsByConstructor;

        private void initTextThings()
        {
            int i = 0;
            foreach (UmdTextInfo textInfo in TextInfo)
                Texts.Add(new(this, i++, textInfo));
            textsByConstructor = new(Texts);
        }

        private void handleRestoredOwnFieldsTexts()
        {
            textsByConstructor.ForEach(t => t.Removed());
            textsByConstructor.Clear();
            textsByConstructor = null;
            int textCount = Texts.Count;
            int textInfoLength = TextInfo.Length;
            if (textCount > textInfoLength)
            {
                for (int i = textCount - 1; i >= textInfoLength; i--)
                {
                    Texts[i].Removed();
                    Texts.RemoveAt(i);
                }
            }
            if (textCount < textInfoLength)
                for (int i = textCount; i < textInfoLength; i++)
                    Texts.Add(new(this, i, TextInfo[i]));
        }

        internal void NotifyTextStaticValueChanged(UmdText text)
        {
            if (text.UseStaticValue)
                notifyTextChanged(text);
        }

        internal void NotifyTextUseStaticValueChanged(UmdText text) => notifyTextChanged(text);
        internal void NotifyTextUsedChanged(UmdText text) => notifyTextChanged(text);
        internal void NotifyTextAlignmentChanged(UmdText text) => notifyTextChanged(text);
        internal void NotifyTextCurrentValueChanged(UmdText text) => notifyTextChanged(text);

        private void notifyTextChanged(UmdText text)
        {
            if (text.Used)
                updateTexts();
        }

        protected virtual void updateTexts()
        {
            if (useFullStaticText)
                DisplayableCompactText = fullStaticText;
            else
                DisplayableCompactText = string.Join(" | ", Texts.Where(t => t.Used).Select(t => t.CurrentValue));
            updateTextsToHardware();
        }
        #endregion

        #region Tallies
        public abstract UmdTallyInfo[] TallyInfo { get; }
        [PersistAs("tallies")]
        [PersistAs(null, 1)]
        public readonly List<UmdTally> Tallies = new();
        private List<UmdTally> talliesByConstructor;

        private void initTallyThings()
        {
            int i = 0;
            foreach (UmdTallyInfo tallyInfo in TallyInfo)
                Tallies.Add(new(this, i++, tallyInfo));
            talliesByConstructor = new(Tallies);
        }

        private void handleRestoredOwnFieldsTallies()
        {
            talliesByConstructor.ForEach(t => t.Removed());
            talliesByConstructor.Clear();
            talliesByConstructor = null;
            int tallyCount = Tallies.Count;
            int tallyInfoLength = TallyInfo.Length;
            if (tallyCount > tallyInfoLength)
            {
                for (int i = tallyCount - 1; i >= tallyInfoLength; i--)
                {
                    Tallies[i].Removed();
                    Tallies.RemoveAt(i);
                }
            }
            if (tallyCount < tallyInfoLength)
                for (int i = tallyCount; i < tallyInfoLength; i++)
                    Tallies.Add(new(this, i, TallyInfo[i]));
        }

        internal void NotifyTallyColorChanged(UmdTally tally) => updateTalliesToHardware();
        internal void NotifyTallyCurrentStateChanged(UmdTally tally) => updateTalliesToHardware();
        #endregion

        protected abstract void updateTextsToHardware();
        protected abstract void updateTalliesToHardware();
        protected abstract void updateTotalToHardware();

    }

}
