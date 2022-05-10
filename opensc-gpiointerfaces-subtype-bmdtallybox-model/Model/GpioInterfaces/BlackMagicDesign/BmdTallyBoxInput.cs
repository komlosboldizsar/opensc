using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.GpioInterfaces.BlackMagicDesign
{
    public class BmdTallyBoxInput : GpioInterfaceInput
    {

        public BmdTallyBoxInput() : base()
        { }

        public BmdTallyBoxInput(string name, GpioInterface gpioInterface, int index) : base(name, gpioInterface, index)
        { }

    }

}
