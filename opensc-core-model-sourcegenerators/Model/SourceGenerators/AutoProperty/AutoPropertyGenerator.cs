using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using OpenSC.Model.SourceGenerators.Templating;

namespace OpenSC.Model.SourceGenerators
{

    [Generator]
    public class AutoPropertyGenertor : SourceGeneratorBase<AutoPropertyGenertor.SyntaxReceiver>
    {

        [TemplateFileName("Class")]
        private Template classTemplate = null;

        [TemplateFileName("Property")]
        private Template propertyTemplate = null;

        [TemplateFileName("Event")]
        private Template eventTemplate = null;

        private class MyAttributeSymbolDataCollection : AttributeSymbolDataCollection<MyAttributeSymbolDataCollection>
        {
            public AttributeSymbolData<AutoProperty> mainAttribute = null;
            public AttributeSymbolData<AutoProperty.Event> eventAttribute = null;
            public AttributeSymbolData<AutoProperty.BeforeChange> beforeChangeAttribute = null;
            public AttributeSymbolData<AutoProperty.AfterChange> afterChangeAttribute = null;
            public AttributeSymbolData<AutoProperty.Validator> validatorAttribute = null;
            public AttributeSymbolData<AutoProperty.DontCopyAttributeType> dontCopyAttributeTypeAttribute = null;
        }

        protected override void _execute(GeneratorExecutionContext executionContext, SyntaxReceiver receiver)
        {
            MyAttributeSymbolDataCollection AutoPropertyCollection = MyAttributeSymbolDataCollection.GetWithContext(executionContext);
            foreach (IGrouping<INamedTypeSymbol, IFieldSymbol> group in receiver.Fields.GroupBy<IFieldSymbol, INamedTypeSymbol>(f => f.ContainingType, SymbolEqualityComparer.Default))
            {
                string classSource = processClass(executionContext, group.Key, group.ToList(), AutoPropertyCollection);
                string baseName = group.Key.Name;
                INamespaceSymbol containerNamespace = group.Key.ContainingNamespace;
                if (containerNamespace != null)
                    baseName = containerNamespace + "." + baseName;
                createFile(executionContext, baseName, classSource);
            }
        }

        protected override string _getSourcePath() => ProjectSourcePath.GetSourceFilePath();

        private string processClass(GeneratorExecutionContext executionContext, INamedTypeSymbol classSymbol, List<IFieldSymbol> fields, MyAttributeSymbolDataCollection AutoPropertyCollection)
        {
            if (!classSymbol.ContainingSymbol.Equals(classSymbol.ContainingNamespace, SymbolEqualityComparer.Default))
                return null;
            string namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
            string className = classSymbol.Name;
            StringBuilder classBodyBuilder = new StringBuilder();
            foreach (IFieldSymbol fieldSymbol in fields)
                processField(executionContext, classBodyBuilder, fieldSymbol, AutoPropertyCollection);
            Dictionary<string, string> substitueCollection = new Dictionary<string, string>()
            {
                { nameof(namespaceName), namespaceName },
                { nameof(className), className }
            };
            return classTemplate.GetInstance().Substitue(substitueCollection).InsertBlock(BLOCK_PLACEHOLDER_CLASSBODY, classBodyBuilder);
        }

        private const string BLOCK_PLACEHOLDER_CLASSBODY = "CLASSBODY";

        private void processField(GeneratorExecutionContext executionContext, StringBuilder source, IFieldSymbol fieldSymbol, MyAttributeSymbolDataCollection AutoPropertyCollection)
        {
            MyAttributeSymbolDataCollection AutoPropertyCollectionForField = AutoPropertyCollection.GetForField(fieldSymbol);
            AttributeSymbolData mainAttributeData = AutoPropertyCollectionForField.mainAttribute;
            string fieldName = fieldSymbol.Name;
            string overriddenPropertyName = mainAttributeData.ArgumentCollection.GetValue<string>(nameof(AutoProperty.PropertyName));
            string propertyName = choosePropertyNameFromFieldName(fieldName, overriddenPropertyName);
            string eventName = chooseEventNameFromPropertyName(propertyName, AutoPropertyCollectionForField.eventAttribute);
            if ((propertyName.Length == 0) || (propertyName == fieldName))
                return;
            PropertyCommonData propertyData = new PropertyCommonData()
            {
                ClassName = fieldSymbol.ContainingSymbol.Name,
                PropertyName = propertyName,
                EventName = eventName,
                FieldTypeString = fieldSymbol.Type.ToDisplayString()
            };
            generateProperty(executionContext, source, fieldSymbol, propertyData, AutoPropertyCollectionForField);
            generateEvent(executionContext, source, fieldSymbol, propertyData, AutoPropertyCollectionForField.eventAttribute);
        }

