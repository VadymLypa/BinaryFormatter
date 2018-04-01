using System;
using System.Collections.Generic;
using System.IO;

namespace MyTechnicalTask.Models
{
    [Serializable]
    public class BinaryManager
    {
        private List<string> _folders;
        private List<string> _files;

        public BinaryManager()
        {
            _folders = new List<string>();
            _files = new List<string>();
        }

        public BinaryManager(string[] folders, string[] files)
        {
            _folders = new List<string>();
            _files = new List<string>();

            foreach (var folder in folders)
            {
                _folders.Add(folder);
            }

            foreach (var file in files)
            {
                _files.Add(file);
            }
        }

        public void Unpack(string dirPath)
        {
            if (dirPath != null)
            {
                string oldPath = Path.GetDirectoryName(_folders[0]);
                foreach (var folder in _folders)
                {
                    string newPath = folder.Replace(oldPath, dirPath);
                    Directory.CreateDirectory(newPath);
                }

                foreach (var file in _files)
                {
                    string newPath = file.Replace(oldPath, dirPath);
                    File.Create(newPath);
                }
            }
            
        }

    }
}