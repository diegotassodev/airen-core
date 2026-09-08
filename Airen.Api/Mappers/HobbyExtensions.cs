using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Api.Mappers;

public static class HobbyExtensions {

    public static Hobby MapToHobby (this CreateHobbyDto dto) {
        return new Hobby {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            IdVersao = dto.IdVersao
        };
    }

    public static void MapToHobby(this UpdateHobbyDto dto, Hobby hobby) {
        hobby.Nome = dto.Nome;
        hobby.Descricao = dto.Descricao;
        hobby.IdVersao = dto.IdVersao;
    }

    public static ReadHobbyDto MapToReadHobby (this Hobby obj) {
        return new ReadHobbyDto {
            Id = obj.Id,
            Nome = obj.Nome,
            Descricao = obj.Descricao,
            NomePersonagem = obj.Versao!.Personagem!.Nome
        };
    }
}