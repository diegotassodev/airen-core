using System.ComponentModel.DataAnnotations;
using Airen.Infrastructure.Models;

namespace Airen.Contracts.DTOs;

public record UpdateVolumeDto {

    [Required (ErrorMessage = "O número do volume não foi definido")]
    [Range(1, int.MaxValue, ErrorMessage = "O número do volume deve ser maior que zero.")]
    public int Numero {get;set;}

    [Required (ErrorMessage = "O nome do volume não foi definido")]
    public string? Nome {get;set;}

    [Required (ErrorMessage = "A data de lançamento do volume não foi definida")]
    public DateTime? DataLancamento {get;set;}

    public string? MensagemVolume {get;set;}
    
    public string? UrlCapa {get;set;}
}