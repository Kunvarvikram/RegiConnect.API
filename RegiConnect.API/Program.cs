using Microsoft.Extensions.DependencyInjection;
using Regiconnect.Api.Data;
using Regiconnect.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure DatabaseHelper and Dependency Injection
builder.Services.AddSingleton<DatabaseHelper>();

// Register repositories
builder.Services.AddScoped<SProcSignupRepository>();
builder.Services.AddScoped<SProcLoginRepository>();

// Register services
builder.Services.AddScoped<SProcSignupService>();
builder.Services.AddScoped<SProcLoginService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
