using OpenSC.Extensions;
using OpenSC.Model.Settings.Converters;
using OpenSC.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OpenSC.Model.Settings
{
    public class SettingsManager
    {

        #region Singleton
        public static SettingsManager Instance { get; } = new SettingsManager();
        private SettingsManager() => autoRegisterAllConvertersFromNamespace(BUILTIN_CONVERTERS_NAMESPACE);
        #endregion

        #region Settings store
        private Dictionary<string, ISetting> registeredSettings = new Dictionary<string, ISetting>();
        public IReadOnlyList<ISetting> RegisteredSettings => registeredSettings.Values.ToList();

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
        #endregion

        #region Serialization
        private static readonly string DIRECTORY_SETTINGS = $"{DataPathInfo.PATH_DATA}settings{Path.DirectorySeparatorChar}";
        private static readonly string FILE_SETTINGS_CURRENT = $"{DIRECTORY_SETTINGS}current.{EXTENSION_SETTINGS}";
        private static readonly string EXTENSION_SETTINGS = "xml";

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
            FileExtensions.CreateDirectoriesForFile(FILE_SETTINGS_CURRENT);
            using (FileStream stream = new FileStream(FILE_SETTINGS_CURRENT, FileMode.Create))
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
                FileExtensions.CreateDirectoriesForFile(FILE_SETTINGS_CURRENT);
                doc.Load(FILE_SETTINGS_CURRENT);
                XmlNode root = doc.DocumentElement;
                if (root.LocalName != ROOT_TAG)
                    return;
                foreach (XmlNode node in root.ChildNodes)
                    deserializeSetting(node);
                SettingsLoaded?.Invoke();
            }
            finally
            {
                loadingSettings = false;
            }
        }

        private XElement serializeSetting(ISetting setting)
        {
            XElement xmlElement = new XElement(SETTING_TAG);
            xmlElement.SetAttributeValue(ATTRIBUTE_KEY_KEY, setting.Key);
            xmlElement.SetAttributeValue(ATTRIBUTE_KEY_VALUE, convertForSerialization(setting));
            return xmlElement;
        }

        private string convertForSerialization(ISetting setting)
        {
            ISettingConverter settingConverter = getConverterForSetting(setting);
            if (settingConverter != null)
                return settingConverter.Serialize(setting);
            ISettingValueConverter valueConverter = getConverterForSettingValue(setting);
            if (valueConverter != null)
                return valueConverter.Serialize(setting.ObjValue);
            return setting.ObjValue?.ToString();
        }

        private void deserializeSetting(XmlNode node)
        {
            try
            {
                if (!registeredSettings.TryGetValue(node.Attributes[ATTRIBUTE_KEY_KEY].Value, out ISetting setting))
                    return;
                convertForDeserialization(setting, node.Attributes[ATTRIBUTE_KEY_VALUE].Value);
            }
            catch
            { }
        }

        private void convertForDeserialization(ISetting setting, string serialized)
        {
            ISettingConverter settingConverter = getConverterForSetting(setting);
            if (settingConverter != null)
            {
                settingConverter.Deserialize(setting, serialized);
                return;
            }
            ISettingValueConverter valueConverter = getConverterForSettingValue(setting);
            if (valueConverter != null)
            {
                setting.ObjValue = valueConverter.Deserialize(serialized);
                return;
            }
            setting.ObjValue = serialized;
        }
        #endregion

        #region Converters
        private Dictionary<Type, ISettingConverter> settingConverters = new Dictionary<Type, ISettingConverter>();
        private Dictionary<Type, ISettingValueConverter> valueConverters = new Dictionary<Type, ISettingValueConverter>();

        private readonly Type[] EMPTY_TYPE_ARRAY = new Type[] { };
        private readonly object[] EMPTY_OBJECT_ARRAY = new object[] { };

        private readonly string BUILTIN_CONVERTERS_NAMESPACE = $"{nameof(OpenSC)}.{nameof(OpenSC.Model)}.{nameof(OpenSC.Model.Settings)}.{nameof(OpenSC.Model.Settings.Converters)}";
        
        private void autoRegisterAllConvertersFromNamespace(string _namespace)
        {
            autoRegisterConvertersFromNamespace(_namespace, settingConverters, getTypeForSettingConverter);
            autoRegisterConvertersFromNamespace(_namespace, valueConverters, getTypeForValueConverter);
        }

        private delegate Type ConverterTypeGetterDelegate<TConverterInterface>(TConverterInterface converter);

        private void autoRegisterConvertersFromNamespace<TConverter>(string _namespace, Dictionary<Type, TConverter> storageDictionary, ConverterTypeGetterDelegate<TConverter> typeGetterMethod)
            where TConverter : class
        {
            Type TYPEOF_CONVERTER = typeof(TConverter);
            Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();
            IEnumerable<Type> converterTypes = allTypes.Where(t => t.IsClass && !t.IsAbstract && (t.Namespace == _namespace) && t.IsAssignableTo(TYPEOF_CONVERTER));
            foreach (Type converterType in converterTypes)
            {
                ConstructorInfo ctor = converterType.GetConstructor(EMPTY_TYPE_ARRAY);
                if (ctor != null)
                {
                    TConverter converter = ctor.Invoke(EMPTY_OBJECT_ARRAY) as TConverter;
                    if (converter != null)
                        storageDictionary.Add(typeGetterMethod(converter), converter);
                }
            }
        }

        private Type getTypeForSettingConverter(ISettingConverter converter)
            => converter.GetType().GetAttribute<SettingConverterAttribute>()?.Type;

        private Type getTypeForValueConverter(ISettingValueConverter converter)
            => converter.GetType().GetAttribute<SettingValueConverterAttribute>()?.Type;

        private ISettingConverter getConverterForSetting(ISetting setting)
            => getConverterForSomething(setting.GetType(), settingConverters);

        private ISettingValueConverter getConverterForSettingValue(ISetting setting)
            => getConverterForSomething(setting.ValueType, valueConverters);

        private TConverter getConverterForSomething<TConverter>(Type forType, Dictionary<Type, TConverter> availableConverters)
            where TConverter : class
        {
            if (availableConverters.TryGetValue(forType, out TConverter converter))
                return converter;
            foreach (KeyValuePair<Type, TConverter> availableConverterKVP in availableConverters)
                if (availableConverterKVP.Key.IsAssignableFrom(forType))
                    return availableConverterKVP.Value;
            return null;
        }
        #endregion

        #region Events
        public delegate void SettingsLoadedDelegate();
        public event SettingsLoadedDelegate SettingsLoaded;
        #endregion

    }

}
