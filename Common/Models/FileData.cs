using System;
using System.Collections.Generic;

namespace Common.Models
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