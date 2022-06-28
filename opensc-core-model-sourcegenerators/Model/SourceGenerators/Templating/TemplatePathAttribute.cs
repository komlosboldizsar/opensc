using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSC.Model.SourceGenerators
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    internal class TemplateFileNameAttribute : Attribute
    {
        public string FileName { get; set; }
        public TemplateFileNameAttribute(string fileName) => FileName = fileName;
    }
}
