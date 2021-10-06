using System;

namespace OpenSC.Model.Macros
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MacroCommandAttribute : Attribute
    {
        public readonly string Code;
        public readonly string Name;
        public readonly string Description;
        public MacroCommandAttribute(string Code, string Name, string Description)
        {
            this.Code = Code;
            this.Name = Name;
            this.Description = Description;
        }
    }
}
