using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public static partial class RouterOutputLockStrings
    {

        public static string GetLockStateSentence(RouterOutputLock @lock)
        {
            string stateString;
            if (@lock.Supported)
            {
                stateString = "Output [{0}] is ";
                if ((@lock.OwnerKnowLevel == RouterOutputLockOwnerKnowLevel.Detailed) && (@lock.Owner != null))
                {
                    stateString += "{1:done} by {2}.";
                }
                else
                {
                    stateString += @lock.State switch
                    {
                        RouterOutputLockState.Clear => "not {1:done}.",
                        RouterOutputLockState.Locked => "{1:done}.",
                        RouterOutputLockState.LockedLocal => "{1:done} locally (by this application).",
                        RouterOutputLockState.LockedRemote => "{1:done} remotely (by another user).",
                        _ => "{1:done} by unknown."
                    };
                }
            }
            else
            {
                stateString = "{1:Doing} output [{0}] is not supported.";
            }
            return string.Format(Formatter, stateString, @lock.Output, @lock.Type, @lock.Owner?.ToString() ?? "?");
        }

        public static string GetStateSentence(this RouterOutputLock @lock) => GetLockStateSentence(@lock);

        public static string GetLockForceUndoQuestion(RouterOutputLock @lock)
            => $"Do you want to {GetDo(@lock.Type, RouterOutputLockOperationType.ForceUnlock, false)}?";

        public static string GetForceUndoQuestion(this RouterOutputLock @lock) => GetLockForceUndoQuestion(@lock);

    }
}
