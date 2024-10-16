namespace Netflixs2.Application.Features;

public record UploadFile(string FileName, Stream FileStream) : IRequest<Netflixs2Result<string>>;

public class UploadFileHandler(IFileStorageService _fileStorageService) : IRequestHandler<UploadFile, Netflixs2Result<string>>
{
    private readonly IFileStorageService _fileStorageService = _fileStorageService;

    public async Task<Netflixs2Result<string>> Handle(UploadFile request, CancellationToken cancellationToken)
    {
        var filePath = await _fileStorageService.UploadFileAsync(request.FileName, request.FileStream);
        return new (filePath);
    }
}
