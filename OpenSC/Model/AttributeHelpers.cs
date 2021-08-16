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
            => type.GetAttribute<TypeLabelAttribute>()?.Label;

        public static string GetTypeCode(this Type type)
            => type.GetAttribute<TypeCodeAttribute>()?.Code;

        public static string GetName(this IDatabaseBase database)
            => database.GetType().GetAttribute<DatabaseNameAttribute>()?.Name;

    }

}
