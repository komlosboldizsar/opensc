using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMD.Switcher.Exceptions
{

    public class CouldNotConnectException : Exception
    {

        public CouldNotConnectException()
        { }

        public CouldNotConnectException(string message) : base(message)
        { }

        public CouldNotConnectException(string message, Exception innerException) : base(message, innerException)
        { }

    }

}
