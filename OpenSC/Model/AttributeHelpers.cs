using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{

    static class AttributeHelpers
    {

        public static TAttribute GetAttribute<TAttribute>(this Type type) where TAttribute : Attribute
            => type.GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault();

        public static string GetTypeLabel(this Type type)
        {
            foreach(object attribute in type.GetCustomAttributes(true))
            {
                TypeLabelAttribute typedAttribute = attribute as TypeLabelAttribute;
                if (typedAttribute != null)
                    return typedAttribute.Label;
            }
            return string.Empty;
        }

        public static string GetTypeCode(this Type type)
        {
            foreach (object attribute in type.GetCustomAttributes(true))
            {
                TypeCodeAttribute typedAttribute = attribute as TypeCodeAttribute;
                if (typedAttribute != null)
                    return typedAttribute.Code;
            }
            return string.Empty;
        }

        public static string GetName(this IDatabaseBase database)
        {
            object[] databaseNameAttributes = database.GetType().GetCustomAttributes(typeof(DatabaseNameAttribute), true);
            if (databaseNameAttributes.Length <= 0)
                return null;
            return (databaseNameAttributes[0] as DatabaseNameAttribute)?.Name;
        }

    }

}
