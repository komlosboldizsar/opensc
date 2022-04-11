using OpenSC.Model.General;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{

    public class UmdText : ObjectBase
    {

        public UMD Owner { get; private set; }
        public int IndexAtOwner { get; init; }
        public UmdTextInfo Info { get; init; }

        #region Instantiation, persistence
        public UmdText(UMD owner, int indexAtOwner, UmdTextInfo info)
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
        public event PropertyChangedTwoValuesDelegate<UmdText, DynamicText> SourceChanged;
        private DynamicText source;
        public DynamicText Source
        {
            get => source;
            set => this.setProperty(ref source, value, SourceChanged, null, afterSourceChange);
        }

        private void afterSourceChange(DynamicText oldValue, DynamicText newValue)
        {
            if (oldValue != null)
                oldValue.CurrentTextChanged -= sourceCurrentTextChangedHandler;
            if (newValue != null)
            {
                newValue.CurrentTextChanged += sourceCurrentTextChangedHandler;
                CurrentValue = newValue.CurrentText;
            }
            else
            {
                CurrentValue = null;
            }
        }

        internal string _tfk_globalId_source; // Temp foreign key

        private void sourceCurrentTextChangedHandler(DynamicText item, string oldValue, string newValue) => CurrentValue = newValue;
        #endregion

        #region Property: StaticValue
        public event PropertyChangedTwoValuesDelegate<UmdText, string> StaticValueChanged;
        private string staticValue;
        public string StaticValue
        {
            get => staticValue;
            set => this.setProperty(ref staticValue, value, StaticValueChanged, null, (_, _) => Owner?.NotifyTextStaticValueChanged(this));
        }
        #endregion

        #region Property: UseStaticValue
        public event PropertyChangedTwoValuesDelegate<UmdText, bool> UseStaticValueChanged;
        private bool useStaticValue;
        public bool UseStaticValue
        {
            get => useStaticValue;
            set => this.setProperty(ref useStaticValue, value, UseStaticValueChanged, null, (_, _) => Owner?.NotifyTextUseStaticValueChanged(this));
        }
        #endregion

        #region Property: Used
        public event PropertyChangedTwoValuesDelegate<UmdText, bool> UsedChanged;
        private bool used;
        public bool Used
        {
            get => used;
            set
            {
                if (!Info.Switchable)
                    return;
                this.setProperty(ref used, value, UsedChanged, null, (ov, nv) => Owner?.NotifyTextUsedChanged(this));
            }
        }
        #endregion

        #region Property: Alignment
        public event PropertyChangedTwoValuesDelegate<UmdText, UmdTextAlignment> AlignmentChanged;
        private UmdTextAlignment alignment;
        public UmdTextAlignment Alignment
        {
            get => alignment;
            set
            {
                if (!Info.Alignable)
                    return;
                this.setProperty(ref alignment, value, AlignmentChanged, null, (ov, nv) => Owner?.NotifyTextAlignmentChanged(this));
            }
        }
        #endregion

        #region Property: CurrentValue
        public event PropertyChangedTwoValuesDelegate<UmdText, string> CurrentValueChanged;
        private string currentValue;
        public string CurrentValue
        {
            get => currentValue;
            set => this.setProperty(ref currentValue, value, CurrentValueChanged, null, (ov, nv) => Owner?.NotifyTextCurrentValueChanged(this));
        }
        #endregion

    }

}
