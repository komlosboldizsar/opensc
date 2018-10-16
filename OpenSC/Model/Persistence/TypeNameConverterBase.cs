using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    abstract public class TypeNameConverterBase : ITypeNameConverter
    {

        protected abstract Dictionary<string, Type> knownTypes { get; }

        public Type ConvertStringToType(string typeLabel)
        {
            if (!knownTypes.TryGetValue(typeLabel, out Type foundType))
                return null;
            return foundType;
        }

        public string ConvertTypeToString(Type type)
        {
            try
            {
                return knownTypes.First(x => (x.Value == type)).Key;
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
