namespace Netflixs2.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddHealthChecks();
        services.AddConnections();
        services.AddEndpointsApiExplorer();
        services.AddGraphQLServer()
            .RegisterService<IMediator>()
            .AddNetflixs2Types()
            .AllowIntrospection(true)
            .AddUploadType()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
            .InitializeOnStartup();
        return services;
    }
}

public static class AppWebConfiguration
{
    public static void UseWeb(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddlewareHandler>();
        app.UseWebSockets();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });
    }
}