        private class PropertyCommonData
        {
            public string ClassName;
            public string PropertyName;
            public string EventName;
            public string FieldTypeString;
        }

        private void generateProperty(GeneratorExecutionContext executionContext, StringBuilder sourceBuilder, IFieldSymbol fieldSymbol, PropertyCommonData propertyCommonData, MyAttributeSymbolDataCollection AutoPropertyCollection)
        {
            AttributeSymbolData mainAttributeData = AutoPropertyCollection.mainAttribute;
            Accessibility propertyAccessibilityValue = mainAttributeData.ArgumentCollection.GetValue<Accessibility>(nameof(AutoProperty.PropertyAccessibility));
            if (propertyAccessibilityValue == Accessibility.NotApplicable)
                propertyAccessibilityValue = Accessibility.Public;
            string propertyAccessibility = propertyAccessibilityValue.ToCode();
            Accessibility getterAccessibilityValue = mainAttributeData.ArgumentCollection.GetValue<Accessibility>(nameof(AutoProperty.GetterAccessibility));
            string getterAccessibility = accessibilityString(propertyAccessibilityValue, getterAccessibilityValue);
            Accessibility setterAccessibilityValue = mainAttributeData.ArgumentCollection.GetValue<Accessibility>(nameof(AutoProperty.SetterAccessibility));
            string setterAccessibility = accessibilityString(propertyAccessibilityValue, setterAccessibilityValue);
            bool isVirtual = mainAttributeData.ArgumentCollection.GetValue<bool>(nameof(AutoProperty.IsVirtual));
            string beforeChange = getChangeDelegate(executionContext, fieldSymbol, AutoPropertyCollection.beforeChangeAttribute, nameof(AutoProperty.BeforeChange.MethodName), convertBeforeChangeDelegate);
            string afterChange = getChangeDelegate(executionContext, fieldSymbol, AutoPropertyCollection.afterChangeAttribute, nameof(AutoProperty.AfterChange.MethodName), convertAfterChangeDelegate);
            string validator = (AutoPropertyCollection.validatorAttribute?.ArgumentCollection.GetString(nameof(AutoProperty.Validator.MethodName))).NullAsString();
            string[] notCopiedAttributeTypeStrings = AutoPropertyCollection.dontCopyAttributeTypeAttribute?.ArgumentCollection.GetStringArray(nameof(AutoProperty.DontCopyAttributeType.Types));
            List<string> copiedAttributesList = new List<string>();
            INamedTypeSymbol autoPropertyAttributeCommonInterfaceSymbol = executionContext.Compilation.GetTypeByMetadataName(typeof(IAutoPropertyAttribute).ToString());
            foreach (AttributeData attr in fieldSymbol.GetAttributes())
                if (!attr.AttributeClass.AllInterfaces.Contains(autoPropertyAttributeCommonInterfaceSymbol))
                    if ((notCopiedAttributeTypeStrings == null) || !notCopiedAttributeTypeStrings.Contains(attr.AttributeClass.ToString()))
                        copiedAttributesList.Add($"[{attr}]");
            string copiedAttributes = string.Join(Environment.NewLine, copiedAttributesList);
            Dictionary<string, string> substitues = new Dictionary<string, string>()
            {
                { "className", propertyCommonData.ClassName },
                { "fieldName", fieldSymbol.Name },
                { "eventName", propertyCommonData.EventName.NullAsString() },
                { "propertyName", propertyCommonData.PropertyName },
                { "fieldType", propertyCommonData.FieldTypeString },
                { nameof(propertyAccessibility), propertyAccessibility },
                { nameof(getterAccessibility), getterAccessibility },
                { nameof(setterAccessibility), setterAccessibility },
                { "virtualModifier", isVirtual ? "virtual" : string.Empty },
                { nameof(beforeChange), beforeChange },
                { nameof(afterChange), afterChange },
                { nameof(validator), validator },
                { nameof(copiedAttributes), copiedAttributes }
            };
            sourceBuilder.AppendLine(propertyTemplate.GetInstanceContent(substitues));
        }

