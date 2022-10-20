using System;

namespace OpenSC.Model.SourceGenerators
{
    public partial class AutoProperty
    {
        [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
        public class AfterChange : Attribute, IAutoPropertyAttribute
        {
            public string MethodName;
            public AfterChange(string MethodName) => this.MethodName = MethodName;
            private AfterChange() { }
        }
    }
}
