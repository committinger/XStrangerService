using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSSFramework.IO
{
    public sealed class PathUtils
    {
        private PathUtils() { }

        private static object _lockObj = new object();

        public static void ThreadSafeCreateDirectory(string directoryFullPath)
        {
            lock (_lockObj)
            {
                if (!Directory.Exists(directoryFullPath))
                    Directory.CreateDirectory(directoryFullPath);
            }
        }


    }
}
