using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Logger
{
    class FileLogger : ILogReceiver
    {

        private string filePath;

        public FileLogger(string directoryPath, string fileName, bool addTimestamp = true, bool autoSubscribe = true, LogMessageType minimumLevel = LogMessageType.Info)
        {

            string timestampStr = "";
            if (addTimestamp)
                timestampStr = DateTime.Now.ToString("-yyyyMMdd-HHmmss");
            filePath = string.Format("{0}{1}{2}{3}.log",
                directoryPath,
                Path.PathSeparator,
                fileName,
                timestampStr);

            if (autoSubscribe)
                LogDispatcher.Subscribe(this, minimumLevel);

        }

        public void ReceiveLogMessage(LogMessageType messageType, DateTime timestamp, string tag, string message)
        {
            writeToFile(string.Format("[{0}] [{1}] [{2}] {3}",
                timestamp.ToString("yyyy.MM.dd. HH':'mm':'ss"),
                convertTypeToString(messageType),
                tag,
                message));
        }

        private void writeToFile(string str)
        {
            using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Write, FileShare.None))
            using(StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(str);
            }
        }

        private string convertTypeToString(LogMessageType messageType)
        {
            switch (messageType)
            {
                case LogMessageType.Verbose:
                    return "VERBOSE";
                case LogMessageType.Info:
                    return "INFO";
                case LogMessageType.Warn:
                    return "WARN";
                case LogMessageType.Error:
                    return "ERROR";
            }
            return "?";
        }

    }
}
