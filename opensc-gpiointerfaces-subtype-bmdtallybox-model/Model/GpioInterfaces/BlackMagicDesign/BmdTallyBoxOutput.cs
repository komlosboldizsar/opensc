using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.GpioInterfaces.BlackMagicDesign
{
    public class BmdTallyBoxOutput : GpioInterfaceOutput
    {

        public BmdTallyBoxOutput(string name, GpioInterface gpioInterface, int index) : base(name, gpioInterface, index)
        { }

        protected override void sendState() => ((BmdTallyBox)GpioInterface)?.SendOutputState(Index, Driver.CurrentState);

    }

}
