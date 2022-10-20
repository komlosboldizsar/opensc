using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenSC.Model.SourceGenerators
{
    public abstract class SourceGeneratorBase<TSyntaxReceiver> : ISourceGenerator
        where TSyntaxReceiver : ISyntaxContextReceiver, new()
    {

        public void Initialize(GeneratorInitializationContext initializationContext)
        {
            sourcePath = _getSourcePath();
            initializationContext.RegisterForSyntaxNotifications(() => new TSyntaxReceiver());
            initTemplates();
            _initialize(initializationContext);
        }

        public void Execute(GeneratorExecutionContext executionContext)
        {
            if (!(executionContext.SyntaxContextReceiver is TSyntaxReceiver receiver))
                return;
            _execute(executionContext, receiver);
        }

        protected virtual void _initialize(GeneratorInitializationContext initializationContext) { }
        protected abstract void _execute(GeneratorExecutionContext executionContext, TSyntaxReceiver receiver);

        #region Path things
        private string sourcePath;
        protected abstract string _getSourcePath();
        #endregion

        #region File creation
        protected void createFile(GeneratorExecutionContext executionContext, string baseName, string sourceContent)
        {
            string generatorName = Path.GetFileName(sourcePath);
            executionContext.AddSource($"{baseName}.{generatorName}.g.cs", SourceText.From(sourceContent, Encoding.UTF8));
        }
        #endregion

        #region Templates
        protected virtual string TemplateDirectory { get; } = "Templates";

        private TemplateEngine defaultTemplateEngine;
        
        private void initTemplates()
        {
            defaultTemplateEngine = new TemplateEngine(sourcePath, TemplateDirectory);
            foreach (FieldInfo templateFieldInfo in GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (templateFieldInfo.FieldType == typeof(Template))
                {
                    TemplateFileNameAttribute attr = templateFieldInfo.GetCustomAttribute<TemplateFileNameAttribute>();
                    if (attr != null)
                    {
                        Template template = defaultTemplateEngine.Load(attr.FileName);
                        templateFieldInfo.SetValue(this, template);
                    }
                }
            }
        }
        #endregion

    }

}