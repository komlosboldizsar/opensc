using System;
using Microsoft.CodeAnalysis;

namespace OpenSC.Model.SourceGenerators
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed partial class AutoProperty : Attribute, IAutoPropertyAttribute
    {
        public string PropertyName;
        public Accessibility PropertyAccessibility;
        public Accessibility GetterAccessibility;
        public Accessibility SetterAccessibility;
        public bool IsVirtual;
    }
}