using OpenSC.Model.General;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{

    public class UmdTally : ObjectBase
    {

        public Umd Owner { get; private set; }
        public int IndexAtOwner { get; init; }
        public UmdTallyInfo Info { get; init; }

        #region Instantiation, persistence
        public UmdTally(Umd owner, int indexAtOwner, UmdTallyInfo info)
        {
            Owner = owner;
            IndexAtOwner = indexAtOwner;
            Info = info;
            color = info.DefaultColor;
        }

        internal void RestoreCustomRelations() => Source = BooleanRegister.Instance[_tfk_name_source];

        internal void Removed()
        {
            Owner = null;
            SourceChanged = null;
            ColorChanged = null;
            CurrentStateChanged = null;
            if (source != null)
                source.StateChanged -= sourceStateChangedHandler;
        }
        #endregion

        #region Property: Source
        public event PropertyChangedTwoValuesDelegate<UmdTally, IBoolean> SourceChanged;
        private IBoolean source;
        public IBoolean Source
        {
            get => source;
            set => this.setProperty(ref source, value, SourceChanged, null, afterSourceChange);
        }

        private void afterSourceChange(IBoolean oldValue, IBoolean newValue)
        {
            if (oldValue != null)
                oldValue.StateChanged -= sourceStateChangedHandler;
            if (newValue != null)
            {
                newValue.StateChanged += sourceStateChangedHandler;
                CurrentState = newValue.CurrentState;
            }
            else
            {
                CurrentState = false;
            }
        }

        internal string _tfk_name_source; // Temp foreign key

        private void sourceStateChangedHandler(IBoolean item, bool oldValue, bool newValue) => CurrentState = newValue;
        #endregion

        #region Property: Color
        public event PropertyChangedTwoValuesDelegate<UmdTally, Color> ColorChanged;
        private Color color;
        public Color Color
        {
            get => color;
            set
            {
                if (Info.ColorMode == UmdTallyInfo.ColorSettingMode.Fix)
                    return;
                this.setProperty(ref color, value, ColorChanged, null, (ov, nv) => Owner?.NotifyTallyColorChanged(this));
            }
        }
        #endregion

        #region Property: CurrentState
        public event PropertyChangedTwoValuesDelegate<UmdTally, bool> CurrentStateChanged;
        private bool currentState;
        public bool CurrentState
        {
            get => currentState;
            set => this.setProperty(ref currentState, value, CurrentStateChanged, null, (ov, nv) => Owner?.NotifyTallyCurrentStateChanged(this));
        }
        #endregion

    }

}
