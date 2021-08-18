using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMD.Switcher.Exceptions
{

    public class NotExistingMixEffectBlockException : Exception
    {

        public NotExistingMixEffectBlockException()
        { }

        public NotExistingMixEffectBlockException(string message) : base(message)
        { }

        public NotExistingMixEffectBlockException(string message, Exception innerException) : base(message, innerException)
        { }

    }

}
