using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.SourceGenerators;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.GpioInterfaces
{

    public abstract partial class GpioInterfaceOutput : SystemObjectBase
    {

        public GpioInterfaceOutput(string name, GpioInterface gpioInterface, int index) : base()
        {
            this.name = name;
            this.GpioInterface = gpioInterface;
            this.Index = index;
            SystemObjectRegister.Instance.Register(this);
            gpioInterface.GlobalIdChanged += (i, ov, nv) => generateGlobalId();
            generateGlobalId();
        }

        public delegate void RemovedDelegate(GpioInterfaceOutput routerOutput);
        public event RemovedDelegate Removed;

        #region GlobalID generation
        private void generateGlobalId()
        {
            if (GpioInterface == null)
            {
                GlobalID = null;
                return;
            }
            GlobalID = $"{GpioInterface.GlobalID}.output.{Index}";
        }
        #endregion

        #region Property: Name
        [AutoProperty]
        [AutoProperty.Validator(nameof(ValidateName))]
        private string name;

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Property: GpioInterface
        public GpioInterface GpioInterface { get; private set; }

        internal void AssignParentRouter(GpioInterface router)
        {
            if (GpioInterface != null)
                return;
            GpioInterface = router;
            router.GlobalIdChanged += (i, ov, nv) => generateGlobalId();
        }

        public void RemovedFromGpioInterface(GpioInterface gpioInterface)
        {
            if (gpioInterface != GpioInterface)
                return;
            GpioInterface = null;
            Removed?.Invoke(this);
        }
        #endregion

        #region Property: Index
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(generateGlobalId))]
        private int index;
        #endregion

        #region Property: Driver
#pragma warning disable CS0169
        internal string _driverIdentifier; // "Temp foreign key"
#pragma warning restore CS0169

        [AutoProperty]
        [AutoProperty.BeforeChange(nameof(_driver_beforeChange))]
        [AutoProperty.AfterChange(nameof(_driver_afterChange))]
        private IBoolean driver;

        private void _driver_beforeChange(IBoolean oldValue, IBoolean newValue)
        {
            if (oldValue != null)
                oldValue.StateChanged -= driverStateChanged;
        }

        private void _driver_afterChange(IBoolean oldValue, IBoolean newValue)
        {
            if (newValue != null)
                newValue.StateChanged += driverStateChanged;
            sendState();
        }

        internal void RestoreDriver()
        {
            if (_driverIdentifier != null)
                Driver = BooleanRegister.Instance[_driverIdentifier];
        }

        private void driverStateChanged(IBoolean item, bool oldValue, bool newValue) => sendState();
        #endregion

        #region State
        protected internal abstract void sendState();
        #endregion

        #region ToString()
        public override string ToString() => string.Format("(#{0}) {1}", index, name);
        #endregion

    }

}
