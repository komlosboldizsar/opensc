using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSC.Model.SourceGenerators
{
    internal static class StringExtensions
    {
        public static string NullAsString(this string str) => str ?? STR_NULL;
        public const string STR_NULL = "null";
    }
}
