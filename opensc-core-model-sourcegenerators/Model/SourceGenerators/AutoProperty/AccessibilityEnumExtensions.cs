using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSC.Model.SourceGenerators
{
    internal static class AccessibilityEnumExtensions
    {
        public static string ToCode(this Accessibility accessibility)
        {
            switch (accessibility)
            {
                case Accessibility.NotApplicable:
                    return "";
                case Accessibility.Private:
                    return "private";
                case Accessibility.ProtectedAndInternal:
                    return "internal protected";
                case Accessibility.Protected:
                    return "protected";
                case Accessibility.Internal:
                    return "internal";
                case Accessibility.ProtectedOrInternal:
                    return "??";
                case Accessibility.Public:
                    return "public";
                default:
                    return "??";
            }
        }
    }
}
