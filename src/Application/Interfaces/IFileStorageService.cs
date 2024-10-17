namespace Netflixs2.Application.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadFileAsync(string fileName, Stream stream);
    Stream GetFile(string filePath);
}