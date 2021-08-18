using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMD.Switcher.Exceptions
{

    public class AlreadyConnectedException : Exception
    {

        public AlreadyConnectedException()
        { }

        public AlreadyConnectedException(string message) : base(message)
        { }

        public AlreadyConnectedException(string message, Exception innerException) : base(message, innerException)
        { }

    }

}
