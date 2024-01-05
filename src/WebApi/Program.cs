using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Infrastructure.Extensions;
using ITranslateTrainer.WebApi.Extensions;
using ITranslateTrainer.WebApi.JsonConverters;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddApplication()
    .AddInfrastructure(connectionString)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new OneOfJsonConverterFactory()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI();
}

app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseExceptionHandler("/error")
    .UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
