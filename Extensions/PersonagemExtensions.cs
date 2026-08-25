using AirenApi.Data.DTOs;
using AirenApi.Models;
namespace AirenApi.Profiles;

public static class PersonagemExtensions {

    public static Personagem MapToPersonagem (this CreatePersonagemDto dto) {
        return new Personagem {
            Nome = dto.Nome,
            NomeOriginal = dto.NomeOriginal,
            Biografia = dto.Biografia,
            DataNascimento = dto.DataNascimento,
            EstaVivo = dto.EstaVivo
        };
    }

    public static void MapToPersonagem(this UpdatePersonagemDto dto, Personagem personagem) {
        personagem.Nome = dto.Nome;
        personagem.NomeOriginal = dto.NomeOriginal;
        personagem.Biografia = dto.Biografia;
        personagem.DataNascimento = dto.DataNascimento;
        personagem.EstaVivo = dto.EstaVivo;
    }

    public static ReadPersonagemDto MapToReadPersonagem (this Personagem obj) {
        return new ReadPersonagemDto {
            Id = obj.Id,
            Nome = obj.Nome,
            NomeOriginal = obj.NomeOriginal,
            Biografia = obj.Biografia,
            DataNascimento = obj.DataNascimento,
            EstaVivo = obj.EstaVivo,
            Potentia = obj.Potentia?.MapToReadPotentiaDto(),
            Versoes = (ICollection<ReadVersaoDto>)obj.Versoes!.Select(v => v.MapToReadVersao()).ToList(),
            Afiliacoes = (ICollection<ReadAfiliacaoDto>)obj.Afiliacoes!.Select(a => a.MapToReadAfiliacao()).ToList(),
            Parentes = (ICollection<ReadParentescoDto>)obj.Parentes!.Select(p => p.MapToReadParentesco()).ToList()

        };
    }
}