using AirenApi.Data;
using AirenApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AirenConnection");

builder.Services.AddDbContext<AirenContext>(opts => opts.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddControllers();

builder.Services.AddApplicationServices();

var app = builder.Build();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();