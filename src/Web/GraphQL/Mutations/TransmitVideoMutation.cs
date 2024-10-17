namespace Netflixs2.Web.GraphQL;

[MutationType]
public class TransmitVideoMutation
{
    public async Task<Netflixs2Result<bool>> TransmitVideo
    (
        IMediator mediator,
        TransmitVideo input
    ) => await mediator.Send(input);
}