using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Persistence
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PolymorphDatabaseAttribute: Attribute
    {

        private Type converter;

        public ITypeNameConverter Converter
        {
            get => (ITypeNameConverter) converter.GetConstructors()[0].Invoke(new object[] { });
        }

        public PolymorphDatabaseAttribute(Type Converter)
        {
            this.converter = Converter;
        }

    }
}
