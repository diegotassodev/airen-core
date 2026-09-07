using Airen.Api.Data.DTOs;
using Airen.Api.Models;

namespace Airen.Api.Profiles;

public static class AfiliacaoExtensions {

    public static Afiliacao MapToAfiliacao(this CreateAfiliacaoDto dto) {
        return new Afiliacao {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };
    }

    public static void MapToAfiliacao(this UpdateAfiliacaoDto dto, Afiliacao afiliacao) {
        afiliacao.Nome = dto.Nome;
        afiliacao.Descricao = dto.Descricao;
    }

    public static ReadAfiliacaoDto MapToReadAfiliacao (this Afiliacao obj) {
        return new ReadAfiliacaoDto {
            Id = obj.Id,
            Nome = obj.Nome,
            Descricao = obj.Descricao
        };
    }
}