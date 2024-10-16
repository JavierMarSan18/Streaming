namespace Netflixs2.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddHealthChecks();
        services.AddConnections();
        services.AddEndpointsApiExplorer();
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
