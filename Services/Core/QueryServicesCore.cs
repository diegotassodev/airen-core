namespace AirenApi.Services;
public static class QueryServicesCore {

    public static IServiceCollection AddApplicationServices(this IServiceCollection service) {
        
        service.AddScoped<AfiliacaoServices>();
        service.AddScoped<CapituloServices>();
        service.AddScoped<HabilidadeServices>();
        service.AddScoped<HobbyService>();
        service.AddScoped<ParentescoService>();
        service.AddScoped<PersonagemService>();
        service.AddScoped<PotentiaService>();
        service.AddScoped<TecnicaService>();
        service.AddScoped<VersaoService>();
        service.AddScoped<VolumeService>();

        return service;
    }
}
