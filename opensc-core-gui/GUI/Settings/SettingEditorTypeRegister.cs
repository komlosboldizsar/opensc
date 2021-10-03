using OpenSC.Model;
using OpenSC.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenSC.GUI.Settings
{
    class SettingEditorTypeRegister
    {

        #region Singleton
        public static SettingEditorTypeRegister Instance { get; } = new SettingEditorTypeRegister();
        private SettingEditorTypeRegister() => autoRegisterAllEditorsFromNamespace(BUILTIN_EDITORS_NAMESPACE);
        #endregion

        private static ISettingEditorControl BASE_EDITOR = new SettingEditorBase();

        private static Dictionary<Type, ISettingEditorControl> registeredSettingEditors = null;
        private static Dictionary<Type, ISettingEditorControl> registeredValueEditors = null;

        private readonly Type[] EMPTY_TYPE_ARRAY = new Type[] { };
        private readonly object[] EMPTY_OBJECT_ARRAY = new object[] { };

        private readonly string BUILTIN_EDITORS_NAMESPACE = $"{nameof(OpenSC)}.{nameof(OpenSC.GUI)}.{nameof(OpenSC.GUI.Settings)}.{nameof(OpenSC.GUI.Settings)}";

        private void autoRegisterAllEditorsFromNamespace(string _namespace)
        {
            autoRegisterEditorsFromNamespace(_namespace, registeredSettingEditors, getTypeForSettingEditor);
            autoRegisterEditorsFromNamespace(_namespace, registeredValueEditors, getTypeForValueEditor);
        }

        private delegate Type EditorTypeGetterDelegate(ISettingEditorControl converter);

        private void autoRegisterEditorsFromNamespace(string _namespace, Dictionary<Type, ISettingEditorControl> storageDictionary, EditorTypeGetterDelegate typeGetterMethod)
        {
            Type TYPEOF_EDITOR = typeof(ISettingEditorControl);
            Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();
            IEnumerable<Type> editorTypes = allTypes.Where(t => t.IsClass && !t.IsAbstract && (t.Namespace == _namespace) && t.IsAssignableTo(TYPEOF_EDITOR));
            foreach (Type editorType in editorTypes)
            {
                ConstructorInfo ctor = editorType.GetConstructor(EMPTY_TYPE_ARRAY);
                if (ctor != null)
                {
                    ISettingEditorControl editor = ctor.Invoke(EMPTY_OBJECT_ARRAY) as ISettingEditorControl;
                    if (editor != null)
                        storageDictionary.Add(typeGetterMethod(editor), editor);
                }
            }
        }

        private Type getTypeForSettingEditor(ISettingEditorControl converter)
            => converter.GetType().GetAttribute<EditorForSettingAttribute>()?.Type;

        private Type getTypeForValueEditor(ISettingEditorControl converter)
            => converter.GetType().GetAttribute<EditorForSettingValueAttribute>()?.Type;

        private ISettingEditorControl getEditorForSetting(ISetting setting)
            => getConverterForSomething(setting.GetType(), registeredSettingEditors);

        private ISettingEditorControl getEditorForValue(ISetting setting)
            => getConverterForSomething(setting.ValueType, registeredValueEditors);

        private ISettingEditorControl getConverterForSomething(Type forType, Dictionary<Type, ISettingEditorControl> availableEditors)
        {
            if (availableEditors.TryGetValue(forType, out ISettingEditorControl converter))
                return converter;
            foreach (KeyValuePair<Type, ISettingEditorControl> availableConverterKVP in availableEditors)
                if (availableConverterKVP.Key.IsAssignableFrom(forType))
                    return availableConverterKVP.Value;
            return null;
        }

        public ISettingEditorControl GetEditorForSetting(ISetting setting)
        {
            ISettingEditorControl settingEditorControl = getEditorForSetting(setting);
            if (settingEditorControl != null)
                return settingEditorControl;
            settingEditorControl = getEditorForValue(setting);
            if (settingEditorControl != null)
                return settingEditorControl;
            return BASE_EDITOR.GetInstanceForSetting(setting);
        }

    }
}
