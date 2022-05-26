using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public static partial class RouterOutputLockStrings
    {

        public static readonly StringCollection Lock = new LockStringCollection();
        public static readonly StringCollection Protect = new ProtectStringCollection();
        private static readonly StringCollection Unknown = new UnknownStringCollection();

        public abstract class StringCollection
        {
            public abstract string this[Variant variant] { get; }
            protected const string STR_UNKNOWN = "?";
        }

        private class LockStringCollection : StringCollection
        {
            public override string this[Variant variant] => variant switch
            {
                Variant.DoLowercase => "lock",
                Variant.DoUppercase => "Lock",
                Variant.DoingLowercase => "locking",
                Variant.DoingUppercase => "Locking",
                Variant.DoneLowercase => "locked",
                Variant.DoneUppercase => "Locked",
                Variant.UndoLowercase => "unlock",
                Variant.UndoUppercase => "Unlock",
                Variant.UndoingLowercase => "unlocking",
                Variant.UndoingUppercase => "Unlocking",
                Variant.UndoneLowercase => "unlocked",
                Variant.UndoneUppercase => "Unlocked",
                Variant.ForceUndoLowercase => "force unlock",
                Variant.ForceUndoUppercase => "Force unlock",
                Variant.ForceUndoingLowercase => "force unlocking",
                Variant.ForceUndoingUppercase => "Force unlocking",
                Variant.ForceUndoneLowercase => "force unlocked",
                Variant.ForceUndoneUppercase => "Force unlocked",
                _ => STR_UNKNOWN
            };
        }

        private class ProtectStringCollection : StringCollection
        {
            public override string this[Variant variant] => variant switch
            {
                Variant.DoLowercase => "protect",
                Variant.DoUppercase => "Protect",
                Variant.DoingLowercase => "protecting",
                Variant.DoingUppercase => "Protecting",
                Variant.DoneLowercase => "protected",
                Variant.DoneUppercase => "Protected",
                Variant.UndoLowercase => "unprotect",
                Variant.UndoUppercase => "Unprotect",
                Variant.UndoingLowercase => "unprotecting",
                Variant.UndoingUppercase => "Unprotecting",
                Variant.UndoneLowercase => "unprotected",
                Variant.UndoneUppercase => "Unprotected",
                Variant.ForceUndoLowercase => "force unprotect",
                Variant.ForceUndoUppercase => "Force unprotect",
                Variant.ForceUndoingLowercase => "force unprotecting",
                Variant.ForceUndoingUppercase => "Force unprotecting",
                Variant.ForceUndoneLowercase => "force unprotected",
                Variant.ForceUndoneUppercase => "Force unprotected",
                _ => STR_UNKNOWN
            };
        }

        private class UnknownStringCollection : StringCollection
        {
            public override string this[Variant variant] => STR_UNKNOWN;
        }

    }
}
