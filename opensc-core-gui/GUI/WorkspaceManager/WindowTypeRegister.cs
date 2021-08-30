using OpenSC.Model;
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

        public static void RegisterWindowType<TWindow>()
            where TWindow : IPersistableWindow
        {
            Type windowType = typeof(TWindow);
            WindowTypeNameAttribute typeNameAttribute = windowType.GetAttribute<WindowTypeNameAttribute>();
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
