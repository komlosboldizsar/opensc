using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public class RouterOutputLockOperationException : Exception
    {

        public RouterOutputLock OperatedLock { get; private init; }
        public RouterOutputLockOperationType Operation { get; private init; }

        public RouterOutputLockOperationException(string message, RouterOutputLock operatedLock, RouterOutputLockOperationType operation)
            : this(message, null, operatedLock, operation) { }
        
        public RouterOutputLockOperationException(string message, Exception innerException, RouterOutputLock operatedLock, RouterOutputLockOperationType operation)
            : base(message, innerException)
        {
            OperatedLock = operatedLock;
            Operation = operation;
        }

    }

}
