using System.ComponentModel.DataAnnotations;
using Airen.Api.Models;

namespace Airen.Api.Data.DTOs;

public class ReadPotentiaDto {

    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? Descricao {get;set;}
    public string? LinkMusica {get;set;}
    public string? NomePersonagem {get;set;}
    public virtual ICollection<ReadHabilidadeDto>? ListaHabilidades {get;set;}

}