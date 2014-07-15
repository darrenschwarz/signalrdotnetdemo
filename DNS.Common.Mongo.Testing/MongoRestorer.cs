using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MrSite.Common.Mongo.Testing
{
    public class MongoRestorer
    {
        public static bool Restore(string host, string database, string dataDir)
        {
            try
            {
                if (string.IsNullOrEmpty(host)) throw new ArgumentNullException("host");
                if (string.IsNullOrEmpty(database)) throw new ArgumentNullException("database");
                if (string.IsNullOrEmpty(dataDir)) throw new ArgumentNullException("database");

                if (!DataDirectoryExists(dataDir))
                    throw new DirectoryNotFoundException("Data directory not found.");

                if (!DataDirectoryHasContent(dataDir))
                    throw new Exception("Data directory is empty.");

                var startInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    FileName = "mongorestore.exe",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = string.Format("--drop --host {0} -d {1} {2}", host, database, dataDir)
                };


                using (var exeProcess = Process.Start(startInfo))
                    exeProcess.WaitForExit();

                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return false;
            }
        }

        //TODO [Ian,20140703] test these...
        protected static bool DataDirectoryExists(string dataDir)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var dataDirectory = Path.Combine(baseDir, dataDir);
            return Directory.Exists(dataDirectory);
        }

        protected static bool DataDirectoryHasContent(string dataDir)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var dataDirectory = Path.Combine(baseDir, dataDir);
            return Directory.GetFiles(dataDirectory).Any();
        }
    }
}