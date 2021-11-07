using System;
using System.Configuration;
using System.IO;

namespace eBayPublisher
{
    /// <summary>
    /// For more information about the FileProcessor type check out the following article:
    /// http://cgeers.wordpress.com/2009/08/14/monitoring-a-directory/
    /// </summary>
    class Program
    {
        private static FileSystemWatcher watcher;
        private static readonly FileProcessor fileProcessor = new EBayFileProcessor();

        static void Main()
        {
            // In which directory are the XML files placed?
            string path = ConfigurationManager.AppSettings["ebayDirectory"];

            // Watch the directory for any new incoming XML files.
            watcher = new FileSystemWatcher { Path = path, Filter = "*.xml" };
            watcher.Created += file_Created;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Monitoring Northwind Product Catalog...");
            Console.WriteLine();
            Console.ReadLine();
        }

        static void file_Created(object sender, FileSystemEventArgs e)
        {
            // Enqueue the new file in the eBay file processor
            fileProcessor.EnqueueFile(e.FullPath);
        }
    }
}
