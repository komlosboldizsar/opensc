using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public static partial class RouterOutputLockStrings
    {

        public static RouterOutputLockTypeFormatter Formatter => RouterOutputLockTypeFormatter.Instance;

        public class RouterOutputLockTypeFormatter : IFormatProvider, ICustomFormatter
        {

            private RouterOutputLockTypeFormatter() { }
            internal static RouterOutputLockTypeFormatter Instance { get; } = new();

            public object GetFormat(Type formatType) => (formatType == typeof(ICustomFormatter)) ? this : null;

            // @source https://docs.microsoft.com/en-us/dotnet/api/system.iformatprovider?view=net-6.0
            public string Format(string format, object argument, IFormatProvider formatProvider)
            {
                if (argument.GetType() != typeof(RouterOutputLockType))
                {
                    try
                    {
                        return handleOtherFormats(format, argument);
                    }
                    catch (FormatException e)
                    {
                        throwFormatException(e, format);
                    }
                }
                if (!format.EndsWith(STR_LOWERCASE) && !format.EndsWith(STR_UPPERCASE))
                {
                    int underscorePosition;
                    while ((underscorePosition = format.IndexOf('_')) != -1)
                    {
                        string newFormat = format.Substring(0, underscorePosition);
                        try
                        {
                            char afterUnderscore = format[underscorePosition + 1];
                            newFormat += char.ToUpper(afterUnderscore);
                        }
                        catch { }
                        try
                        {
                            newFormat += format.Substring(underscorePosition + 2);
                        }
                        catch { }
                        format = newFormat;
                    }
                    try
                    {
                        char firstLetter = format[0];
                        if (char.IsUpper(firstLetter))
                        {
                            format += STR_UPPERCASE;
                        }
                        else if (char.IsLower(firstLetter))
                        {
                            format += STR_LOWERCASE;
                            format = char.ToUpper(format[0]) + format.Substring(1);
                        }

                    }
                    catch { }
                }
                if (Enum.TryParse(typeof(Variant), format, out object variant))
                    return Get((RouterOutputLockType)argument, (Variant)variant);
                else
                    return $"[{argument}]";
            }

            private const string STR_LOWERCASE = "Lowercase";
            private const string STR_UPPERCASE = "Uppercase";

            private void throwFormatException(FormatException e, string format) => throw new FormatException($"The format of '{format}' is invalid.", e);

            private string handleOtherFormats(string format, object arg)
            {
                if (arg is IFormattable formattableArg)
                    return formattableArg.ToString(format, CultureInfo.CurrentCulture);
                return arg?.ToString() ?? string.Empty;
            }

        }

    }
}
