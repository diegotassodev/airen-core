using System.ComponentModel.DataAnnotations;
using Airen.Api.Models;

namespace Airen.Api.Data.DTOs;

public class CreateHabilidadeDto {

    [Required (ErrorMessage = "O nome não foi definido")]
    public string? Nome {get;set;}

    public string? Descricao {get;set;}

    [Required]
    public int IdPotentia {get;set;}
}