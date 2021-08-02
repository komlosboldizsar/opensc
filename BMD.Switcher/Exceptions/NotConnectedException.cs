using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMD.Switcher.Exceptions
{

    public class NotConnectedException : Exception
    {

        public NotConnectedException()
        { }

        public NotConnectedException(string message) : base(message)
        { }

        public NotConnectedException(string message, Exception innerException) : base(message, innerException)
        { }

    }

}
