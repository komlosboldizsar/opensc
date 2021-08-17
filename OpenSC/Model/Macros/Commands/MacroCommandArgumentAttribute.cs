using System;

namespace OpenSC.Model.Macros
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MacroCommandArgumentAttribute : Attribute
    {
        public readonly int Index;
        public readonly string Name;
        public readonly string Description;
        public readonly Type ObjectType;
        public readonly MacroArgumentKeyType KeyType;
        public MacroCommandArgumentAttribute(int Index, string Name, string Description, Type ObjectType, MacroArgumentKeyType KeyType)
        {
            this.Index = Index;
            this.Name = Name;
            this.Description = Description;
            this.ObjectType = ObjectType;
            this.KeyType = KeyType;
        }
    }
}
