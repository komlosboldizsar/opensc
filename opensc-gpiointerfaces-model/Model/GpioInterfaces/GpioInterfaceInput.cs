using OpenSC.Model.General;
using OpenSC.Model.SourceGenerators;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.GpioInterfaces
{

    public partial class GpioInterfaceInput : BooleanBase, ISystemObject
    {

        public GpioInterfaceInput() : base() => SystemObjectRegister.Instance.Register(this);

        public GpioInterfaceInput(string name, GpioInterface gpioInterface, int index) : base()
        {
            this.name = name;
            this.GpioInterface = gpioInterface;
            this.Index = index;
            this.Color = System.Drawing.Color.Blue;
            SystemObjectRegister.Instance.Register(this);
            generateIdentifier();
            generateDescription();
            register();
            gpioInterface.GlobalIdChanged += (i, ov, nv) => generateGlobalId();
            gpioInterface.IdChanged += (i, ov, nv) => generateDescription();
            gpioInterface.NameChanged += (i, ov, nv) => generateDescription();
            generateGlobalId();
        }

        #region GlobalID generation
        private void generateGlobalId()
        {
            if (GpioInterface == null)
            {
                GlobalID = null;
                return;
            }
            GlobalID = $"{GpioInterface.GlobalID}.input.{Index}";
        }
        #endregion

        #region Identifier generation
        private void generateIdentifier()
        {
            if (GpioInterface == null)
            {
                Identifier = $"gpiointerface.unknown.input.{Index}";
                return;
            }
            Identifier = $"gpiointerface.{GpioInterface.ID}.input.{Index}";
        }
        #endregion

        #region Description generation
        private void generateDescription()
        {
            if (GpioInterface == null)
            {
                Description = $"Input [(#{index}) {name}] of unknown GPIO interface.";
                return;
            }
            Description = $"Input [(#{index}) {name}] of GPIO interface [(#{GpioInterface.ID}) {GpioInterface.Name}].";
        }
        #endregion

        #region Property: Name
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_name_afterChange))]
        private string name;

        private void _name_afterChange(string oldValue, string newValue) => generateDescription();
        #endregion

        #region Property: GpioInterface
        public GpioInterface GpioInterface { get; private set; }

        internal void AssignParentGpioInterface(GpioInterface gpioInterface)
        {
            if (GpioInterface != null)
                return;
            GpioInterface = gpioInterface;
            gpioInterface.GlobalIdChanged += (i, ov, nv) => generateGlobalId();
            generateGlobalId();
        }

        public void RemovedFromGpioInterface(GpioInterface gpioInterface)
        {
            if (gpioInterface != GpioInterface)
                return;
            GpioInterface = null;
        }
        #endregion

        #region Property: Index
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_index_afterChange))]
        private int index;

        private void _index_afterChange(int oldValue, int newValue)
        {
            generateIdentifier();
            generateDescription();
            generateGlobalId();
        }
        #endregion
        
        #region Property: DebounceTime
        [AutoProperty]
        private int debounceTime;
        #endregion

        #region State
        public void NotifyStateChanged(bool newValue)
        {
            if ((newValue == CurrentState) || (DateTime.Now - lastStateChange).TotalMilliseconds<debounceTime)
                return;
            CurrentState = newValue;
            lastStateChange = DateTime.Now;
        }

        internal virtual void QueryState() { }

        private DateTime lastStateChange = DateTime.Now;
        #endregion

        #region ToString()
        public override string ToString() => string.Format("(#{0}) {1}", index, name);
        #endregion

    }

}
