using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenSC.Model.SourceGenerators
{

    internal abstract class AttributeSymbolDataCollection<T>
        where T : AttributeSymbolDataCollection<T>, new()
    {

        private List<FieldInfo> attributeSymbolDataFields;

        public void SetContext(GeneratorExecutionContext? generatorExecutionContext)
            => fieldLookup((GeneratorExecutionContext)generatorExecutionContext);

        public static T GetWithContext(GeneratorExecutionContext? generatorExecutionContext)
        {
            T instance = new T();
            instance.SetContext(generatorExecutionContext);
            return instance;
        }

        private void fieldLookup(GeneratorExecutionContext generatorExecutionContext)
        {
            attributeSymbolDataFields = new List<FieldInfo>();
            foreach (FieldInfo fieldInfo in GetType().GetFields())
            {
                Type attributeSymbolDataType = fieldInfo.FieldType;
                if (attributeSymbolDataType.IsGenericType && (attributeSymbolDataType.GetGenericTypeDefinition() == typeof(AttributeSymbolData<>)))
                {
                    attributeSymbolDataFields.Add(fieldInfo);
                    Type attributeType = fieldInfo.FieldType.GetGenericArguments()[0];
                    AttributeSymbolData attributeSymbolData = Activator.CreateInstance(fieldInfo.FieldType) as AttributeSymbolData;
                    attributeSymbolData.Symbol = generatorExecutionContext.Compilation.GetTypeByMetadataName(attributeType.FullName);
                    fieldInfo.SetValue(this, attributeSymbolData);
                }
            }
        }

        public T GetForField(IFieldSymbol fieldSymbol)
        {
            T collectionForField = new T();
            foreach (FieldInfo fieldInfo in attributeSymbolDataFields)
            {
                AttributeSymbolData attributeSymbolData = Activator.CreateInstance(fieldInfo.FieldType) as AttributeSymbolData;
                INamedTypeSymbol attributeSymbol = (fieldInfo.GetValue(this) as AttributeSymbolData).Symbol;
                attributeSymbolData.Symbol = attributeSymbol;
                AttributeData attributeData = fieldSymbol.GetAttributes().FirstOrDefault(ad => ad.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default));
                if (attributeData != null)
                {
                    attributeSymbolData.Data = attributeData;
                    fieldInfo.SetValue(collectionForField, attributeSymbolData);
                }
            }
            return collectionForField;
        }

    }

}
