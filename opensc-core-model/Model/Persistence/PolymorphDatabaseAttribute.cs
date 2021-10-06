using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PolymorphDatabaseAttribute : Attribute
    {
        public IModelTypeRegister TypeRegister { get; private set; }

        public PolymorphDatabaseAttribute(Type typeRegisterType)
        {
            PropertyInfo propertyInfo = typeRegisterType.GetProperty(INSTANCE_VARIABLE_NAME, INSTANCE_VARIABLE_LOOKUP_BINDING_FLAGS);
            if (propertyInfo == null)
                return;
            this.TypeRegister = (IModelTypeRegister)propertyInfo.GetValue(null);
        }

        private const string INSTANCE_VARIABLE_NAME = "Instance";
        private const BindingFlags INSTANCE_VARIABLE_LOOKUP_BINDING_FLAGS = BindingFlags.Static | BindingFlags.Public;
    }
}
