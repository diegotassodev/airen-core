using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Api.Repositories;

public static class QueryRepositoryCore 
{
    public static IServiceCollection AddApplicationRepositories(this IServiceCollection service) 
    {
        service.AddScoped<IRepository<Afiliacao, ReadAfiliacaoDto, UpdateAfiliacaoDto>, AfiliacaoRepository>();
        service.AddScoped<IRepository<Capitulo, ReadCapituloDto, UpdateCapituloDto>, CapituloRepository>();
        service.AddScoped<IRepository<Habilidade, ReadHabilidadeDto, UpdateHabilidadeDto>, HabilidadeRepository>();
        service.AddScoped<IRepository<Hobby, ReadHobbyDto, UpdateHobbyDto>, HobbyRepository>();
        service.AddScoped<IRepository<Parentesco,ReadParentescoDto, UpdateParentescoDto>, ParentescoRepository>();
        service.AddScoped<IRepository<Personagem, ReadPersonagemDto, UpdatePersonagemDto>, PersonagemRepository>();
        service.AddScoped<IRepository<Potentia, ReadPotentiaDto, UpdatePotentiaDto>, PotentiaRepository>();
        service.AddScoped<IRepository<Tecnica, ReadTecnicaDto, UpdateTecnicaDto>, TecnicaRepository>();
        service.AddScoped<IRepository<Versao, ReadVersaoDto, UpdateVersaoDto>, VersaoRepository>();
        service.AddScoped<IRepository<Volume, ReadVolumeDto, UpdateVolumeDto>, VolumeRepository>();

        return service;
    }
}
