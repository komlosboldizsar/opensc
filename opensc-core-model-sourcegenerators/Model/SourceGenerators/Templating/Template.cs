using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSC.Model.SourceGenerators
{
    internal class Template
    {
        public string Content { get; private set; }
        public Template(string content) => Content = content;
        public SubstituedTemplate GetInstance() => new SubstituedTemplate(this);
        public string GetInstanceContent(Dictionary<string, string> substitues) => (new SubstituedTemplate(this)).Substitue(substitues).ToString();
    }
}
