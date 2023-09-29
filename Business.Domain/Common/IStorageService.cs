

namespace Business.Domain.Common
{
    public interface IStorageService
    {
        Task SaveFileAsync(Stream mediaBinaryStream, string folderName, string fileName);
        Task DeleteFileAsync(string fileName);
    }
}
