using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airen.Infrastructure.Models;

public class Habilidade {

    [Key]
    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? Descricao {get;set;}

    [ForeignKey(nameof(Potentia))]
    public int IdPotentia {get;set;}
    public virtual Potentia? Potentia {get;set;}
}