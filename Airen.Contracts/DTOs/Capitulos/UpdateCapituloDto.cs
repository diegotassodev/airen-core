using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Airen.Infrastructure.Models;

namespace Airen.Contracts.DTOs;

public record UpdateCapituloDto {

    [Required (ErrorMessage = "O número do capítulo não foi definido")]
    [Range(1, int.MaxValue, ErrorMessage = "O número do capítulo deve ser maior que zero.")]
    public int Numero {get;set;}

    [Required (ErrorMessage = "O título do capítulo não foi definido")]
    public string? Titulo {get;set;}

    [Required (ErrorMessage = "O resumo do capítulo não foi definido.")]
    public string? Resumo {get;set;}

    [Required (ErrorMessage = "O número de páginas do capítulo não foi definido.")]
    [Range(1, int.MaxValue, ErrorMessage = "O número de páginas deve ser maior que zero.")]
    public int NumeroPaginas {get;set;}

    [Required(ErrorMessage = "O volume do capítulo não foi definido.")]
    [Range(1, int.MaxValue, ErrorMessage = "Selecione um volume válido.")]
    public int IdVolume {get;set;}
    public virtual Volume? Volume {get;set;}
    
}