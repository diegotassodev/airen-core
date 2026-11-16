using Airen.Infrastructure.Models;

namespace Airen.Contracts.DTOs;

public record ReadVolumeDto {

    public int Id {get;set;}
    public int Numero {get;set;}
    public string? Nome {get;set;}
    public DateTime? DataLancamento {get;set;}
    public string? MensagemVolume {get;set;}
    public string? UrlCapa {get;set;}
    public virtual ICollection<ReadCapituloDto>? Capitulos {get;set;}
}