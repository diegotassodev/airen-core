using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airen.Infrastructure.Models;

public class Capitulo {

    [Key]
    public int Id {get;set;}
    public int Numero {get;set;}
    public string? Titulo {get;set;}
    public string? Resumo {get;set;}
    public int NumeroPaginas {get;set;}

    [ForeignKey(nameof(Volume))]
    public int? IdVolume {get;set;}
    public virtual Volume? Volume {get;set;}

}