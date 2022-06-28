using System;

namespace OpenSC.Model.SourceGenerators
{
    public partial class AutoProperty
    {
        [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public sealed class GeneratedForField : Attribute, IAutoPropertyAttribute
        {
            public string FieldName;
            public GeneratedForField(string FieldName) => this.FieldName = FieldName;
        }
    }
}