using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.WorkspaceManager.ValueConverters
{
    class ValueConverter
    {

        private static IValueConverter[] convertersToRegister = new IValueConverter[]
        { };

        private static Dictionary<string, IValueConverter> registeredConvertersByName = null;

        private static Dictionary<string, IValueConverter> ConvertersByName
        {
            get
            {
                if (registeredConvertersByName == null) {
                    registeredConvertersByName = new Dictionary<string, IValueConverter>();
                    foreach (IValueConverter converter in convertersToRegister)
                    {
                        string typeName = getTypeNameForConverter(converter);
                        if (typeName != string.Empty)
                            registeredConvertersByName.Add(typeName, converter);
                    }
                }
                return registeredConvertersByName;
            }
        }

        private static Dictionary<Type, IValueConverter> registeredConvertersByType = null;

        private static Dictionary<Type, IValueConverter> ConvertersByType
        {
            get
            {
                if (registeredConvertersByType == null)
                {
                    registeredConvertersByType = new Dictionary<Type, IValueConverter>();
                    foreach (IValueConverter converter in convertersToRegister)
                    {
                        Type type = getTypeForConverter(converter);
                        if(type != null)
                            registeredConvertersByType.Add(type, converter);
                    }
                }
                return registeredConvertersByType;
            }
        }

        public static SerializedData Serialize(object obj)
        {
            if (obj == null)
                return new SerializedData() { TypeName = "", Value = "" };
            if (!ConvertersByType.TryGetValue(obj.GetType(), out IValueConverter converter))
                return new SerializedData() { TypeName = "", Value = obj.ToString() };
            return new SerializedData() { TypeName = getTypeNameForConverter(converter), Value = converter.Serialize(obj) };
        }

        public static object Deserialize(string value, string typeName)
        {
            if (!ConvertersByName.TryGetValue(typeName, out IValueConverter converter))
                return value;
            return converter.Deserialize(value);
        }

        public static object Deserialize(SerializedData serializedData)
        {
            return Deserialize(serializedData.Value, serializedData.TypeName);
        }

        private static ConverterTypeAttribute getAttributeForConverter(IValueConverter converter)
        {
            object[] attributes = converter.GetType().GetCustomAttributes(true);
            try
            {
                return attributes.First(attr => (attr is ConverterTypeAttribute)) as ConverterTypeAttribute;
            }
            catch
            {
                return null;
            }
        }

        private static string getTypeNameForConverter(IValueConverter converter)
        {
            ConverterTypeAttribute typeAttr = getAttributeForConverter(converter);
            return (typeAttr != null) ? typeAttr.TypeName : string.Empty;
        }

        private static Type getTypeForConverter(IValueConverter converter)
        {
            ConverterTypeAttribute typeAttr = getAttributeForConverter(converter);
            return (typeAttr != null) ? typeAttr.Type : null;
        }

        public struct SerializedData
        {
            public string TypeName;
            public string Value;
        }

    }
}
