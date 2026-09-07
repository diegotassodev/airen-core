using System.ComponentModel.DataAnnotations;
using Airen.Api.Models;

namespace Airen.Api.Data.DTOs;

public class CreateHobbyDto {

    [Required (ErrorMessage = "O nome não foi definido")]
    public string? Nome {get;set;}

    [Required (ErrorMessage = "A descrição não foi definida")]
    public string? Descricao {get;set;}

    [Required (ErrorMessage = "A versão não foi definida")]
    public int IdVersao {get;set;}
}