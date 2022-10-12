namespace OpenSC.Extensions
{
    public static class BytesToHexStringConverter
    {

        public static string ToHexString(this byte[] bytes, bool spaces = false)
        {
            var lookup32 = lookup;
            char[] result = new char[bytes.Length * (spaces ? 3 : 2)];
            for (int i = 0, t = 0; i < bytes.Length; i++)
            {
                uint charValue = lookup32[bytes[i]];
                result[t++] = (char)charValue;
                result[t++] = (char)(charValue >> 16);
                if (spaces)
                    result[t++] = ' ';
            }
            return new string(result);
        }

        private static readonly uint[] lookup = createLookup();

        private static uint[] createLookup()
        {
            uint[] result = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                string s = i.ToString("X2");
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }
            return result;
        }

    }
}
