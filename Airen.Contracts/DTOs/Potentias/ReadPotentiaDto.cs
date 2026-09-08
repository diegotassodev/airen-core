using System.ComponentModel.DataAnnotations;
using Airen.Infrastructure.Models;

namespace Airen.Contracts.DTOs;

public class ReadPotentiaDto {

    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? Descricao {get;set;}
    public string? LinkMusica {get;set;}
    public string? NomePersonagem {get;set;}
    public virtual ICollection<ReadHabilidadeDto>? ListaHabilidades {get;set;}

}