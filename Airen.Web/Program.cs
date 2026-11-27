using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Airen.Web;
using Airen.Web.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiServer = builder.Configuration["API-Server"];

builder.Services.AddTransient<AfiliacoesAPI>();
builder.Services.AddTransient<CapitulosAPI>();
builder.Services.AddTransient<IGeminiAPI, GeminiAPI>();
builder.Services.AddTransient<HabilidadesAPI>();
builder.Services.AddTransient<HobbiesAPI>();
builder.Services.AddTransient<ParentescoAPI>();
builder.Services.AddTransient<PersonagensAPI>();
builder.Services.AddTransient<PotentiasAPI>();
builder.Services.AddTransient<TecnicasAPI>();
builder.Services.AddTransient<VersoesAPI>();
builder.Services.AddTransient<VolumesAPI>();

builder.Services.AddHttpClient("API", client => {
    client.BaseAddress = new Uri(apiServer!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

await builder.Build().RunAsync();
