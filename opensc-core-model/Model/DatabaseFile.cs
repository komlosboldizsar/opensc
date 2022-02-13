using OpenSC.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    public class DatabaseFile: IDisposable
    {

        private DatabaseFileMode mode;

        public FileStream Stream { get; private set; }

        public DatabaseFile(string fileName, DatabaseFileMode mode)
        {
            this.mode = mode;
            FileExtensions.CreateDirectoriesForFile(fileName);
            if (mode == DatabaseFileMode.Read)
                Stream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Read);
            else
                Stream = File.Open(fileName, FileMode.Create);
        }

        public void Dispose()
        {
            Stream.Close();
            Stream = null;
        }

        public enum DatabaseFileMode
        {
            Read,
            Write
        }
    }
}
