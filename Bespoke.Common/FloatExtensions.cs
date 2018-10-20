using System;

namespace Bespoke.Common
{
    /// <summary>
    /// Extesion methods for <see cref="System.Single"/>.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>The angle in degrees.</returns>
        public static float ToDegrees(this float radians)
        {
            return (radians * 57.29578f);
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The angle in radians</returns>
        public static float ToRadians(this float degrees)
        {
            return (degrees * 0.01745329f);
        }
    }
}
