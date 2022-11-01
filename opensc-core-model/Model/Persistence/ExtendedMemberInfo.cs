using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    internal class ExtendedMemberInfo
    {

        public readonly MemberInfo MemberInfo;
        public readonly FieldInfo FieldInfo;
        public readonly PropertyInfo PropertyInfo;
        public readonly Type ValueType;
        public readonly PersistAsAttribute[] PersistAsAttributes;
        public readonly AutoProperty.GeneratedForField GeneratedForFieldAttribute;
        public readonly PersistDetailedAttribute PersistDetailedAttribute;
        public readonly PersistSubclassAttribute[] PersistSubclassAttributes;
        public readonly PolymorphFieldAttribute PolymorphFieldAttribute;
        public readonly DeserializeMembersOnlyAttribute DeserializeMembersOnlyAttribute;
        public readonly bool RequiresRelationBuilding;
        public readonly bool CanDeserializeElement;

        public ExtendedMemberInfo(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo;
            FieldInfo = memberInfo as FieldInfo;
            PropertyInfo = memberInfo as PropertyInfo;
            ValueType = (FieldInfo != null) ? FieldInfo.FieldType : PropertyInfo.PropertyType;
            PersistAsAttributes = collectDimensionedAttribute<PersistAsAttribute>(memberInfo);
            GeneratedForFieldAttribute = memberInfo.GetCustomAttribute<AutoProperty.GeneratedForField>();
            PersistDetailedAttribute = memberInfo.GetCustomAttribute<PersistDetailedAttribute>();
            PersistSubclassAttributes = collectDimensionedAttribute<PersistSubclassAttribute>(memberInfo);
            PolymorphFieldAttribute = memberInfo.GetCustomAttribute<PolymorphFieldAttribute>();
            DeserializeMembersOnlyAttribute = memberInfo.GetCustomAttribute<DeserializeMembersOnlyAttribute>();
            var associationTypeData = ValueType.IsAssociationTypeComplex();
            bool hasPersistDetailedAttribute = PersistDetailedAttribute != null;
            RequiresRelationBuilding = associationTypeData.TypeIs && !hasPersistDetailedAttribute;
            CanDeserializeElement = !associationTypeData.TypeIs || (associationTypeData.ElementIs == false) || hasPersistDetailedAttribute;
        }

        private TAttribute[] collectDimensionedAttribute<TAttribute>(MemberInfo memberInfo)
            where TAttribute : Attribute, IDimensionedPersistAttribute
        {
            IEnumerable<TAttribute> attributes = memberInfo.GetCustomAttributes<TAttribute>();
            if (attributes.Count() == 0)
                return null;
            int dimensionMax = attributes.Max(paa => paa.Dimension);
            TAttribute[] attributesArray = new TAttribute[dimensionMax + 1];
            foreach (TAttribute attribute in attributes)
                attributesArray[attribute.Dimension] = attribute;
            return attributesArray;
        }

        public PersistAsAttribute GetPersistAsAttributeForDimension(int dimension)
            => getTAttributeForDimension(PersistAsAttributes, dimension);

        public PersistSubclassAttribute GetPersistSubclassAttributeForDimension(int dimension)
            => getTAttributeForDimension(PersistSubclassAttributes, dimension);

        private TAttribute getTAttributeForDimension<TAttribute>(TAttribute[] attributeArray, int dimension)
            where TAttribute : Attribute
            => ((attributeArray == null) || (attributeArray.Length <= dimension)) ? null : attributeArray[dimension];
        
        public object GetValue(object item)
            => (FieldInfo != null) ? FieldInfo.GetValue(item) : PropertyInfo.GetValue(item);

        public void SetValue(object item, object value)
        {
            if (FieldInfo != null)
                FieldInfo.SetValue(item, value);
            else
                PropertyInfo.SetValue(item, value);
        }

    };
}
