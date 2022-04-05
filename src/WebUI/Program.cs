using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Infrastructure.Extensions;
using ITranslateTrainer.WebUI.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Adds services
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

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();