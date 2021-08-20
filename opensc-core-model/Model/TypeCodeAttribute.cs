using System;

namespace OpenSC.Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TypeCodeAttribute : Attribute
    {
        public string Code { get; private set; }
        public TypeCodeAttribute(string Code)
        {
            this.Code = Code;
        }
    }
}
