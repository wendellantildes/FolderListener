using System;
namespace FolderListener.Core
{
    public struct FolderListenerConfiguration
    {
        public int FrequencyInSeconds { get; private set; }
        public string Path { get; private set; }

        public FolderListenerConfiguration(int frequencyInSeconds, string path){
            this.FrequencyInSeconds = frequencyInSeconds;
            this.Path = path;
        }
    }
}
