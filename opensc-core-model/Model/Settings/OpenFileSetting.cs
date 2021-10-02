using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings
{

    public class OpenFileSetting : Setting<OpenFilePath>
    {

        public OpenFileSetting(string key, string category, string humanReadableTitle, string humanReadableDescription, string defaultValue, string fileFilter = "", bool copyEnabled = false, CopyToAppFolderDelegate copyToAppFolderMethod = null)
            : base(key, category, humanReadableTitle, humanReadableDescription, defaultValue)
        {
            FileFilter = fileFilter;
            CopyEnabled = copyEnabled;
            this.copyToAppFolderMethod = copyToAppFolderMethod;
        }

        public string FileFilter { get; private set; }

        public bool CopyEnabled { get; private set; }

        public delegate string CopyToAppFolderDelegate(string originalPath);
        private CopyToAppFolderDelegate copyToAppFolderMethod;

        public string SetValueWithCopy(OpenFilePath originalPath)
        {
            if (!CopyEnabled)
                throw new InvalidOperationException($"{nameof(SetValueWithCopy)}() method can't be called when {nameof(CopyEnabled)} property is set to false.");
            Value = copyToAppFolderMethod(originalPath);
            return Value;
        }

    }

    public class OpenFilePath
    {
        public string Path { get; set; }
        public OpenFilePath(string path) => Path = path;
        public override string ToString() => Path;
        public static implicit operator string(OpenFilePath ofp) => ofp.Path;
        public static implicit operator OpenFilePath(string str) => new OpenFilePath(str);
    }

}
