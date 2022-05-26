using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public class RouterOutputHasOtherLockException : RouterOutputLockOperationException
    {

        public RouterOutputLock ConflictingLock { get; private init; }

        public RouterOutputHasOtherLockException(RouterOutputLock lockToPlace, RouterOutputLock lockThatConflicts)
            : base(getMessage(lockToPlace, lockThatConflicts), lockToPlace, RouterOutputLockOperationType.Lock)
        {
            ConflictingLock = lockThatConflicts;
        }

        private static string getMessage(RouterOutputLock lockToPlace, RouterOutputLock lockThatConflicts)
            => string.Format(RouterOutputLockStrings.Formatter,
                    "Output [{0}] is {2:done}! You must {2:undo} before {1:doing}.",
                    lockToPlace.Output,
                    lockToPlace.Type,
                    lockThatConflicts.Type);

    }

}
