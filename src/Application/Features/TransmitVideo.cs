namespace Netflixs2.Application.Features;

public record TransmitVideo(string FilePath, string DestinationIPAddress, int DestinationPort) : IRequest<Netflixs2Result<bool>>;

public class TransmitVideoHandler(IFileStorageService fileStorageService) : IRequestHandler<TransmitVideo, Netflixs2Result<bool>>
{
    private readonly IFileStorageService _fileStorageService = fileStorageService;

    public async Task<Netflixs2Result<bool>> Handle(TransmitVideo request, CancellationToken cancellationToken)
    {
        using (var udpClient = new UdpClient())
        {
            var destinationEndpoint = new IPEndPoint(IPAddress.Parse(request.DestinationIPAddress), request.DestinationPort);
            await TransmitFileOverUdp(request.FilePath, udpClient, destinationEndpoint);
        }
        return new (true);
    }

    private async Task TransmitFileOverUdp(string filePath, UdpClient udpClient, IPEndPoint destinationEndpoint)
    {
        var fileStream = _fileStorageService.GetFile(filePath);
        var bufferSize = 1200;
        var buffer = new byte[bufferSize];
        int bytesRead;
        while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
        {
            var dataToSend = new byte[bytesRead];
            Array.Copy(buffer, dataToSend, bytesRead);
            await udpClient.SendAsync(dataToSend, dataToSend.Length, destinationEndpoint);
        }
    }
}
