using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSC.Model.SourceGenerators
{

    internal abstract class AttributeSymbolData
    {
        public INamedTypeSymbol Symbol { get; set; }
        private AttributeData _data;
        public AttributeData Data
        {
            get => _data;
            set
            {
                _data = value;
                ArgumentCollection = createArgumentCollection();
            }
        }
        public AttributeArgumentCollection ArgumentCollection { get; private set; }
        protected abstract AttributeArgumentCollection createArgumentCollection();
    }

    internal class AttributeSymbolData<TAttribute> : AttributeSymbolData
        where TAttribute : Attribute
    {
        protected override AttributeArgumentCollection createArgumentCollection() => AttributeArgumentCollection.CreateFrom<TAttribute>(Data);
    }

}
