using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Api.Mappers;

public static class ParentescoExtensions 
{
    public static Parentesco MapToParentesco (this CreateParentescoDto dto) 
    {
        return new Parentesco 
        {
            IdPersonagem = dto.IdPersonagem,
            IdParente = dto.IdParente,
            TipoParentesco = dto.TipoParentesco
        };
    }

    public static void MapToParentesco(this UpdateParentescoDto dto, Parentesco parentesco) 
    {
        parentesco.IdPersonagem = dto.IdPersonagem;
        parentesco.IdParente = dto.IdParente;
        parentesco.TipoParentesco = dto.TipoParentesco;
    }

    public static ReadParentescoDto MapToReadParentesco (this Parentesco obj) 
    {
        return new ReadParentescoDto 
        {
            Id = obj.Id,
            NomePersonagem = obj.Personagem!.Nome,
            NomeParente = obj.Parente!.Nome,
            TipoParentesco = obj.TipoParentesco
        };
    }
}