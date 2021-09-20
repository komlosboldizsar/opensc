using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    abstract public class ModelTypeRegisterBase<TModelBasetype> : IModelTypeRegister
        where TModelBasetype : IModel
    {

        private Dictionary<string, Type> registeredTypes = new Dictionary<string, Type>();

        public IEnumerable<Type> RegisteredTypes => registeredTypes.Values;

        public Type ConvertStringToType(string typeLabel)
        {
            if (!registeredTypes.TryGetValue(typeLabel, out Type foundType))
                return null;
            return foundType;
        }

        public string ConvertTypeToString(Type type)
        {
            try
            {
                return registeredTypes.First(x => (x.Value == type)).Key;
            }
            catch
            {
                return string.Empty;
            }
        }

        public void RegisterType<TModelSubtype>() where TModelSubtype : TModelBasetype
        {
            Type subtypeInfo = typeof(TModelSubtype);
            string typeCode = subtypeInfo.GetAttribute<TypeCodeAttribute>()?.Code;
            if (typeCode == null)
                return;
            registeredTypes.Add(typeCode, subtypeInfo);
        }

    }
}
