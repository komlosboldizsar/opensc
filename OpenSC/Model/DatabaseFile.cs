using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model
{
    class DatabaseFile: IDisposable
    {

        private string fileName;

        public FileStream Stream { get; private set; }

        public DatabaseFile(string fileName)
        {
            this.fileName = fileName;
            Stream = File.Create(fileName);
        }

        public void Dispose()
        {
            Stream.Close();
            Stream = null;
        }
    }
}
