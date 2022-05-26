using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public class RouterOutputLockedByOtherUserException : RouterOutputLockOperationException
    {

        public RouterOutputLockedByOtherUserException(RouterOutputLock lockToPlace)
            : base(getMessage(lockToPlace), lockToPlace, RouterOutputLockOperationType.Lock)
        { }

        private static string getMessage(RouterOutputLock lockToPlace)
            => string.Format(RouterOutputLockStrings.Formatter,
                    "Output {0} is {1:done} by another user! You must use the force {1:undo} function before {1:doing}.",
                    lockToPlace.Output,
                    lockToPlace.Type);

    }

}
