using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
                    Arguments = string.Format("--drop --host {0} -d {1} {2}", host, database, dataDir)
                };

                string response = string.Empty;

                using (var exeProcess = Process.Start(startInfo))
                {
                    var lineVal = exeProcess.StandardOutput.ReadLine();
                    while (lineVal != null)
                    {
                        response += lineVal;
                        lineVal = exeProcess.StandardOutput.ReadLine();
                    }
                    lineVal = exeProcess.StandardError.ReadLine();
                    while (lineVal != null)
                    {
                        response += lineVal;

                        lineVal = exeProcess.StandardError.ReadLine();
                    }
                    exeProcess.WaitForExit();
                }
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
        //private string Execute(string workingDirectory, string arguments)
        //{            
        //    var startInfo = new ProcessStartInfo("git.exe")
        //    {
        //        UseShellExecute = false,
        //        WorkingDirectory = workingDirectory,
        //        RedirectStandardInput = true,
        //        RedirectStandardOutput = true,
        //        RedirectStandardError = true,
        //        Arguments = arguments
        //    };

        //    var process = new Process
        //    {
        //        StartInfo = startInfo
        //    };
        //    process.Start();
        //    var response = new GitCommandResponse();

        //    var lineVal = process.StandardOutput.ReadLine();
        //    while (lineVal != null)
        //    {
        //        response.Output.Add(lineVal);
        //        lineVal = process.StandardOutput.ReadLine();
        //    }
        //    lineVal = process.StandardError.ReadLine();
        //    while (lineVal != null)
        //    {
        //        response.Error.Add(lineVal);
        //        lineVal = process.StandardError.ReadLine();
        //    }

        //    process.WaitForExit();
        //    return response;
        //}


    }
}