using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace OpenSC.Model.SourceGenerators
{

    internal static class ProjectSourcePath
    {
        public static string GetSourceFilePath([CallerFilePath] string callerFilePath = null) => callerFilePath ?? "";
    }
}
