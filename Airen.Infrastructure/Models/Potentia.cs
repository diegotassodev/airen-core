using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airen.Infrastructure.Models;

public class Potentia {

    [Key]
    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? Descricao {get;set;}
    public string? LinkMusica {get;set;} 

    [ForeignKey(nameof(Personagem))]
    public int PersonagemId {get;set;}
    public virtual Personagem? Personagem {get;set;}

    public virtual ICollection<Habilidade>? Habilidades {get;set;}
}