        private string accessibilityString(Accessibility propertyAccessibility, Accessibility accessorAccessibility)
            => ((accessorAccessibility != Accessibility.NotApplicable) && (accessorAccessibility != propertyAccessibility)) ? accessorAccessibility.ToCode() : string.Empty;

        private string getChangeDelegate(GeneratorExecutionContext executionContext, IFieldSymbol fieldSymbol, AttributeSymbolData attributeSymbolData, string methodNameFieldName, Func<string, int, string> converterMethod)
        {
            string methodName = attributeSymbolData?.ArgumentCollection.GetString(methodNameFieldName);
            return convertChangeDelegate(executionContext, fieldSymbol, attributeSymbolData, methodName, converterMethod).NullAsString();
        }

        private string convertChangeDelegate(GeneratorExecutionContext executionContext, IFieldSymbol fieldSymbol, AttributeSymbolData attributeSymbolData, string methodName, Func<string, int, string> converterMethod)
        {
            if (methodName == null)
                return null;
            void reportDiagnostic(DiagnosticDescriptor descriptor) => executionContext.ReportDiagnostic(Diagnostic.Create(descriptor,
                attributeSymbolData.Symbol.Locations[0],
                methodName, attributeSymbolData.Symbol.Name, fieldSymbol.ContainingType.Name, fieldSymbol.Name));
            try
            {
                IMethodSymbol foundMethod = getMethodByName(fieldSymbol.ContainingType, methodName);
                if (foundMethod == null)
                {
                    reportDiagnostic(DIAGNOSTIC_DESCRIPTOR_OG1001);
                    return null;
                }
                return converterMethod(methodName, foundMethod.Parameters.Length);
            }
            catch (AmbiguousMatchException)
            {
                reportDiagnostic(DIAGNOSTIC_DESCRIPTOR_OG1002);
            }
            catch (MethodTypeMismatchException)
            {
                reportDiagnostic(DIAGNOSTIC_DESCRIPTOR_OG1003);
            }
            return null;
        }

        private static readonly DiagnosticDescriptor DIAGNOSTIC_DESCRIPTOR_OG1001 = new DiagnosticDescriptor(
            "OG1001", "Delegate not found", "Delegate declared with the name '{0}' requested by '{1}' attribute of field '{2}.{3}' is not found",
            "AutoProperties", DiagnosticSeverity.Warning, true);

        private static readonly DiagnosticDescriptor DIAGNOSTIC_DESCRIPTOR_OG1002 = new DiagnosticDescriptor(
            "OG1002", "Ambiguous delegate name", "Multiple delegates declared with the name '{0}' requested by '{1}' attribute of field '{2}.{3}' were found",
            "AutoProperties", DiagnosticSeverity.Warning, true);

        private static readonly DiagnosticDescriptor DIAGNOSTIC_DESCRIPTOR_OG1003 = new DiagnosticDescriptor(
            "OG1003", "Couldn't convert delegate", "Couldn't convert delegate with the name '{0}' requested by '{1}' attribute of field '{2}.{3}' to the signature needed by the attribute and it's overlying event",
            "AutoProperties", DiagnosticSeverity.Warning, true);

