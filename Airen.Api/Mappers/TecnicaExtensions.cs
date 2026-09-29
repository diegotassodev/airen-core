using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Api.Mappers;

public static class TecnicaExtensions 
{
    public static Tecnica MapToTecnica (this CreateTecnicaDto dto) 
    {
        return new Tecnica 
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Categoria = dto.Categoria,
            IdVersao = dto.IdVersao
        };
    }

    public static void MapToTecnica(this UpdateTecnicaDto dto, Tecnica tecnica) 
    {
        tecnica.Nome = dto.Nome;
        tecnica.Descricao = dto.Descricao;
        tecnica.Categoria = dto.Categoria;
        tecnica.IdVersao = dto.IdVersao;
    }

    public static ReadTecnicaDto MapToReadTecnica (this Tecnica obj) 
    {
        return new ReadTecnicaDto 
        {
            Id = obj.Id,
            Nome = obj.Nome,
            Descricao = obj.Descricao,
            Categoria = obj.Categoria,
            NomePersonagem = obj.Versao!.Personagem!.Nome
        };
    }
}