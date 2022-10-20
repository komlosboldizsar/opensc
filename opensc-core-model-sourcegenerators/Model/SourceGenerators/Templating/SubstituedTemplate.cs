using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenSC.Model.SourceGenerators
{
    internal class SubstituedTemplate
    {

        public Template Parent { get; private set; }
        public string Content { get; private set; }

        public SubstituedTemplate(Template parent)
        {
            Parent = parent;
            Content = parent.Content;
        }

        public override string ToString() => Content;
        public static implicit operator string(SubstituedTemplate substituedTemplate) => substituedTemplate.ToString();

        public SubstituedTemplate Substitue(Dictionary<string, string> substitues)
        {
            Content = REGEX_SUBSTITUE.Replace(Content, match => {
                string varname = match.Groups[REGEX_SUBSTITUE_GROUPNAME_VARNAME].Value;
                string hashtag = match.Groups[REGEX_SUBSTITUE_GROUPNAME_HASHTAG].Value;
                if (!substitues.TryGetValue(varname, out string substitue))
                    return string.Empty;
                if ((hashtag == REGEX_SUBSTITUE_HASHTAG) && !string.IsNullOrWhiteSpace(substitue))
                    substitue += " ";
                return substitue;
            });
            return this;
        }

        private static readonly Regex REGEX_SUBSTITUE = new Regex($@"{{(?<{REGEX_SUBSTITUE_GROUPNAME_VARNAME}>[a-zA-Z0-9_]+)(?<{REGEX_SUBSTITUE_GROUPNAME_HASHTAG}>{REGEX_SUBSTITUE_HASHTAG}?)}}");
        private const string REGEX_SUBSTITUE_GROUPNAME_VARNAME = "varname";
        private const string REGEX_SUBSTITUE_GROUPNAME_HASHTAG = "hashtag";
        private const string REGEX_SUBSTITUE_HASHTAG = "#";

        public SubstituedTemplate InsertBlock(string placeholder, string content)
        {
            string regexp = string.Format(REGEX_INSERTBLOCK_TEMPLATE, placeholder);
            Content = Regex.Replace(Content, regexp, match => {
                string startWhites = match.Groups[REGEX_INSERTBLOCK_GROUPNAME_STARTWHITES].Value;
                string endWhites = match.Groups[REGEX_INSERTBLOCK_GROUPNAME_ENDWHITES].Value;
                return startWhites + content.Replace("\r\n", $"\r\n{startWhites}") + endWhites;
            }, RegexOptions.Multiline);
            return this;
        }

        public SubstituedTemplate InsertBlock(string placeholder, StringBuilder contentBuilder)
            => InsertBlock(placeholder, contentBuilder.ToString());

        private static readonly string REGEX_INSERTBLOCK_TEMPLATE = $@"^(?<{REGEX_INSERTBLOCK_GROUPNAME_STARTWHITES}>\s+)(?<{REGEX_INSERTBLOCK_GROUPNAME_PLACEHOLDER}>__{{0}}__)(?<{REGEX_INSERTBLOCK_GROUPNAME_ENDWHITES}>\s+)$";
        private const string REGEX_INSERTBLOCK_GROUPNAME_STARTWHITES = "startwhites";
        private const string REGEX_INSERTBLOCK_GROUPNAME_PLACEHOLDER = "placeholder";
        private const string REGEX_INSERTBLOCK_GROUPNAME_ENDWHITES = "endwhites";

    }
}
