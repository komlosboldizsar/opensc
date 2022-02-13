using System;

namespace OpenSC.GUI.WorkspaceManager.ValueConverters
{

    [AttributeUsage(AttributeTargets.Class)]
    class ConverterTypeAttribute: Attribute
    {

        public Type Type { get; private set; }

        public string TypeName { get; private set; }

        public ConverterTypeAttribute(Type Type, string TypeName)
        {
            this.Type = Type;
            this.TypeName = TypeName;
        }

    }

}
