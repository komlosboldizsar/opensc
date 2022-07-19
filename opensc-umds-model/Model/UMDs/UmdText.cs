using OpenSC.Model.General;
using OpenSC.Model.SourceGenerators;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{

    public partial class UmdText : ObjectBase
    {

        public Umd Owner { get; private set; }
        public int IndexAtOwner { get; init; }
        public UmdTextInfo Info { get; init; }

        #region Instantiation, persistence
        public UmdText(Umd owner, int indexAtOwner, UmdTextInfo info)
        {
            Owner = owner;
            IndexAtOwner = indexAtOwner;
            Info = info;
            used = info.DefaultUsed;
            alignment = info.DefaultAlignment;
        }

        internal void RestoreCustomRelations() => Source = SystemObjectRegister.Instance[_tfk_globalId_source] as DynamicText;

        internal void Removed()
        {
            Owner = null;
            SourceChanged = null;
            AlignmentChanged = null;
            CurrentValueChanged = null;
            if (source != null)
                source.CurrentTextChanged -= sourceCurrentTextChangedHandler;
        }
        #endregion

        #region Property: Source
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_source_beforeChange))]
        [AutoProperty.AfterChange(nameof(_source_afterChange))]
        private DynamicText source;

        private void _source_beforeChange(DynamicText oldValue, DynamicText newValue, BeforeChangePropertyArgs args)
        {
            if (oldValue != null)
                oldValue.CurrentTextChanged -= sourceCurrentTextChangedHandler;
        }

        private void _source_afterChange(DynamicText oldValue, DynamicText newValue)
        {
            
            if (newValue != null)
                newValue.CurrentTextChanged += sourceCurrentTextChangedHandler;
            if (!useStaticValue)
                CurrentValue = newValue?.CurrentText;
        }

        internal string _tfk_globalId_source; // Temp foreign key

        private void sourceCurrentTextChangedHandler(DynamicText item, string oldValue, string newValue)
        {
            if (useStaticValue)
                return;
            CurrentValue = newValue;
        }
        #endregion

        #region Property: StaticValue
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_staticValue_afterChange))]
        private string staticValue;

        private void _staticValue_afterChange(string oldValue, string newValue)
        {
            if (useStaticValue)
                CurrentValue = newValue;
        }
        #endregion

        #region Property: UseStaticValue
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_useStaticValue_afterChange))]
        private bool useStaticValue;

        private void _useStaticValue_afterChange(bool oldValue, bool newValue)
            => CurrentValue = newValue ? StaticValue : Source?.CurrentText;
        #endregion

        #region Property: Used
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_used_beforeChange))]
        [AutoProperty.AfterChange(nameof(_used_afterChange))]
        private bool used;

        private void _used_beforeChange(bool oldValue, bool newValue, BeforeChangePropertyArgs args)
        {
            if (!Info.Switchable)
                args.Cancel();
        }

        private void _used_afterChange(bool oldValue, bool newValue) => Owner?.NotifyTextUsedChanged(this);
        #endregion

        #region Property: Alignment
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_alignment_beforeChange))]
        [AutoProperty.AfterChange(nameof(_alignment_afterChange))]
        private UmdTextAlignment alignment;

        private void _alignment_beforeChange(UmdTextAlignment oldValue, UmdTextAlignment newValue, BeforeChangePropertyArgs args)
        {
            if (!Info.Alignable)
                args.Cancel();
        }

        private void _alignment_afterChange(UmdTextAlignment oldValue, UmdTextAlignment newValue) => Owner?.NotifyTextAlignmentChanged(this);
        #endregion

        #region Property: CurrentValue
        public event PropertyChangedTwoValuesDelegate<UmdText, string> CurrentValueChanged;
        private string currentValue = string.Empty;
        public string CurrentValue
        {
            get => currentValue;
            set => this.setProperty(ref currentValue, value ?? "", CurrentValueChanged, null, (ov, nv) => Owner?.NotifyTextCurrentValueChanged(this));
        }
        #endregion

    }

}
