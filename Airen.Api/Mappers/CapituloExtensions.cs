using Airen.Api.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Api.Mappers;

public static class CapituloExtensions {

    public static Capitulo MapToCapitulo(this CreateCapituloDto dto) {
        return new Capitulo {
            Numero = dto.Numero,
            Titulo = dto.Titulo,
            Resumo = dto.Resumo,
            NumeroPaginas = dto.NumeroPaginas,
            IdVolume = dto.IdVolume
        };
    }

    public static void MapToCapitulo(this UpdateCapituloDto dto, Capitulo capitulo) {
        capitulo.Numero = dto.Numero;
        capitulo.Titulo = dto.Titulo;
        capitulo.Resumo = dto.Resumo;
        capitulo.NumeroPaginas = dto.NumeroPaginas;
        capitulo.IdVolume = dto.IdVolume;
    }

    public static ReadCapituloDto MapToReadCapitulo(this Capitulo obj) {
        return new ReadCapituloDto {
            Id = obj.Id,
            Numero = obj.Numero,
            Titulo = obj.Titulo,
            Resumo = obj.Resumo,
            NumeroPaginas = obj.NumeroPaginas,
            NumeroVolume = obj.Volume!.Numero
        };
    }
}