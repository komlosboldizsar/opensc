using OpenSC.Model.General;
using OpenSC.Model.SourceGenerators;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{

    public partial class UmdTally : ObjectBase
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
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_source_beforeChange))]
        [AutoProperty.AfterChange(nameof(_source_afterChange))]
        private IBoolean source;

        private void _source_beforeChange(IBoolean oldValue, IBoolean newValue, BeforeChangePropertyArgs args)
        {
            if (oldValue != null)
                oldValue.StateChanged -= sourceStateChangedHandler;
        }

        private void _source_afterChange(IBoolean oldValue, IBoolean newValue)
        {
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
        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_color_beforeChange))]
        [AutoProperty.AfterChange(nameof(_color_afterChange))]
        private Color color;

        private void _color_beforeChange(Color oldValue, Color newValue, BeforeChangePropertyArgs args)
        {
            if (Info.ColorMode == UmdTallyInfo.ColorSettingMode.Fix)
                args.Cancel();
        }

        private void _color_afterChange(Color oldValue, Color newValue) => Owner?.NotifyTallyColorChanged(this);
        #endregion

        #region Property: CurrentState
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_currentState_afterChange))]
        private bool currentState;

        private void _currentState_afterChange(bool oldValue, bool newValue) => Owner?.NotifyTallyCurrentStateChanged(this);
        #endregion

    }

}
