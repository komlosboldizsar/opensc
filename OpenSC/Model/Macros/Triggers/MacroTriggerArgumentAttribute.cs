using System;

namespace OpenSC.Model.Macros
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MacroTriggerArgumentAttribute : Attribute
    {
        public readonly int Index;
        public readonly string Name;
        public readonly string Description;
        public MacroTriggerArgumentAttribute(int Index, string Name, string Description)
        {
            this.Index = Index;
            this.Name = Name;
            this.Description = Description;
        }
    }
}
