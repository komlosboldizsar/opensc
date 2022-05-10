using OpenSC.Model.General;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.GpioInterfaces
{

    public class GpioInterfaceInput : BooleanBase, ISystemObject
    {

        public GpioInterfaceInput() : base() => SystemObjectRegister.Instance.Register(this);

        public GpioInterfaceInput(string name, GpioInterface gpioInterface, int index) : base()
        {
            this.name = name;
            this.GpioInterface = gpioInterface;
            this.Index = index;
            SystemObjectRegister.Instance.Register(this);
            gpioInterface.GlobalIdChanged += (i, ov, nv) => generateGlobalId();
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

        public event PropertyChangedTwoValuesDelegate<GpioInterfaceInput, string> NameChanged;
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

        public event PropertyChangedTwoValuesDelegate<GpioInterfaceInput, int> IndexChanged;
        #endregion

        #region State
        public void NotifyStateChanged(bool newValue) => CurrentState = newValue;

        internal virtual void QueryState()
        { }
        #endregion

        #region ToString()
        public override string ToString() => string.Format("(#{0}) {1}", index, name);
        #endregion

    }

}
