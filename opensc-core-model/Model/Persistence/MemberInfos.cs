using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    internal static class MemberInfos
    {

        private static readonly Dictionary<Type, ExtendedMemberInfo[]> extendedMemberInfosForTypes = new();
        private static readonly Dictionary<Type, Dictionary<string, ExtendedMemberInfo>> extendedMemberInfosByNameForTypes = new();

        public static ExtendedMemberInfo[] GetExtendedMemberInfos(this Type type)
        {
            if (type == null)
                return Array.Empty<ExtendedMemberInfo>();
            ExtendedMemberInfo[] extendedMemberInfos;
            while (!extendedMemberInfosForTypes.TryGetValue(type, out extendedMemberInfos))
                collectExtendedMemberInfosForType(type);
            return extendedMemberInfos;
        }

        public static Dictionary<string, ExtendedMemberInfo> GetExtendedMemberInfosByName(this Type type)
        {
            if (type == null)
                return new Dictionary<string, ExtendedMemberInfo>();
            Dictionary<string, ExtendedMemberInfo> extendedMemberInfosByName;
            while (!extendedMemberInfosByNameForTypes.TryGetValue(type, out extendedMemberInfosByName))
                collectExtendedMemberInfosForType(type);
            return extendedMemberInfosByName;
        }

        public static ExtendedMemberInfo GetExtendedMemberInfo(this Type type, string name)
        {
            type.GetExtendedMemberInfosByName().TryGetValue(name, out ExtendedMemberInfo extendedMemberInfo);
            return extendedMemberInfo;
        }

        private const BindingFlags memberCollectLookupBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

        private static void collectExtendedMemberInfosForType(Type type)
        {
            IEnumerable<ExtendedMemberInfo> membersOfType = type
                .GetMembers(memberCollectLookupBindingFlags)
                .Where(mi => ((mi is FieldInfo) || (mi is PropertyInfo)))
                .Select(mi => new ExtendedMemberInfo(mi))
                .Where(emi => emi.GetPersistAsAttributeForDimension(0) != null);
            List<ExtendedMemberInfo> extendedMemberInfos = new();
            extendedMemberInfos.AddRange(type.BaseType.GetExtendedMemberInfos());
            extendedMemberInfos.AddRange(membersOfType);
            Dictionary<string, ExtendedMemberInfo> extendedMemberInfosByName = new();
            foreach (ExtendedMemberInfo extendedMemberInfo in extendedMemberInfos)
                extendedMemberInfosByName[extendedMemberInfo.MemberInfo.Name] = extendedMemberInfo;
            IEnumerable<AutoProperty.GeneratedForField> generatedForFieldAttributes = extendedMemberInfos
                .Where(emi => emi.GeneratedForFieldAttribute != null)
                .Select(emi => emi.GeneratedForFieldAttribute);
            foreach (AutoProperty.GeneratedForField generatedForFieldAttribute in generatedForFieldAttributes)
                extendedMemberInfosByName.Remove(generatedForFieldAttribute.FieldName);
            extendedMemberInfosForTypes.Add(type, extendedMemberInfosByName.Values.ToArray());
            extendedMemberInfosByNameForTypes.Add(type, extendedMemberInfosByName);
        }

    }
}
