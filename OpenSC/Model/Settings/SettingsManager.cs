using OpenSC.Model.Settings.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Settings
{
    class SettingsManager
    {

        public static SettingsManager Instance { get; } = new SettingsManager();

        private Dictionary<string, ISetting> registeredSettings = new Dictionary<string, ISetting>();

        public void RegisterSetting(ISetting setting)
        {
            if (registeredSettings.ContainsKey(setting.Key))
                throw new Exception();
            registeredSettings.Add(setting.Key, setting);
            setting.ValueChanged += settingValueChangedHandler;
        }

        private void settingValueChangedHandler(ISetting setting, object oldValue, object newValue)
        {
            if (!registeredSettings.ContainsKey(setting.Key))
                return;
            if(!loadingSettings)
                SaveSettings();
        }

        private const string SETTINGS_FILE_PATH = "settings.xml";

        private const string ROOT_TAG = "settings";
        private const string SETTING_TAG = "setting";
        private const string ATTRIBUTE_KEY_KEY = "key";
        private const string ATTRIBUTE_KEY_VALUE = "value";

        private static readonly XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
        {
            CloseOutput = false,
            Indent = true,
            IndentChars = "\t"
        };

        public void SaveSettings()
        {

            XElement rootElement = new XElement(ROOT_TAG);
            foreach (ISetting setting in registeredSettings.Values)
            {
                XElement settingElement = serializeSetting(setting);
                if (settingElement != null)
                    rootElement.Add(settingElement);
            }

            using (FileStream stream = new FileStream(SETTINGS_FILE_PATH, FileMode.Create))
            using (XmlWriter writer = XmlWriter.Create(stream, xmlWriterSettings))
            {
                rootElement.WriteTo(writer);
            }

        }

        private bool loadingSettings = false;

        public void LoadSettings()
        {

            try
            {

                loadingSettings = true;

                XmlDocument doc = new XmlDocument();
                doc.Load(SETTINGS_FILE_PATH);

                XmlNode root = doc.DocumentElement;
                if (root.LocalName != ROOT_TAG)
                    return;

                foreach (XmlNode node in root.ChildNodes)
                    deserializeSetting(node);

            }
            finally
            {
                loadingSettings = false;
            }

        }

        private XElement serializeSetting(ISetting setting)
        {

            XElement xmlElement = new XElement(SETTING_TAG);
            ISettingValueConverter converter = GetConverterForSetting(setting);

            xmlElement.SetAttributeValue(ATTRIBUTE_KEY_KEY, setting.Key);
            string convertedValue = (converter != null) ? converter.Serialize(setting.ObjValue) : setting.ObjValue?.ToString();
            xmlElement.SetAttributeValue(ATTRIBUTE_KEY_VALUE, convertedValue);

            return xmlElement;

        }

        private void deserializeSetting(XmlNode node)
        {

            try
            {

                string key = node.Attributes[ATTRIBUTE_KEY_KEY].Value;
                if (!registeredSettings.TryGetValue(key, out ISetting setting))
                    return;

                string serializedValue = node.Attributes[ATTRIBUTE_KEY_VALUE].Value;
                object convertedValue = serializedValue;
                ISettingValueConverter converter = GetConverterForSetting(setting);
                if (converter != null)
                    convertedValue = converter.Deserialize(serializedValue);

                setting.ObjValue = convertedValue;

            }
            catch
            { }

        }

        private static ISettingValueConverter[] knownConverters = new ISettingValueConverter[]
        {
            new BoolConverter(),
            new IntConverter(),
            new ColorConverter()
        };

        private static Dictionary<Type, ISettingValueConverter> converters = null;

        private static Dictionary<Type, ISettingValueConverter> Converters
        {
            get
            {
                if (converters == null)
                {
                    converters = new Dictionary<Type, ISettingValueConverter>();
                    foreach (ISettingValueConverter converter in knownConverters) {
                        Type converterType = GetTypeForConverter(converter);
                        if (converterType != null)
                            converters.Add(converterType, converter);
                    }
                }
                return converters;
            }
        }

        private static Type GetTypeForConverter(ISettingValueConverter converter)
        {
            object[] attributes = converter.GetType().GetCustomAttributes(true);
            object foundAttribute = attributes.FirstOrDefault(attr => (attr is SettingValueConverterAttribute));
            if (foundAttribute == null)
                return null;
            SettingValueConverterAttribute typedAttribute = foundAttribute as SettingValueConverterAttribute;
            return typedAttribute?.Type;
        }

        private static ISettingValueConverter GetConverterForSetting(ISetting setting)
        {
            if (!Converters.TryGetValue(setting.Type, out ISettingValueConverter converter))
                return null;
            return converter;
        }

    }
}
