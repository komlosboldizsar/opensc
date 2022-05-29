namespace OpenSC.Library.BmdVideohub
{
    public record Label(int? Index, string Text)
    {
        public string ToProtocolStr() => $"{Index ?? 0} {Text ?? ""}";
        public static Label FromProtocolStr(string str)
        {
            string[] parts = str.Split(' ', 2);
            string text = "";
            if (parts.Length == 2)
                text = parts[1];
            int? index = null;
            if (int.TryParse(parts[0], out int _index))
                index = _index;
             return new Label(index, text);
        }
    }
}
