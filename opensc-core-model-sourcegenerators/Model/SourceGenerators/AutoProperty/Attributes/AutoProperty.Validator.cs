using System;

namespace OpenSC.Model.SourceGenerators
{
    public partial class AutoProperty
    {
        [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
        public class Validator : Attribute, IAutoPropertyAttribute
        {
            public string MethodName;
            public Validator(string MethodName) => this.MethodName = MethodName;
            private Validator() { }
        }
    }
}
