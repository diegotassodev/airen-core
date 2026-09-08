using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airen.Infrastructure.Models;

public class Tecnica {

    [Key]
    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? Descricao {get;set;}
    public string? Categoria {get;set;}

    [ForeignKey(nameof(Versao))]
    public int IdVersao {get;set;}
    public virtual Versao? Versao {get;set;}
}