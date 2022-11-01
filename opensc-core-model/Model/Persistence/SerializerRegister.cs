using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    public class SerializerRegister
    {

        private static readonly ICompleteXmlSerializer[] commonCompleteSerializers = new ICompleteXmlSerializer[]
        {
            new ColorXmlSerializer()
        };

        private static readonly IMemberXmlSerializer[] commonMemberSerializers = new IMemberXmlSerializer[]
        { };

        private static Dictionary<Type, ICompleteXmlSerializer> registeredCompleteSerializers = new();
        private static Dictionary<Type, IMemberXmlSerializer> registeredMemberSerializers = new();
      
        static SerializerRegister()
        {
            foreach (ICompleteXmlSerializer completeSerializer in commonCompleteSerializers)
                registeredCompleteSerializers.Add(completeSerializer.Type, completeSerializer);
            foreach (IMemberXmlSerializer memberSerializer in commonMemberSerializers)
                registeredMemberSerializers.Add(memberSerializer.Type, memberSerializer);
        }

        public static void RegisterCompleteSerializer(ICompleteXmlSerializer serializer)
            => registeredCompleteSerializers.Add(serializer.Type, serializer);

        public static void RegisterMemberSerializer(IMemberXmlSerializer serializer)
            => registeredMemberSerializers.Add(serializer.Type, serializer);

        public static ICompleteXmlSerializer GetCompleteSerializerForType(Type type)
        {
            if (!registeredCompleteSerializers.TryGetValue(type, out ICompleteXmlSerializer foundCompleteSerializer))
                return null;
            return foundCompleteSerializer;
        }

        public static IMemberXmlSerializer GetMemberSerializerForType(Type type)
        {
            if (!registeredMemberSerializers.TryGetValue(type, out IMemberXmlSerializer foundMemberSerializer))
                return null;
            return foundMemberSerializer;
        }

    }
}
