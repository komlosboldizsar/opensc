using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenSC.Model.SourceGenerators
{
    internal class TemplateEngine
    {

        private const string EXTENSION_CS = ".cs";
        private const string EXTENSION_CSTEMPLATE = ".cstemplate";

        private string baseName;
        private string directory;

        public TemplateEngine(string ownerFilePath, string directoryRelative)
        {
            if (!ownerFilePath.EndsWith(EXTENSION_CS))
                throw new ArgumentException(nameof(ownerFilePath));
            string fileName = Path.GetFileName(ownerFilePath);
            baseName = fileName.Substring(0, fileName.Length - EXTENSION_CS.Length);
            directory = Path.GetDirectoryName(ownerFilePath) + Path.DirectorySeparatorChar + directoryRelative + Path.DirectorySeparatorChar;
        }

        public Template Load(string templateName)
        {   
            string templatePath = directory + baseName + "." + templateName + EXTENSION_CSTEMPLATE;
            string templateContent = File.ReadAllText(templatePath);
            return new Template(templateContent);
        }

    }

}
