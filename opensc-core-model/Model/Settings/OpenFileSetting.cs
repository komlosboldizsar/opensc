using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Settings
{

    public class OpenFileSetting : Setting<string>
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

        public string SetValueWithCopy(string originalPath)
        {
            if (!CopyEnabled)
                throw new InvalidOperationException($"{nameof(SetValueWithCopy)}() method can't be called when {nameof(CopyEnabled)} property is set to false.");
            Value = copyToAppFolderMethod(originalPath);
            return Value;
        }

    }

}
