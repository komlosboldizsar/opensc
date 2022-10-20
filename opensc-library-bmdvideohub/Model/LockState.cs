namespace OpenSC.Library.BmdVideohub
{

    public enum LockState
    {
        Unlocked,
        Owned,
        Taken
    }

    public static class LockStateExtensions
    {

        public static char ToLetter(this LockState? state)
            => state switch
            {
                LockState.Unlocked => 'U',
                LockState.Owned => 'O',
                LockState.Taken => 'L',
                _ => 'X'
            };

        public static LockState? FromLetter(char letter)
            => letter switch
            {
                'U' => LockState.Unlocked,
                'O' => LockState.Owned,
                'L' => LockState.Taken,
                _ => null
            };

    }

}
