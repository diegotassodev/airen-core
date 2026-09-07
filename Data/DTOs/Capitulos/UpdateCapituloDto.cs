using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Airen.Api.Models;

namespace Airen.Api.Data.DTOs;

public class UpdateCapituloDto {

    [Required (ErrorMessage = "O número do capítulo não foi definido")]
    public int Numero {get;set;}

    [Required (ErrorMessage = "O título do capítulo não foi definido")]
    public string? Titulo {get;set;}

    [Required (ErrorMessage = "O resumo do capítulo não foi definido.")]
    public string? Resumo {get;set;}

    [Required (ErrorMessage = "O número de páginas do capítulo não foi definido.")]
    public int NumeroPaginas {get;set;}

    [Required(ErrorMessage = "O volume do capítulo não foi definido.")]
    public int IdVolume {get;set;}
    public virtual Volume? Volume {get;set;}
    
}