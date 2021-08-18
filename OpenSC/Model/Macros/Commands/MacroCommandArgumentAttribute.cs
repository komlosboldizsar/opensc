using System;

namespace OpenSC.Model.Macros
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MacroCommandArgumentAttribute : Attribute
    {
        public readonly int Index;
        public readonly string Name;
        public readonly string Description;
        public MacroCommandArgumentAttribute(int Index, string Name, string Description)
        {
            this.Index = Index;
            this.Name = Name;
            this.Description = Description;
        }
    }
}
