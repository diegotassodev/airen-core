using System.ComponentModel.DataAnnotations;

namespace Airen.Api.Models;

public class Afiliacao {

    [Key]
    [Required]
    public int Id {get;set;}

    [Required (ErrorMessage = "O nome não foi definido")]
    public string? Nome {get;set;}

    public string? Descricao {get;set;}
}