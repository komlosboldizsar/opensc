using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    public class SerializerRegister
    {

        private static readonly IInstantiatingXmlSerializer[] commonInstantiatingSerializers = new IInstantiatingXmlSerializer[]
        {
            new ColorXmlSerializer()
        };

        private static readonly IValueOnlyXmlSerializer[] commonValueOnlySerializers = new IValueOnlyXmlSerializer[]
        { };

        private static Dictionary<Type, IInstantiatingXmlSerializer> registeredInstantiatingDeserializers = new();
        private static Dictionary<Type, IValueOnlyXmlSerializer> registeredValueOnlyDeserializers = new();
      
        static SerializerRegister()
        {
            foreach (IInstantiatingXmlSerializer instantiatingSerializer in commonInstantiatingSerializers)
                RegisterSerializer(instantiatingSerializer);
            foreach (IValueOnlyXmlSerializer valueOnlySerializer in commonValueOnlySerializers)
                RegisterSerializer(valueOnlySerializer);
        }

        public static void RegisterSerializer(IInstantiatingXmlSerializer instantiatingSerializer)
            => registeredInstantiatingDeserializers.Add(instantiatingSerializer.Type, instantiatingSerializer);

        public static void RegisterSerializer(IValueOnlyXmlSerializer valueOnlySerializer)
            => registeredValueOnlyDeserializers.Add(valueOnlySerializer.Type, valueOnlySerializer);

        public static IInstantiatingXmlSerializer GetInstantingSerializer(Type type)
        {
            if (!registeredInstantiatingDeserializers.TryGetValue(type, out IInstantiatingXmlSerializer foundInstantingSerializer))
                return null;
            return foundInstantingSerializer;
        }

        public static IValueOnlyXmlSerializer GetValueOnlySerializer(Type type)
        {
            if (!registeredValueOnlyDeserializers.TryGetValue(type, out IValueOnlyXmlSerializer foundValueOnlySerializer))
                return null;
            return foundValueOnlySerializer;
        }

        public static (IInstantiatingXmlSerializer, IValueOnlyXmlSerializer) GetDeserializer(Type type, bool lookForInstanting = true, bool lookForValueOnly = true)
        {
            if (lookForInstanting && registeredInstantiatingDeserializers.TryGetValue(type, out IInstantiatingXmlSerializer instantingSerializer))
                return (instantingSerializer, null);
            if (lookForValueOnly && registeredValueOnlyDeserializers.TryGetValue(type, out IValueOnlyXmlSerializer valueOnlySerializer))
                return (null, valueOnlySerializer);
            return (null, null);
        }

        public static IXmlSerializer GetSerializer(Type type)
        {
            if (registeredInstantiatingDeserializers.TryGetValue(type, out IInstantiatingXmlSerializer instantingSerializer))
                return instantingSerializer;
            if (registeredValueOnlyDeserializers.TryGetValue(type, out IValueOnlyXmlSerializer valueOnlySerializer))
                return valueOnlySerializer;
            return null;
        }

    }
}
