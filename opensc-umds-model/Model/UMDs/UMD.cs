using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
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

    public abstract partial class Umd : ModelBase
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

        #region Property: Enabled
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(UpdateEverything))]
        [PersistAs("enabled")]
        private bool enabled = true;
        #endregion

        #region Property: DisplayableRawText
        [AutoProperty]
        protected string displayableRawText = string.Empty;
        #endregion

        #region Property: DisplayableCompactText
        [AutoProperty]
        protected string displayableCompactText = string.Empty;
        #endregion

        #region Property: FullStaticText
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_fullStaticText_afterChange))]
        [PersistAs("full_static_text")]
        private string fullStaticText;

        private void _fullStaticText_afterChange(string oldValue, string newValue)
        {
            if (useFullStaticText)
                UpdateTexts();
        }
        #endregion

        #region Property: UseFullStaticText
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(UpdateTexts))]
        [PersistAs("use_full_static_text")]
        private bool useFullStaticText = false;
        #endregion

        #region Property: AlignmentWithFullStaticText
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_alignmentWithFullStaticText_beforeChange))]
        [AutoProperty.AfterChange(nameof(_alignmentWithFullStaticText_afterChange))]
        [PersistAs("alignment_with_full_static_text")]
        private UmdTextAlignment alignmentWithFullStaticText = UmdTextAlignment.Left;

        private void _alignmentWithFullStaticText_beforeChange(UmdTextAlignment oldValue, UmdTextAlignment newValue, BeforeChangePropertyArgs args)
        {
            if (!AlignableFullStaticText)
                args.Cancel();
        }

        private void _alignmentWithFullStaticText_afterChange(UmdTextAlignment oldValue, UmdTextAlignment newValue)
        {
            if (useFullStaticText)
                UpdateTexts();
        }
        #endregion

        #region Property: PeriodicUpdateEnabled
        [AutoProperty]
        [PersistAs("periodic_update_enabled")]
        private bool periodicUpdateEnabled = true;
        #endregion

        #region Property: PeriodicUpdateInterval
        [AutoProperty]
        [PersistAs("periodic_update_interval")]
        private int periodicUpdateInterval = 15;
        #endregion

        internal int secondsSinceLastPeriodicUpdate = 0;

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
            if (!enabled)
                return;
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
            if (!enabled)
                return;
            sendTalliesToHardware();
        }

        protected abstract void calculateTallyFields();
        protected abstract void sendTalliesToHardware();
        #endregion

        public void UpdateEverything()
        {
            calculateTextFields();
            calculateTallyFields();
            if (!enabled)
                return;
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
