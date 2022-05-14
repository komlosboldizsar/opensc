namespace OpenSC.Library.BmdVideohub
{
    public record Crosspoint(int? Output, int? Input)
    {
        public string ToProtocolStr() => $"{Output ?? 0} {Input ?? 0}";
        public static Crosspoint FromProtocolStr(string str)
        {
            string[] parts = str.Split(' ');
            if (parts.Length != 2)
                return null;
            int? output = null, input = null;
            if (int.TryParse(parts[0], out int _output))
                output = _output;
            if (int.TryParse(parts[1], out int _input))
                input = _input;
             return new Crosspoint(output, input);
        }
    }
}
