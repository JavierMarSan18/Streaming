namespace Netflixs2.Web.GraphQL;

public record UploadFileInput(IFile File);

[MutationType]
public class UploadFileMutation
{
    
    public async Task<Netflixs2Result<string>> UploadFileAsync
    (
        IMediator mediator,
        UploadFileInput input
    )
    {
        var result =  await mediator.Send(new UploadFile(input.File.Name, input.File.OpenReadStream()));
        return result;
    }
}

