using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace FolderListener.Core
{
    public class FolderListenerService
    {
        private System.Timers.Timer scheduler;
        public FolderListenerConfiguration folderListenerConfiguration { get; private set; }
        public delegate void ReadAction(string path);
        public event ReadAction OnRead; 
        public delegate void DirectoryNotFoundAction(string path);
        public event DirectoryNotFoundAction OnDirectoryNotFound;

        public FolderListenerService(FolderListenerConfiguration folderListenerConfiguration){
            this.folderListenerConfiguration = folderListenerConfiguration;
            this.Configure();
        }

        public void Configure(){
            
        }

        public void Start(){
            new Thread(new ThreadStart(this.ConfigureAndStartScheduler)).Start();
        }

        private void ConfigureAndStartScheduler()
        {
            this.scheduler = new System.Timers.Timer();
            this.scheduler.Interval = this.folderListenerConfiguration.FrequencyInSeconds * 1000;
            this.scheduler.Elapsed += async (sender, e) => await Task.Run(
                () => this.Listen()
            );
            this.scheduler.Start();
        }

        public void Stop(){
            this.scheduler.Stop();
        }

        public void Listen(){
            if(Directory.Exists(this.folderListenerConfiguration.Path)){
                foreach (var filePath in Directory.GetFiles(this.folderListenerConfiguration.Path))
                {
                    this.OnRead?.Invoke(filePath);
                }
            }else{
                this.OnDirectoryNotFound?.Invoke(this.folderListenerConfiguration.Path);
            }
        }
    }
}
