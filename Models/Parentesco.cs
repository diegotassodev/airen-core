using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airen.Api.Models;

public class Parentesco {

    [Key]
    [Required]
    public int Id {get;set;}

    [ForeignKey(nameof(Personagem))]
    [Required (ErrorMessage = "O id do personagem não foi definido")]
    public int IdPersonagem {get;set;}
    public virtual Personagem? Personagem {get;set;}

    [ForeignKey(nameof(Parente))]
    [Required (ErrorMessage = "O id do parente não foi definido")]
    public int IdParente {get;set;}
    public virtual Personagem? Parente {get;set;}

    public string? TipoParentesco {get;set;}

}