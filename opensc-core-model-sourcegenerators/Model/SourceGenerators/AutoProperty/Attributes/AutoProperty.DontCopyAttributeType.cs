using System;

namespace OpenSC.Model.SourceGenerators
{
    public partial class AutoProperty
    {
        [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
        public sealed class DontCopyAttributeType : Attribute, IAutoPropertyAttribute
        {
            public Type[] Types;
            public DontCopyAttributeType(params Type[] Types) => this.Types = Types;
            private DontCopyAttributeType() { }
        }
    }
}