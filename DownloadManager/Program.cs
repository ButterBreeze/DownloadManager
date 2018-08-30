/**
 * Program: Download Manager background task
 * Programmer: ButterBreeze
 * What this program does: Has an event listener on file changes in download manager. It will 0
 *
 *
 * TODO: Create a detection if the program is already running
 * TODO: Create a log for fails
 * TODO: Create a temp file deleter every month
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace DownloadManager
{
    internal class Program
    {
        
        /// <summary>
        ///    Entry point to the program
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)

        {

            OnChanged(null, null);
            //FileStream file = new FileStream("downloadWatcher.txt", );
            FileSystemWatcher downloadFolderEventHandler = new FileSystemWatcher("C:\\Users\\tally\\Downloads\\");
            downloadFolderEventHandler.Changed += new FileSystemEventHandler(OnChanged);
            downloadFolderEventHandler.Created += new FileSystemEventHandler(OnChanged);
            downloadFolderEventHandler.Deleted += new FileSystemEventHandler(OnChanged);
            //downloadFolderEventHandler.Renamed += new FileSystemEventHandler(OnChanged);
            downloadFolderEventHandler.EnableRaisingEvents = true;
            Console.WriteLine("Please enter '-' to exit program");
            while (Console.Read() != '-') ;

        }




        public static void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {

                String folderInQuestion = "C:\\Users\\tally\\Downloads\\";
                //gets all files in downloads folder
                String[] undesignatedFiles = Directory.GetFiles(folderInQuestion);     
                //This opens the unassigned files manger window to allow you to deal with them
                Process windowManager = new Process();
                windowManager.StartInfo.FileName =
                    "C:\\Users\\tally\\source\\repos\\FileMangerForm\\FileMangerForm\\bin\\Debug\\FileMangerForm.exe";
                //windowManager.StartInfo.CreateNoWindow = true;
                //windowManager.StartInfo.UseShellExecute = false;
                
                //this combines the array of undesignated files and sends them to the window to deal with
                const String argsSeparator = "\" \"";
                String args = "\"" + folderInQuestion + "\\\" \""+ String.Join(argsSeparator, undesignatedFiles)+ "\"";
                Console.WriteLine(args);
                windowManager.StartInfo.Arguments = args;
                windowManager.Start();

                Console.WriteLine("Passed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed" + ex.StackTrace.ToString());                
                Console.ReadKey();
            }  
        }
    }
}