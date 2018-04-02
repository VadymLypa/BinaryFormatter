using System;
using System.Collections.Generic;

namespace MyTechnicalTask.Models
{
    [Serializable]
    public class FileData
    {
        public List<string> Folders;
        public List<string> Files;

        public FileData(List<string> folders, List<string> files)
        {
            Folders = folders;
            Files = files;
        }
    }
}