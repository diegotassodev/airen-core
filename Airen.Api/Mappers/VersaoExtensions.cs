using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Api.Mappers;

public static class VersaoExtensions {

    public static Versao MapToVersao (this CreateVersaoDto dto) {
        return new Versao {
            Nome = dto.Nome,
            Genero = dto.Genero,
            UrlImagem = dto.UrlImagem,
            IdadeInicial = dto.IdadeInicial,
            IdadeFinal = dto.IdadeFinal,
            Descricao = dto.Descricao,
            Personalidade = dto.Personalidade,
            IdPersonagem = dto.IdPersonagem
        };
    }

    public static void MapToVersao(this UpdateVersaoDto dto, Versao versao) {
        versao.Nome = dto.Nome;
        versao.Genero = dto.Genero;
        versao.UrlImagem = dto.UrlImagem;
        versao.IdadeInicial = dto.IdadeInicial;
        versao.IdadeFinal = dto.IdadeFinal;
        versao.Descricao = dto.Descricao;
        versao.Personalidade = dto.Personalidade;
        versao.IdPersonagem = dto.IdPersonagem;
    }

    public static ReadVersaoDto MapToReadVersao (this Versao obj) {
        return new ReadVersaoDto {
            Id = obj.Id,
            NomePersonagem = obj.Personagem!.Nome,
            Nome = obj.Nome,
            Personalidade = obj.Personalidade,
            Descricao = obj.Descricao,
            Genero = obj.Genero,
            UrlImagem = obj.UrlImagem,
            IdadeInicial = obj.IdadeInicial,
            IdadeFinal = obj.IdadeFinal,
            Tecnicas = (ICollection<ReadTecnicaDto>)obj.Tecnicas!.Select(t => t.MapToReadTecnica()).ToList(),
            Hobbies = (ICollection<ReadHobbyDto>)obj.Hobbies!.Select(h => h.MapToReadHobby()).ToList(),
            Volumes = (ICollection<ReadVolumeDto>)obj.Volumes!.Select(V => V.MapToReadVolume()).ToList()
        };
    }
}