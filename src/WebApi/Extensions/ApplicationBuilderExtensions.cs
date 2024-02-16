namespace ITranslateTrainer.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    // ReSharper disable once UnusedMethodReturnValue.Global
    public static IApplicationBuilder UseFileServerWithoutCaching(this IApplicationBuilder app)
    {
        return app.UseFileServer(new FileServerOptions
        {
            StaticFileOptions =
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers[key: "Cache-Control"] = "no-cache, no-store";
                    context.Context.Response.Headers[key: "Pragma"] = "no-cache";
                    context.Context.Response.Headers[key: "Expires"] = "-1";
                },
            },
        });
    }
}
