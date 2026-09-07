using Airen.Api.Data.DTOs;
using Airen.Api.Models;
namespace Airen.Api.Profiles;

public static class PotentiaExtensions {

    public static Potentia MapToPotentia (this CreatePotentiaDto dto) {
        return new Potentia {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            LinkMusica = dto.LinkMusica,
            PersonagemId = dto.PersonagemId
        };
    }

    public static void MapToPotentia(this UpdatePotentiaDto dto, Potentia potentia) {
        potentia.Nome = dto.Nome;
        potentia.Descricao = dto.Descricao;
        potentia.LinkMusica = dto.LinkMusica;
        potentia.PersonagemId = dto.PersonagemId;
    }

    public static ReadPotentiaDto MapToReadPotentiaDto (this Potentia obj) {
        return new ReadPotentiaDto {
            Id = obj.Id,
            Nome = obj.Nome,
            Descricao = obj.Descricao,
            LinkMusica = obj.LinkMusica,
            NomePersonagem = obj.Personagem!.Nome,
            ListaHabilidades = (ICollection<ReadHabilidadeDto>)obj.Habilidades!.Select(h => h.MapToReadHabilidade()).ToList()
        };
    }
}