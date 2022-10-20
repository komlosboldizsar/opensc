using System;
namespace OpenSC.Model.SourceGenerators
{
    public partial class AutoProperty
    {
        [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
        public sealed class Event : Attribute, IAutoPropertyAttribute
        {
            public Event() { }
            public Event(string EventName) => this.EventName = EventName;
            public Event(bool NoEventGeneration) => this.NoEventGeneration = NoEventGeneration;
            public Event(Type SenderType) => this.SenderType = SenderType;
            public string EventName;
            public bool NoEventGeneration;
            public Type SenderType;
        }
    }
}