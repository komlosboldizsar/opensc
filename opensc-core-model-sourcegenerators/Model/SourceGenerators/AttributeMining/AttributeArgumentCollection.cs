using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenSC.Model.SourceGenerators
{

    public class AttributeArgumentCollection
    {

        private Dictionary<string, object> fieldValues;
        private Dictionary<string, object> fieldStrings;

        public AttributeArgumentCollection(Dictionary<string, object> fieldValues, Dictionary<string, object> fieldStrings)
        {
            this.fieldValues = fieldValues ?? new Dictionary<string, object>();
            this.fieldStrings = fieldStrings ?? new Dictionary<string, object>();
        }

        // @source https://stackoverflow.com/a/67539911
        public static AttributeArgumentCollection CreateFrom<TAttribute>(AttributeData attributeData) where TAttribute : Attribute
        {
            if (attributeData == null)
                return null;
            try
            {
                Dictionary<string, object> fieldValues = new Dictionary<string, object>();
                Dictionary<string, object> fieldStrings = new Dictionary<string, object>();
                TAttribute attributeForDefaultValues = (TAttribute)Activator.CreateInstance(typeof(TAttribute), true);
                foreach (FieldInfo fieldInfo in typeof(TAttribute).GetFields())
                {
                    string fieldName = fieldInfo.Name;
                    object fieldValue = fieldInfo.GetValue(attributeForDefaultValues);
                    fieldValues[fieldName] = fieldValue;
                    object fieldValueString;
                    if (fieldValue == null)
                    {
                        fieldValueString = null;
                    }
                    else
                    {
                        if (fieldInfo.FieldType.IsArray)
                        {
                            int fieldValueLength = ((Array)fieldValue).Length;
                            string[] fieldValueStringArray = new string[fieldValueLength];
                            for (int i = 0; i < fieldValueLength; i++)
                                fieldValueStringArray[i] = ((Array)fieldValue).GetValue(i)?.ToString();
                            fieldValueString = fieldValueStringArray;
                        }
                        else
                        {
                            fieldValueString = fieldValue.ToString();
                        }
                    }
                    fieldStrings[fieldName] = fieldValueString;
                }
                IMethodSymbol attributeConstructor = attributeData.AttributeConstructor;
                if (attributeConstructor != null)
                {
                    ImmutableArray<TypedConstant> constructorArguments = attributeData.ConstructorArguments;
                    int i = 0;
                    foreach (IParameterSymbol parameter in attributeConstructor.Parameters)
                    {
                        if (constructorArguments.Length > i)
                        {
                            string constructorArgumentName = parameter.Name;
                            (object, object) convertedTypedConstant = typedConstantToStringOrStringArray(constructorArguments[i++]);
                            fieldValues[constructorArgumentName] = convertedTypedConstant.Item1;
                            fieldStrings[constructorArgumentName] = convertedTypedConstant.Item2;
                        }
                    }
                }
                foreach (KeyValuePair<string, TypedConstant> namedArgument in attributeData.NamedArguments)
                {
                    string namedArgumentName = namedArgument.Key;
                    (object, object) convertedTypedConstant = typedConstantToStringOrStringArray(namedArgument.Value);
                    fieldValues[namedArgumentName] = convertedTypedConstant.Item1;
                    fieldStrings[namedArgumentName] = convertedTypedConstant.Item2;
                }
                return new AttributeArgumentCollection(fieldValues, fieldStrings);
            }
            catch
            {
                return new AttributeArgumentCollection(null, null);
            }
        }

        private static IEnumerable<object> getActualConstuctorParams(AttributeData attributeData)
        {
            foreach (var arg in attributeData.ConstructorArguments)
                yield return (arg.Kind == TypedConstantKind.Array)
                    ? arg.Values.Select(a => a.Value).OfType<string>().ToArray()
                    : arg.Value;
        }

        private static (object, object) typedConstantToStringOrStringArray(TypedConstant typedConstant)
        {
            if (typedConstant.Kind == TypedConstantKind.Array)
            {
                ImmutableArray<TypedConstant> typedConstantValueArray = typedConstant.Values;
                int typedConstantValueStringArrayLength = typedConstantValueArray.Length;
                string[] typedConstantValueStringArray = new string[typedConstantValueStringArrayLength];
                for (int i = 0; i < typedConstantValueStringArrayLength; i++)
                    typedConstantValueStringArray[i] = typedConstantValueToString(typedConstantValueArray[i].Value);
                return (typedConstantValueArray, typedConstantValueStringArray);
            }
            else
            {
                object typedConstantValue = typedConstant.Value;
                return (typedConstantValue, typedConstantValueToString(typedConstantValue));
            }
        }

        private static string typedConstantValueToString(object typedConstantValue)
            => (typedConstantValue is ISymbol sym) ? sym.ToDisplayString() : typedConstantValue?.ToString();

        public T GetValue<T>(string fieldName)
            => fieldValues.TryGetValue(fieldName, out object fieldValue) ? (T)fieldValue : default(T);

        public string GetString(string fieldName)
            => fieldStrings.TryGetValue(fieldName, out object fieldString) ? (string)fieldString : null;

        public string[] GetStringArray(string fieldName)
            => fieldStrings.TryGetValue(fieldName, out object fieldStringArray) ? (string[])fieldStringArray : null;

    }

}
