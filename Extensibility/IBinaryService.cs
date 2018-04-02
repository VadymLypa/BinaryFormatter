using Common.Models;

namespace Extensibility
{
    public interface IBinaryService
    {
        void Unpack(string dirPath, FileData binary);
    }
}