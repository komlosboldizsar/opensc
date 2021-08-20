using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{

    public static class AttributeHelpers
    {

        public static TAttribute GetAttribute<TAttribute>(this Type type) where TAttribute : Attribute
            => type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;

        public static string GetTypeLabel(this Type type)
            => type.GetAttribute<TypeLabelAttribute>()?.Label;

        public static string GetTypeCode(this Type type)
            => type.GetAttribute<TypeCodeAttribute>()?.Code;

        public static string GetName(this IDatabaseBase database)
            => database.GetType().GetAttribute<DatabaseNameAttribute>()?.Name;

    }

}
