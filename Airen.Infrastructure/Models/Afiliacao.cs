using System.ComponentModel.DataAnnotations;

namespace Airen.Infrastructure.Models;

public class Afiliacao {

    [Key]
    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? Descricao {get;set;}
}