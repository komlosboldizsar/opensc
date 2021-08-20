using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public class DynamicTextFunctionArgumentBase : IDynamicTextFunctionArgument
    {

        public int Index { get; private set; }
        public string Description { get; private set; }
        public Type ObjectType { get; protected set; }
        public DynamicTextFunctionArgumentType KeyType { get; protected set; }

        public DynamicTextFunctionArgumentBase(Type objectType = null, DynamicTextFunctionArgumentType keyType = DynamicTextFunctionArgumentType.Integer)
        {
            DynamicTextFunctionArgumentAttribute myAttribute = GetType().GetAttribute<DynamicTextFunctionArgumentAttribute>();
            if (myAttribute == null)
                throw new Exception(); //NoDynamicTextFunctionArgumentAttributeException();
            Index = myAttribute.Index;
            Description = myAttribute.Description;
            ObjectType = objectType;
            KeyType = keyType;
        }

        public virtual object GetObjectByKey(object key, object[] previousArgumentObjects)
        {
            if (previousArgumentObjects.Count() < Index)
                return null;
            return _getObjectByKey(key, previousArgumentObjects);
        }

        protected virtual object _getObjectByKey(object key, object[] previousArgumentObjects) => null;

    }

}
