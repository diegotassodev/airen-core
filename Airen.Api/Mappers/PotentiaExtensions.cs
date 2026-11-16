using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Api.Mappers;

public static class PotentiaExtensions 
{
    public static Potentia MapToPotentia (this CreatePotentiaDto dto) 
    {
        return new Potentia 
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            LinkMusica = dto.LinkMusica,
            PersonagemId = dto.PersonagemId
        };
    }

    public static Potentia MapToPotentia(this UpdatePotentiaDto dto, Potentia potentia) 
    {
        potentia.Nome = dto.Nome;
        potentia.Descricao = dto.Descricao;
        potentia.LinkMusica = dto.LinkMusica;
        potentia.PersonagemId = dto.PersonagemId;
        return potentia;
    }

    public static ReadPotentiaDto MapToReadPotentiaDto (this Potentia obj) 
    {
        return new ReadPotentiaDto 
        {
            Id = obj.Id,
            Nome = obj.Nome,
            Descricao = obj.Descricao,
            LinkMusica = obj.LinkMusica,
            NomePersonagem = obj.Personagem!.Nome,
            ListaHabilidades = obj.Habilidades?.Select(h => h.MapToReadHabilidade()).ToList()
        };
    }
}