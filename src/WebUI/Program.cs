using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Infrastructure.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adds services
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Adds controllers
builder.Services.AddControllers();

// Adds swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseFileServer(new FileServerOptions
    {
        StaticFileOptions =
        {
            OnPrepareResponse = context =>
            {
                context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                context.Context.Response.Headers["Pragma"] = "no-cache";
                context.Context.Response.Headers["Expires"] = "-1";
            }
        }
    });
}
else
{
    app.UseFileServer();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();