        private IMethodSymbol getMethodByName(INamedTypeSymbol classSymbol, string methodName)
        {
            INamedTypeSymbol classForLookup = classSymbol;
            IMethodSymbol lastFoundMethod = null;
            while (classForLookup != null)
            {
                IEnumerable<IMethodSymbol> methodsOfClass = classForLookup.GetMembers(methodName).OfType<IMethodSymbol>();
                IEnumerator<IMethodSymbol> methodsOfClassEnumerator = methodsOfClass.GetEnumerator();
                if (methodsOfClassEnumerator.MoveNext()) // found > 0
                {
                    IMethodSymbol foundMethod = methodsOfClassEnumerator.Current;
                    if (lastFoundMethod == null)
                        lastFoundMethod = foundMethod;
                    else if ((lastFoundMethod.OverriddenMethod == null) || !SymbolEqualityComparer.Default.Equals(foundMethod, lastFoundMethod.OverriddenMethod))
                        throw new AmbiguousMatchException();
                    if (methodsOfClassEnumerator.MoveNext()) // found > 1
                        throw new AmbiguousMatchException();
                }
                classForLookup = classForLookup.BaseType;
            }
            return lastFoundMethod;
        }

        private class MethodTypeMismatchException : Exception { }

        private string convertBeforeChangeDelegate(string methodName, int paramsLength)
        {
            switch (paramsLength)
            {
                case 0:
                    return $"(ov, nv, a) => {methodName}()";
                case 1:
                    return $"(ov, nv, a) => {methodName}(nv)";
                case 2:
                    return $"(ov, nv, a) => {methodName}(ov, nv)";
                case 3:
                    return methodName;
                default:
                    throw new MethodTypeMismatchException();
            }
        }

        private string convertAfterChangeDelegate(string methodName, int paramsLength)
        {
            switch (paramsLength)
            {
                case 0:
                    return $"(ov, nv) => {methodName}()";
                case 1:
                    return $"(ov, nv) => {methodName}(nv)";
                case 2:
                    return methodName;
                default:
                    throw new MethodTypeMismatchException();
            }
        }

        private void generateEvent(GeneratorExecutionContext executionContext, StringBuilder sourceBuilder, IFieldSymbol fieldSymbol, PropertyCommonData propertyCommonData, AttributeSymbolData eventAttributeData)
        {
            if (propertyCommonData.EventName == null)
                return;
            string senderType = eventAttributeData?.ArgumentCollection.GetString(nameof(AutoProperty.Event.SenderType)) ?? propertyCommonData.ClassName;
            Dictionary<string, string> substitues = new Dictionary<string, string>()
            {
                { nameof(senderType), senderType },
                { "fieldType", propertyCommonData.FieldTypeString },
                { "eventName", propertyCommonData.EventName }
            };
            sourceBuilder.AppendLine(eventTemplate.GetInstanceContent(substitues));
        }

        private string choosePropertyNameFromFieldName(string fieldName, string overridenNameOpt)
        {
            if (overridenNameOpt != null)
                return overridenNameOpt;
            fieldName = fieldName.TrimStart('_');
            if (fieldName.Length == 0)
                return string.Empty;
            if (fieldName.Length == 1)
                return fieldName.ToUpper();
            return fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
        }

        private string chooseEventNameFromPropertyName(string propertyName, AttributeSymbolData eventAttributeData)
        {
            if (eventAttributeData?.ArgumentCollection.GetValue<bool>(nameof(AutoProperty.Event.NoEventGeneration)) == true)
                return null;
            string overridenNameOpt = eventAttributeData?.ArgumentCollection.GetValue<string>(nameof(AutoProperty.Event.EventName));
            if (overridenNameOpt != null)
                return overridenNameOpt;
            return propertyName + EVENT_POSTFIX;
        }

        private const string EVENT_POSTFIX = "Changed";

        public class SyntaxReceiver : ISyntaxContextReceiver
        {

            public List<IFieldSymbol> Fields { get; } = new List<IFieldSymbol>();
  
            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                if ((context.Node is FieldDeclarationSyntax fieldDeclarationSyntax) && (fieldDeclarationSyntax.AttributeLists.Count > 0))
                {
                    foreach (VariableDeclaratorSyntax variable in fieldDeclarationSyntax.Declaration.Variables)
                    {
                        IFieldSymbol fieldSymbol = context.SemanticModel.GetDeclaredSymbol(variable) as IFieldSymbol;
                        if (fieldSymbol.GetAttributes().Any(ad => ad.AttributeClass.ToDisplayString() == typeof(AutoProperty).FullName))
                            Fields.Add(fieldSymbol);
                    }
                }
            }

        }

    }

}
