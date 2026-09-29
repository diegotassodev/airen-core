using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Api.Repositories;

public static class QueryRepositoryCore {

    public static IServiceCollection AddApplicationRepositories(this IServiceCollection service) {
        
        service.AddScoped<IRepository<Afiliacao, ReadAfiliacaoDto>, AfiliacaoRepository>();
        service.AddScoped<IRepository<Capitulo, ReadCapituloDto>, CapituloRepository>();
        service.AddScoped<IRepository<Habilidade, ReadHabilidadeDto>, HabilidadeRepository>();
        service.AddScoped<IRepository<Hobby, ReadHobbyDto>, HobbyRepository>();
        service.AddScoped<IRepository<Parentesco, ReadParentescoDto>, ParentescoRepository>();
        service.AddScoped<IRepository<Personagem, ReadPersonagemDto>, PersonagemRepository>();
        service.AddScoped<IRepository<Potentia, ReadPotentiaDto>, PotentiaRepository>();
        service.AddScoped<IRepository<Tecnica, ReadTecnicaDto>, TecnicaRepository>();
        service.AddScoped<IRepository<Versao, ReadVersaoDto>, VersaoRepository>();
        service.AddScoped<IRepository<Volume, ReadVolumeDto>, VolumeRepository>();

        return service;
    }
}
