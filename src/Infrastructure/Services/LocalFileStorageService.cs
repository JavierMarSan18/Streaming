
namespace Netflixs2.Infrastructure.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

    public async Task<string> UploadFileAsync(string fileName, Stream stream)
    {
        if (!Directory.Exists(_uploadPath))
            Directory.CreateDirectory(_uploadPath);
        var fileExtension = Path.GetExtension(fileName);
        var onlyFileName = Path.GetFileNameWithoutExtension(fileName);
        var filePath = Path.Combine(_uploadPath, $"{onlyFileName}-{Guid.NewGuid()}{fileExtension}").Replace(" ", "_");
        using var fileStream = File.Create(filePath);
        stream.Seek(0, SeekOrigin.Begin);
        await stream.CopyToAsync(fileStream);
        return filePath;
    }
}
