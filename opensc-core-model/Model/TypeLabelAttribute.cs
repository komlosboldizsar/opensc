using System;

namespace OpenSC.Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TypeLabelAttribute: Attribute
    {
        public string Label { get; private set; }
        public TypeLabelAttribute(string Label)
        {
            this.Label = Label;
        }
    }
}
