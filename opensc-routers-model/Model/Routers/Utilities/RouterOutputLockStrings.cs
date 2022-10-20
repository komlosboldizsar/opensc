using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public static partial class RouterOutputLockStrings
    {

        public enum Variant
        {
            DoLowercase,
            DoUppercase,
            DoingLowercase,
            DoingUppercase,
            DoneLowercase,
            DoneUppercase,
            UndoLowercase,
            UndoUppercase,
            UndoingLowercase,
            UndoingUppercase,
            UndoneLowercase,
            UndoneUppercase,
            ForceUndoLowercase,
            ForceUndoUppercase,
            ForceUndoingLowercase,
            ForceUndoingUppercase,
            ForceUndoneLowercase,
            ForceUndoneUppercase,
            UnknownLowercase = 99,
            UnknownUppercase
        }

    }
}
