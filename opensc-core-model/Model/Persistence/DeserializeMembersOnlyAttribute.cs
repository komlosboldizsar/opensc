using System;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage((AttributeTargets.Field | AttributeTargets.Property), AllowMultiple = true)]
    public class DeserializeMembersOnlyAttribute : Attribute
    { }
}
