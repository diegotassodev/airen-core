using Airen.Api.Repositories;
using Airen.Api.Services;
using Airen.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AirenConnection");

builder.Services.AddDbContext<AirenContext>(opts => opts.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddControllers();

builder.Services.AddCors
(
    options => options.AddPolicy
    (
        "wasm", policy => 
        {
            policy.WithOrigins("https://localhost:7268")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }
    )
);

builder.Services.AddApplicationRepositories();
builder.Services.AddScoped<IGeminiService, GeminiService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("wasm");

app.MapControllers();

app.Run();