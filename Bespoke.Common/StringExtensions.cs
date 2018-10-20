using System;

namespace Bespoke.Common
{
    /// <summary>
    /// Extesion methods for <see cref="System.String"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determine if a string represents a numeric value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <returns>true if <paramref name="value"/> represents a numeric value; otherwise, false.</returns>
        public static bool IsNumeric(this string value)
        {
            int result;
            return IsNumeric(value, out result);
        }

        /// <summary>
        /// Determine if a string represents a numeric value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="result">The numeric value potentially stored within the string.</param>
        /// <returns>true if <paramref name="value"/> represents a numeric value; otherwise, false.</returns>
        public static bool IsNumeric(this string value, out int result)
        {
            return int.TryParse(value, out result);
        }
    }
}
