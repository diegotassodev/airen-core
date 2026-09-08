using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airen.Infrastructure.Models;

public class Versao {

    [Key]
    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? Genero {get;set;}
    public string? UrlImagem {get;set;}
    public int IdadeInicial {get;set;}
    public int IdadeFinal {get;set;}
    public string? Descricao {get;set;}
    public string? Personalidade {get;set;}

    [ForeignKey(nameof(Personagem))]
    public int IdPersonagem {get;set;}
    public virtual Personagem? Personagem {get;set;}

    public virtual ICollection<Tecnica>? Tecnicas {get;set;}
    public virtual ICollection<Hobby>? Hobbies {get;set;}
    public virtual ICollection<Volume>? Volumes {get;set;}
    
}