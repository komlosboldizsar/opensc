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
        public readonly PersistDetailedAttribute PersistDetailedAttribute;
        public readonly PersistSubclassAttribute PersistSubclassAttribute;
        public readonly PolymorphFieldAttribute PolymorphFieldAttribute;
        public readonly bool RequiresRelationBuilding;
        public readonly bool CanDeserializeElement;

        public ExtendedMemberInfo(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo;
            FieldInfo = memberInfo as FieldInfo;
            PropertyInfo = memberInfo as PropertyInfo;
            ValueType = (FieldInfo != null) ? FieldInfo.FieldType : PropertyInfo.PropertyType;
            IEnumerable<PersistAsAttribute> persistAsAttributes = memberInfo.GetCustomAttributes<PersistAsAttribute>();
            if (persistAsAttributes.Count() > 0)
            {
                int dimensionMax = persistAsAttributes.Max(paa => paa.Dimension);
                PersistAsAttributes = new PersistAsAttribute[dimensionMax + 1];
                foreach (PersistAsAttribute persistAsAttribute in persistAsAttributes)
                    PersistAsAttributes[persistAsAttribute.Dimension] = persistAsAttribute;
            }
            else
            {
                PersistAsAttributes = null;
            }
            PersistDetailedAttribute = memberInfo.GetCustomAttribute<PersistDetailedAttribute>();
            PersistSubclassAttribute = memberInfo.GetCustomAttribute<PersistSubclassAttribute>();
            PolymorphFieldAttribute = memberInfo.GetCustomAttribute<PolymorphFieldAttribute>();
            var associationTypeData = ValueType.IsAssociationTypeComplex();
            bool hasPersistDetailedAttribute = PersistDetailedAttribute != null;
            RequiresRelationBuilding = associationTypeData.TypeIs && !hasPersistDetailedAttribute;
            CanDeserializeElement = !associationTypeData.TypeIs || (associationTypeData.ElementIs == false) || hasPersistDetailedAttribute;
        }

        public PersistAsAttribute GetPersistAsAttributeForDimension(int dimension)
            => ((PersistAsAttributes == null) || (PersistAsAttributes.Length <= dimension)) ? null : PersistAsAttributes[dimension];

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
