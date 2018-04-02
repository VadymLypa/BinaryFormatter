using System.IO;
using System.Linq;
using MyTechnicalTask.Abstractions;
using MyTechnicalTask.Models;

namespace MyTechnicalTask.Services
{
    public class BinaryService : IBinaryService
    {   
        public void Unpack(string dirPath, FileData binary)
        {
            if (dirPath != null)
            {
                string oldPath = Path.GetDirectoryName(binary.Folders.FirstOrDefault());
                foreach (var folder in binary.Folders)
                {
                    string newPath = folder.Replace(oldPath, dirPath);
                    Directory.CreateDirectory(newPath);
                }

                foreach (var file in binary.Files)
                {
                    string newPath = file.Replace(oldPath, dirPath);
                    File.Create(newPath);
                }
            }
        }
    }
}