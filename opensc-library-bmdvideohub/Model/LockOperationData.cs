namespace OpenSC.Library.BmdVideohub
{
    public record LockOperationData(int? Output, LockOperation? Operation)
    {
        public string ToProtocolStr() => $"{Output ?? 0} {Operation.ToLetter()}";
        public static LockOperationData FromProtocolStr(string str)
        {
            string[] parts = str.Split(' ');
            if (parts.Length != 2)
                return null;
            int? output = null;
            if (int.TryParse(parts[0], out int _output))
                output = _output;
            return new LockOperationData(output, LockOperationExtensions.FromLetter(parts[1][0]));
        }
    }
}
