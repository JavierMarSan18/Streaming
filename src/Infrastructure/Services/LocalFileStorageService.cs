
namespace Netflixs2.Infrastructure.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

    public async Task<string> UploadFileAsync(string fileName, Stream stream)
    {
        if (!Directory.Exists(_uploadPath))
            Directory.CreateDirectory(_uploadPath);
        var fileExtension = Path.GetExtension(fileName);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName).Replace(" ", "_");
        var filePath = Path.Combine(_uploadPath, $"{fileNameWithoutExtension}-{Guid.NewGuid()}{fileExtension}");
        using var fileStream = File.Create(filePath);
        stream.Seek(0, SeekOrigin.Begin);
        await stream.CopyToAsync(fileStream);
        return filePath;
    }

    public Stream GetFile(string filePath)
    {
        return File.OpenRead(filePath);
    }
}
