using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Api.Mappers;

public static class VolumeExtensions {

    public static Volume MapToVolume (this CreateVolumeDto dto) {
        return new Volume {
            Numero = dto.Numero,
            Nome = dto.Nome,
            DataLancamento = dto.DataLancamento,
            MensagemVolume = dto.MensagemVolume,
            UrlCapa = dto.UrlCapa
        };
    }

    public static void MapToVolume(this UpdateVolumeDto dto, Volume volume) {
        volume.Numero = dto.Numero;
        volume.Nome = dto.Nome;
        volume.DataLancamento = dto.DataLancamento;
        volume.MensagemVolume = dto.MensagemVolume;
        volume.UrlCapa = dto.UrlCapa;
    }

    public static ReadVolumeDto MapToReadVolume (this Volume obj) {
        return new ReadVolumeDto {
            Id = obj.Id,
            Numero = obj.Numero,
            Nome = obj.Nome,
            DataLancamento = obj.DataLancamento,
            MensagemVolume = obj.MensagemVolume,
            UrlCapa = obj.UrlCapa,
            Capitulos = (ICollection<ReadCapituloDto>)obj.Capitulos!.Select(c => c.MapToReadCapitulo()).ToList()
        };
    }
}