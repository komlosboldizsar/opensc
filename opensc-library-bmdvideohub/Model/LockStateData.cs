namespace OpenSC.Library.BmdVideohub
{
    public record LockStateData(int? Output, LockState? State)
    {
        public string ToProtocolStr() => $"{Output ?? 0} {State.ToLetter()}";
        public static LockStateData FromProtocolStr(string str)
        {
            string[] parts = str.Split(' ');
            if (parts.Length != 2)
                return null;
            int? output = null;
            if (int.TryParse(parts[0], out int _output))
                output = _output;
            return new LockStateData(output, LockStateExtensions.FromLetter(parts[1][0]));
        }
    }
}
