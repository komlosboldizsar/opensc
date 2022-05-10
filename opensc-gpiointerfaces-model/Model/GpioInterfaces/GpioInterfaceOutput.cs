using OpenSC.Logger;
using OpenSC.Model.General;
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

    public abstract class GpioInterfaceOutput : SystemObjectBase
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
        private string name;

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException();
                if (value == name)
                    return;
                string oldName = name;
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(Name));
            }
        }

        public delegate void NameChangedDelegate(GpioInterfaceOutput output, string oldName, string newName);
        public event NameChangedDelegate NameChanged;
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
        private int index;

        public int Index
        {
            get => index;
            set
            {
                if (value == index)
                    return;
                int oldValue = index;
                index = value;
                IndexChanged?.Invoke(this, oldValue, value);
                ((INotifyPropertyChanged)this).RaisePropertyChanged(nameof(Index));
                generateGlobalId();
            }
        }

        public event PropertyChangedTwoValuesDelegate<GpioInterfaceOutput, int> IndexChanged;
        #endregion

        #region Property: Driver
        public event PropertyChangedTwoValuesDelegate<GpioInterfaceOutput, IBoolean> DriverChanged;

#pragma warning disable CS0169
        internal string _driverIdentifier; // "Temp foreign key"
#pragma warning restore CS0169

        private IBoolean driver;

        public IBoolean Driver
        {
            get => driver;
            set
            {
                BeforeChangePropertyDelegate<IBoolean> beforeChangeDelegate = (ov, nv) =>
                {
                    if (ov != null)
                        ov.StateChanged -= driverStateChanged;
                };
                AfterChangePropertyDelegate<IBoolean> afterChangeDelegate = (ov, nv) =>
                {
                    if (nv != null)
                        nv.StateChanged += driverStateChanged;
                    sendState();
                };
                this.setProperty(ref driver, value, DriverChanged, beforeChangeDelegate, afterChangeDelegate);
            }
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
