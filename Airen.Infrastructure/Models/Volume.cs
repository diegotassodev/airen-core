using System.ComponentModel.DataAnnotations;

namespace Airen.Infrastructure.Models;

public class Volume {

    [Key]
    public int Id {get;set;}
    public int Numero {get;set;}
    public string? Nome {get;set;}
    public DateTime? DataLancamento {get;set;}
    public string? MensagemVolume {get;set;}
    public string? UrlCapa {get;set;}

    public virtual ICollection<Capitulo>? Capitulos {get;set;}

}