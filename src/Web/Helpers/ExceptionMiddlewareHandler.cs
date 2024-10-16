namespace Netflixs2.Web.Helpers;

#pragma warning disable
public class ErrorDetails
{
    #region Properties
    public int StatusCode { get; }
    public string Message { get; }
    #endregion
    #region Constructors
    public ErrorDetails(int statusCode, string message) 
    { 
        StatusCode = statusCode;
        Message = message;
    }
    #endregion
    #region Methods
    public override string ToString() => JsonSerializer.Serialize<ErrorDetails>(this);
    #endregion
}

public class ExceptionMiddlewareHandler(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(new ErrorDetails
        (
            context.Response.StatusCode, 
            $"Netflixs2 internal Server Error: {ex} {ex.Message}"
        ).ToString());
    }
}
