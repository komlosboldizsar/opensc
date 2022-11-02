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
            Texts = new(this);
            Tallies = new(this);
            alignmentWithFullStaticText = DefaultAlignmentWithFullStaticText;
        }

        public override void RestoredOwnFields()
        {
            base.RestoredOwnFields();
            Texts.RestoredOwnFields();
            Tallies.RestoredOwnFields();
        }

        public override void RestoreCustomRelations()
        {
            base.RestoreCustomRelations();
            Texts.RestoreCustomRelations();
            Tallies.RestoreCustomRelations();
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
            Texts.ParentRemoved();
            Tallies.ParentRemoved();
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
        [PersistAs("full_static")]
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
        [PersistAs("full_static/@use")]
        private bool useFullStaticText = false;
        #endregion

        #region Property: AlignmentWithFullStaticText
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_alignmentWithFullStaticText_beforeChange))]
        [AutoProperty.AfterChange(nameof(_alignmentWithFullStaticText_afterChange))]
        [PersistAs("full_static/@alignment")]
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
        [PersistAs("periodic_update/@enabled")]
        private bool periodicUpdateEnabled = true;
        #endregion

        #region Property: PeriodicUpdateInterval
        [AutoProperty]
        [PersistAs("periodic_update/@interval")]
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
        [PersistAs("text", 1)]
        [PersistSubclass(nameof(textTypeGetter), 1)]
        [DeserializeMembersOnly]
        public readonly UmdTextCollection Texts;
        protected virtual Type textTypeGetter() => typeof(UmdText);
        protected internal virtual UmdText CreateText() => new();

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
        [PersistAs("tally", 1)]
        [PersistSubclass(nameof(tallyTypeGetter), 1)]
        [DeserializeMembersOnly]
        public readonly UmdTallyCollection Tallies;
        protected virtual Type tallyTypeGetter() => typeof(UmdTally);
        protected internal virtual UmdTally CreateTally() => new();

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
