using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    public class SerializerRegister
    {

        private static readonly IValueXmlSerializer[] commonSerializers = new IValueXmlSerializer[]
        {
            new ColorXmlSerializer()
        };

        private static Dictionary<Type, IValueXmlSerializer> registeredSerializers = new();
      
        static SerializerRegister()
        {
            foreach (IValueXmlSerializer serializer in commonSerializers)
                registeredSerializers.Add(serializer.Type, serializer);
        }

        public static void RegisterSerializer(IValueXmlSerializer serializer)
            => registeredSerializers.Add(serializer.Type, serializer);

        public static IValueXmlSerializer GetSerializerForType(Type type)
        {
            if (!registeredSerializers.TryGetValue(type, out IValueXmlSerializer foundSerializer))
                return null;
            return foundSerializer;
        }

    }
}
