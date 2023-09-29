using Microsoft.AspNetCore.Hosting;

namespace Business.Domain.Common
{
    public class StorageService : IStorageService
    {
        private readonly string _userContentFolder;

        public StorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = webHostEnvironment.WebRootPath;
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string folderName, string fileName)
        {
            var folferPath = Path.Combine(_userContentFolder, folderName);
            var filePath = Path.Combine(folferPath, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }
    }
}
