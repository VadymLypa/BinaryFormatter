using MyTechnicalTask.Models;

namespace MyTechnicalTask.Abstractions
{
    public interface IBinaryService
    {
        void Unpack(string dirPath, FileData binary);
    }
}