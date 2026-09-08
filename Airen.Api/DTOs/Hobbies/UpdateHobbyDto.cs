using System.ComponentModel.DataAnnotations;
using Airen.Infrastructure.Models;

namespace Airen.Api.DTOs;

public class UpdateHobbyDto {

    [Required (ErrorMessage = "O nome não foi definido")]
    public string? Nome {get;set;}

    [Required (ErrorMessage = "A descrição não foi definida")]
    public string? Descricao {get;set;}

    public int IdVersao {get;set;}
}