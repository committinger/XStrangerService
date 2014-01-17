using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSSFramework.IO
{
    public sealed class FileUtils
    {
        private FileUtils() { }

        private static object _lockObj = new object();

        public static void ThreadsafeAppendText(string fileFullPath, string content)
        {
            string directoryPath = Path.GetDirectoryName(fileFullPath);
            if (!Directory.Exists(directoryPath))
                PathUtils.ThreadSafeCreateDirectory(directoryPath);
            threadsafeAppendText(fileFullPath, content, Encoding.UTF8);
        }

        private static void threadsafeAppendText(string fileFullPath, string content, Encoding encoding)
        {
            lock (_lockObj)
            {
                File.AppendAllText(fileFullPath, content, encoding);
            }
        }
    }
}
