using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{

    abstract public class MacroTriggerArgumentBase : IMacroTriggerArgument
    {

        public int Index { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Type ObjectType { get; protected set; }
        public MacroArgumentKeyType KeyType { get; protected set; }

        public MacroTriggerArgumentBase(Type objectType = null, MacroArgumentKeyType keyType = MacroArgumentKeyType.Integer)
        {
            MacroTriggerArgumentAttribute myAttribute = GetType().GetAttribute<MacroTriggerArgumentAttribute>();
            if (myAttribute == null)
                throw new Exception(); //NoMacroTriggerArgumentAttributeException();
            Index = myAttribute.Index;
            Name = myAttribute.Name;
            Description = myAttribute.Description;
            ObjectType = objectType;
            KeyType = keyType;
        }

        public virtual object GetObjectByKey(string key, object[] previousArgumentObjects)
        {
            if (previousArgumentObjects.Count() < Index)
                return null;
            return _getObjectByKey(key, previousArgumentObjects);
        }

        protected virtual object _getObjectByKey(string key, object[] previousArgumentObjects) => null;

        public virtual string GetKeyByObject(object obj)
        {
            IModel objAsModel = obj as IModel;
            if (objAsModel != null)
                return objAsModel.ID.ToString();
            return obj?.ToString();
        }

        public virtual IEnumerable<object> GetPossibilities(object[] previousArgumentObjects)
        {
            if (previousArgumentObjects.Length < Index)
                return null;
            return _getPossibilities(previousArgumentObjects);
        }

        protected virtual IEnumerable<object> _getPossibilities(object[] previousArgumentObjects) => null;

        public virtual string GetStringForPossibility(object item) => item?.ToString() ?? string.Empty;

    }

}
