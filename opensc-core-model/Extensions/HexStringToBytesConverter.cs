using System;

namespace OpenSC.Extensions
{
    public static class HexStringToBytesConverter
    {

        public static byte[] HexToBytes(this string text)
        {
            text = text.Replace(" ", "").Replace("\t", "");
            int textLength = text.Length;
            if (textLength % 2 != 0)
                throw new ArgumentException("Odd number of hex characters", nameof(text));
            int bytesLength = textLength / 2;
            byte[] byteArray = new byte[bytesLength];
            for (int b = 0, c = 0; b < byteArray.Length; b++)
            {
                uint nibbleH = lookup[text[c++]];
                uint nibbleL = lookup[text[c++]];
                if ((nibbleH == LUT_HEXTOUINT_INVALID) || (nibbleL == LUT_HEXTOUINT_INVALID))
                    throw new ArgumentException($"Invalid hex character", nameof(text));
                byteArray[b] = (byte)((nibbleH << 4) + nibbleL);
            }
            return byteArray;
        }

        private const int LUT_HEXTOUINT_INVALID = 255;
        private static readonly uint[] lookup = createLookup();

        private static uint[] createLookup()
        {
            uint[] lut = new uint[256];
            for (int chr = 0; chr < 256; chr++)
            {
                if ((chr >= '0') && (chr <= '9'))
                    lut[chr] = (uint)(chr - '0');
                else if ((chr >= 'a') && (chr <= 'f'))
                    lut[chr] = (uint)(chr - 'a' + 10);
                else if ((chr >= 'A') && (chr <= 'F'))
                    lut[chr] = (uint)(chr - 'A' + 10);
                else
                    lut[chr] = LUT_HEXTOUINT_INVALID;
            }
            return lut;
        }

    }
}
