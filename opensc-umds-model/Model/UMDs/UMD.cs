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
            UpdateEverything();
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
                    UpdateTexts();
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
            set => this.setProperty(ref useFullStaticText, value, UseFullStaticTextChanged, null, (_, _) => UpdateTexts());
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
                        UpdateTexts();
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
        [PersistSubclass(nameof(textTypeGetter))]
        public readonly List<UmdText> Texts = new();
        protected virtual Type textTypeGetter() => typeof(UmdText);
        protected internal virtual UmdText CreateText(Umd owner, int indexAtOwner, UmdTextInfo info) => new(owner, indexAtOwner, info);

        private List<UmdText> textsByConstructor;

        private void initTextThings()
        {
            int i = 0;
            foreach (UmdTextInfo textInfo in TextInfo)
                Texts.Add(CreateText(this, i++, textInfo));
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
                    Texts.Add(CreateText(this, i, TextInfo[i]));
        }

        internal void NotifyTextUsedChanged(UmdText text)
        {
            if (!Updating)
                UpdateTexts();
        }

        internal void NotifyTextAlignmentChanged(UmdText text)
        {
            if (!Updating && text.Used)
                UpdateTexts();
        }

        internal void NotifyTextCurrentValueChanged(UmdText text)
        {
            if (!Updating && text.Used)
                UpdateTexts();
        }

        public void UpdateTexts()
        {
            calculateTextFields();
            sendTextsToHardware();
        }

        protected abstract void calculateTextFields();
        protected abstract void sendTextsToHardware();
        #endregion

        #region Tallies
        public abstract UmdTallyInfo[] TallyInfo { get; }
        [PersistAs("tallies")]
        [PersistAs(null, 1)]
        [PersistSubclass(nameof(tallyTypeGetter))]
        public readonly List<UmdTally> Tallies = new();
        protected virtual Type tallyTypeGetter() => typeof(UmdTally);
        protected internal virtual UmdTally CreateTally(Umd owner, int indexAtOwner, UmdTallyInfo info) => new(owner, indexAtOwner, info);

        private List<UmdTally> talliesByConstructor;

        private void initTallyThings()
        {
            int i = 0;
            foreach (UmdTallyInfo tallyInfo in TallyInfo)
                Tallies.Add(CreateTally(this, i++, tallyInfo));
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
                    Tallies.Add(CreateTally(this, i, TallyInfo[i]));
        }

        internal void NotifyTallyColorChanged(UmdTally tally)
        {
            if (!Updating)
                UpdateTallies();
        }

        internal void NotifyTallyCurrentStateChanged(UmdTally tally)
        {
            if (!Updating)
                UpdateTallies();
        }

        public void UpdateTallies()
        {
            calculateTallyFields();
            sendTalliesToHardware();
        }

        protected abstract void calculateTallyFields();
        protected abstract void sendTalliesToHardware();
        #endregion

        public void UpdateEverything()
        {
            calculateTextFields();
            calculateTallyFields();
            sendEverythingToHardware();
        }

        protected abstract void sendEverythingToHardware();

        protected override void afterUpdate()
        {
            base.afterUpdate();
            UpdateEverything();
        }

    }

}
