using System;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DeserializeMembersOnlyAttribute : Attribute
    { }
}
