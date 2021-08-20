using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.WorkspaceManager
{
    public class WindowTypeRegister
    {

        private static Dictionary<string, Type> registeredTypes = new Dictionary<string, Type>();

        public static void RegisterWindowType(Type windowType)
        {

            if (!windowType.GetInterfaces().Contains(typeof(IPersistableWindow)))
                throw new Exception();

            object[] foundAttributes = windowType.GetCustomAttributes(typeof(WindowTypeNameAttribute), true);
            if (foundAttributes.Length == 0)
                throw new Exception();
            WindowTypeNameAttribute typeNameAttribute = foundAttributes[0] as WindowTypeNameAttribute;
            if (typeNameAttribute == null)
                throw new Exception();

            string typeName = typeNameAttribute.TypeName;
            if (!registeredTypes.ContainsKey(typeName))
                registeredTypes.Add(typeName, windowType);

        }

        public static string GetTypeNameForWindow(IPersistableWindow window)
        {
            try
            {
                Type windowType = window.GetType();
                return registeredTypes.First(x => x.Value == windowType).Key;
            }
            catch
            {
                return null;
            }
        }

        public static Type GetTypeForTypeName(string typeName)
        {
            if (!registeredTypes.TryGetValue(typeName, out Type foundType))
                return null;
            return foundType;
        }

    }
}
