using System;
using System.Collections.Generic;

namespace MyTechnicalTask.Models
{
    [Serializable]
    public class FileData
    {
        public IEnumerable<string> Folders;
        public IEnumerable<string> Files;

        public FileData(IEnumerable<string> folders, IEnumerable<string> files)
        {
            Folders = folders;
            Files = files;
        }
    }
}