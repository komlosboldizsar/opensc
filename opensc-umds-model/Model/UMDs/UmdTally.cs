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

    public partial class UmdTally : ObjectBase, IComponent<Umd, UmdTally, UmdTallyCollection>
    {

        [AutoProperty]
        private int index;

        public string Name { get; set; } /* remove later */

        private UmdTallyInfo _info;
        public virtual UmdTallyInfo Info
        {
            get => _info;
            set
            {
                _info = value;
                color = value.DefaultColor;
            }
        }

        #region Instantiation, persistence
        internal void RestoreCustomRelations()
            => Source = BooleanRegister.Instance[_tfk_name_source];

        public void Removed()
        {
            Parent = null;
            SourceChanged = null;
            ColorChanged = null;
            CurrentStateChanged = null;
            if (source != null)
                source.StateChanged -= sourceStateChangedHandler;
        }
        #endregion


        #region Property: Parent (=Umd)
        public Umd Parent { get; private set; }
        private UmdTallyCollection parentCollection;

        public void AssignParent(Umd umd, UmdTallyCollection parentCollection)
        {
            if (Parent != null)
                return;
            Parent = umd;
            this.parentCollection = parentCollection;
        }

        public void deassignParent()
        {
            Parent = null;
            parentCollection = null;
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

        private void _color_afterChange(Color oldValue, Color newValue) => Parent?.NotifyTallyColorChanged(this);
        #endregion

        #region Property: CurrentState
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_currentState_afterChange))]
        private bool currentState;

        private void _currentState_afterChange(bool oldValue, bool newValue) => Parent?.NotifyTallyCurrentStateChanged(this);
        #endregion

    }

}
