using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace DNS.Common.Mongo.Testing
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
                    Arguments = string.Format("--drop --host {0} -d {1} {2}", host, database, dataDir),
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                var response = new StringBuilder();                

                using (var exeProcess = Process.Start(startInfo))
                {
                    while (exeProcess != null && !exeProcess.HasExited) //NOTE [Darren,20140715] modified as per http://stackoverflow.com/questions/1390559/how-to-get-the-output-of-a-system-diagnostics-process
                    {
                        response.Append(exeProcess.StandardOutput.ReadToEnd());
                        response.Append(exeProcess.StandardError.ReadToEnd());
                        //TODO [Darren,20140715] Add logging to this class?
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message); //TODO [Darren,20140715] Should worry about exception handling in utilities like this? Leave todo's sufficient?
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