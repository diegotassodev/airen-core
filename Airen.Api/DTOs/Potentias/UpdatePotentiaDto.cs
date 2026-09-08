using System.ComponentModel.DataAnnotations;
using Airen.Infrastructure.Models;

namespace Airen.Api.DTOs;

public class UpdatePotentiaDto {

    [Required (ErrorMessage = "O nome não foi definido")]
    public string? Nome {get;set;}

    [Required (ErrorMessage = "A descrição não foi definida")]
    public string? Descricao {get;set;}

    public string? LinkMusica {get;set;}

    [Required (ErrorMessage = "O ID do personagem não foi definido")]
    public int PersonagemId {get;set;}
}