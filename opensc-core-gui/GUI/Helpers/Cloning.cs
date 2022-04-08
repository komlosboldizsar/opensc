using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Helpers
{

    public static class Cloning
    {

        public static Control Clone(this Control baseInstance, string[] excludeProperties = null)
        {
            Type baseType = baseInstance.GetType();
            PropertyInfo[] controlProperties = baseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Control newInstance = (Control)Activator.CreateInstance(baseType);
            foreach (PropertyInfo propInfo in controlProperties)
                if (propInfo.CanWrite)
                    if ((propInfo.Name != "WindowTarget") && (excludeProperties?.Contains(propInfo.Name) != true))
                        propInfo.SetValue(newInstance, propInfo.GetValue(baseInstance, null), null);
            return newInstance;
        }

        // @source https://stackoverflow.com/a/10267292
        public static T CloneT<T>(this T baseInstance, string[] excludeProperties = null)
            where T : Control
        {
            PropertyInfo[] controlProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            T newInstance = Activator.CreateInstance<T>();
            foreach (PropertyInfo propInfo in controlProperties)
                if (propInfo.CanWrite)
                    if ((propInfo.Name != "WindowTarget") && (excludeProperties?.Contains(propInfo.Name) != true))
                        propInfo.SetValue(newInstance, propInfo.GetValue(baseInstance, null), null);
            return newInstance;
        }

    }

}
