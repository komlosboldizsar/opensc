using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMD.Switcher.Exceptions
{

    public class NotExistingSourceException : Exception
    {

        public NotExistingSourceException()
        { }

        public NotExistingSourceException(string message) : base(message)
        { }

        public NotExistingSourceException(string message, Exception innerException) : base(message, innerException)
        { }

    }

}
