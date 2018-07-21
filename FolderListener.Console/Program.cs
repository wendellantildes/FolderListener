using System;
using System.IO;
using FolderListener.Core;

namespace FolderListener.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentPath = AppDomain.CurrentDomain.BaseDirectory;
            System.Console.WriteLine($"Folder: {currentPath}");
            var seconds = 5;
            var folderListener = new FolderListenerService(
                new FolderListenerConfiguration(seconds, currentPath)
            );
            folderListener.OnRead += (path) => {
                System.Console.WriteLine($"Readind :{new FileInfo(path).FullName} at {DateTime.Now}");
            };
            folderListener.OnDirectoryNotFound += (path) => {
                System.Console.WriteLine($"Folder not found: {path}");
            };
            System.Console.WriteLine("Press y/n to start/stop the service and any different key to quit the program.");
            while(true){
                var nextChar = System.Console.ReadLine();
                if(nextChar == "y"){
                    System.Console.WriteLine("Starting listener.");
                    folderListener.Start();

                }else if(nextChar == "n"){
                    System.Console.WriteLine("Stopping listener.");
                    folderListener.Stop();
                }else{
                    System.Console.WriteLine("Bye.");
                    break;
                }
            }
        }
    }
}
