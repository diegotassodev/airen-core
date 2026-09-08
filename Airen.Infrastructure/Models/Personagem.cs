using System.ComponentModel.DataAnnotations;

namespace Airen.Infrastructure.Models;

public class Personagem {

    [Key]
    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? NomeOriginal {get;set;}
    public string? Biografia {get;set;}
    public DateTime? DataNascimento {get;set;}
    public bool EstaVivo {get;set;}

    public virtual Potentia? Potentia {get;set;}
    
    public virtual ICollection<Versao>? Versoes {get;set;}
    public virtual ICollection<Afiliacao>? Afiliacoes {get;set;}
    public virtual ICollection<Parentesco>? Parentes {get;set;}
}