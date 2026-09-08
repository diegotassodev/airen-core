using System.ComponentModel.DataAnnotations;
using Airen.Infrastructure.Models;

namespace Airen.Api.DTOs;

public class UpdateHabilidadeDto {

    [Required (ErrorMessage = "O nome não foi definido")]
    public string? Nome {get;set;}

    public string? Descricao {get;set;}

    [Required]
    public int IdPotentia {get;set;}
}