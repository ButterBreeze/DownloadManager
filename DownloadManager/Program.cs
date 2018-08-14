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
            
            FileSystemWatcher downloadFolderEventHandler = new FileSystemWatcher("/home/tally/Downloads");
            downloadFolderEventHandler.Changed += new FileSystemEventHandler(OnChanged);
            downloadFolderEventHandler.Created += new FileSystemEventHandler(OnChanged);
            downloadFolderEventHandler.Deleted += new FileSystemEventHandler(OnChanged);
            //downloadFolderEventHandler.Renamed += new FileSystemEventHandler(OnChanged);
        }




        public static void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                //gets all files in downloads folder
                String[] undesignatedFiles = Directory.GetFiles("/home/tally/Downloads/");     
                //This opens the unassigned files manger window to allow you to deal with them
                Process windowManager = new Process();
                windowManager.StartInfo.FileName =
                    "/home/tally/DevShtuffs/CSharpShtuff/windowManager/windowManager.exe";
                windowManager.StartInfo.CreateNoWindow = true;
                windowManager.StartInfo.UseShellExecute = false;
                
                //this combines the array of undesignated files and sends them to the window to deal with
                const String argsSeparator = " -";
                String args = String.Join(argsSeparator, undesignatedFiles);
                windowManager.StartInfo.Arguments = args;
                

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}