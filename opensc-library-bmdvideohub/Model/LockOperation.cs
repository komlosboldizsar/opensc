namespace OpenSC.Library.BmdVideohub
{

    public enum LockOperation
    {
        Lock,
        Unlock,
        ForceUnlock
    }

    public static class LockOperationExtensions
    {

        public static char ToLetter(this LockOperation? operation)
            => operation switch
            {
                LockOperation.Lock => 'O',
                LockOperation.Unlock => 'U',
                LockOperation.ForceUnlock => 'F',
                _ => 'X'
            };

        public static LockOperation? FromLetter(char letter)
            => letter switch
            {
                'O' => LockOperation.Lock,
                'U' => LockOperation.Unlock,
                'F' => LockOperation.ForceUnlock,
                _ => null
            };

    }

}
