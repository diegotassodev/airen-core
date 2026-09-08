using System.ComponentModel.DataAnnotations;
using Airen.Infrastructure.Models;

namespace Airen.Api.DTOs;

public class CreatePersonagemDto {

    [Required (ErrorMessage = "O nome não foi definido")]
    public string? Nome {get;set;}

    public string? NomeOriginal {get;set;}

    [Required (ErrorMessage = "A biografia não foi definida")]
    public string? Biografia {get;set;}

    [Required (ErrorMessage = "A data de nascimento não foi definida")]
    public DateTime? DataNascimento {get;set;}

    public bool EstaVivo {get;set;}

}