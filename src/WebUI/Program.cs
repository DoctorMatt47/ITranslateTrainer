using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Infrastructure.Extensions;
using ITranslateTrainer.WebUI.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddApplication()
    .AddInfrastructure(connectionString)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseSwagger().UseSwaggerUI().UseFileServerWithoutCaching();
else
    app.UseFileServer();

app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseExceptionHandler("/error")
    .UseAuthorization();

app.MapControllers();

app.Run();