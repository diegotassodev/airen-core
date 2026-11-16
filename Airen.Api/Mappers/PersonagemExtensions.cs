using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Api.Mappers;

public static class PersonagemExtensions 
{
    public static Personagem MapToPersonagem (this CreatePersonagemDto dto) 
    {
        return new Personagem 
        {
            Nome = dto.Nome,
            NomeOriginal = dto.NomeOriginal,
            Biografia = dto.Biografia,
            DataNascimento = dto.DataNascimento,
            EstaVivo = dto.EstaVivo
        };
    }

    public static Personagem MapToPersonagem(this UpdatePersonagemDto dto, Personagem personagem) 
    {
        personagem.Nome = dto.Nome;
        personagem.NomeOriginal = dto.NomeOriginal;
        personagem.Biografia = dto.Biografia;
        personagem.DataNascimento = dto.DataNascimento;
        personagem.EstaVivo = dto.EstaVivo;
        return personagem;
    }

    public static ReadPersonagemDto MapToReadPersonagem (this Personagem obj) 
    {
        return new ReadPersonagemDto 
        {
            Id = obj.Id,
            Nome = obj.Nome,
            NomeOriginal = obj.NomeOriginal,
            Biografia = obj.Biografia,
            DataNascimento = obj.DataNascimento,
            EstaVivo = obj.EstaVivo,
            Potentia = obj.Potentia?.MapToReadPotentiaDto(),
            Versoes = obj.Versoes?.Select(v => v.MapToReadVersao()).ToList(),
            Afiliacoes = obj.Afiliacoes?.Select(a => a.MapToReadAfiliacao()).ToList(),
            Parentes = obj.Parentes?.Select(p => p.MapToReadParentesco()).ToList()
        };
    }
}