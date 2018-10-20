using OpenSC.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenSC.GUI.Settings
{
    class SettingEditorTypeRegister
    {

        private static ISettingEditorControl[] knownEditors = new ISettingEditorControl[]
        {
            new StringSettingEditor()
        };

        private static Dictionary<Type, ISettingEditorControl> registeredEditors = null;

        public static Dictionary<Type, ISettingEditorControl> RegisteredEditors
        {
            get
            {
                if(registeredEditors == null)
                {
                    registeredEditors = new Dictionary<Type, ISettingEditorControl>();
                    foreach(ISettingEditorControl editor in knownEditors)
                    {
                        Type editorType = GetTypeForEditor(editor);
                        if (editorType != null)
                            registeredEditors.Add(editorType, editor);
                    }
                }
                return registeredEditors;
            }
        }

        private static Type GetTypeForEditor(ISettingEditorControl editor)
        {
            object[] attributes = editor.GetType().GetCustomAttributes(true);
            object foundAttribute = attributes.FirstOrDefault(attr => (attr is SettingEditorControlTypeAttribute));
            if (foundAttribute == null)
                return null;
            SettingEditorControlTypeAttribute typedAttribute = foundAttribute as SettingEditorControlTypeAttribute;
            return typedAttribute?.Type;
        }

        public static ISettingEditorControl GetEditorForSetting(ISetting setting)
        {
            if (!RegisteredEditors.TryGetValue(setting.Type, out ISettingEditorControl editor))
                return null;
            return editor.GetInstanceForSetting(setting);
        }

    }
}
