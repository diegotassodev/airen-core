using System.ComponentModel.DataAnnotations;
using Airen.Infrastructure.Models;

namespace Airen.Api.DTOs;

public class CreateParentescoDto {

    [Required (ErrorMessage = "O id do personagem não foi definido")]
    public int IdPersonagem {get;set;}

    [Required (ErrorMessage = "O id do parente não foi definido")]
    public int IdParente {get;set;}

    public string? TipoParentesco {get;set;}
}