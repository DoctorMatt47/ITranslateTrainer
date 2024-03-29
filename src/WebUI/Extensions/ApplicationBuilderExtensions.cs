﻿namespace ITranslateTrainer.WebUI.Extensions;

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
                    context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                    context.Context.Response.Headers["Pragma"] = "no-cache";
                    context.Context.Response.Headers["Expires"] = "-1";
                },
            },
        });
    }
}
