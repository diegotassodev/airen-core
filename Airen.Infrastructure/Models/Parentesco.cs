using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airen.Infrastructure.Models;

public class Parentesco {

    [Key]
    public int Id {get;set;}
    public string? TipoParentesco {get;set;}

    [ForeignKey(nameof(Personagem))]
    public int IdPersonagem {get;set;}
    public virtual Personagem? Personagem {get;set;}

    [ForeignKey(nameof(Parente))]
    public int IdParente {get;set;}
    public virtual Personagem? Parente {get;set;}


}