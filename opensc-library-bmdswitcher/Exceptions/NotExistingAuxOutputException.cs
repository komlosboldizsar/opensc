using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMD.Switcher.Exceptions
{

    public class NotExistingAuxOutputException : Exception
    {

        public NotExistingAuxOutputException()
        { }

        public NotExistingAuxOutputException(string message) : base(message)
        { }

        public NotExistingAuxOutputException(string message, Exception innerException) : base(message, innerException)
        { }

    }

}
