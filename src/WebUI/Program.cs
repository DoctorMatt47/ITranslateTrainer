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
    .AddControllers()
    .AddJsonConverters();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseSwagger().UseSwaggerUI().UseFileServerWithoutCaching();
else
    app.UseFileServer();

app.UseExceptionHandler("/error")
    .UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();

app.Run();
