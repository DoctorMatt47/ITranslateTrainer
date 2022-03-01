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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
