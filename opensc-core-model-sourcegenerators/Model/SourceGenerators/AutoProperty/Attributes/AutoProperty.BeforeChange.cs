using System;

namespace OpenSC.Model.SourceGenerators
{
    public partial class AutoProperty
    {
        [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
        public class BeforeChange : Attribute, IAutoPropertyAttribute
        {
            public string MethodName;
            public BeforeChange(string MethodName) => this.MethodName = MethodName;
            private BeforeChange() { }
        }
    }
}
