using System.ComponentModel.DataAnnotations;
using Airen.Infrastructure.Models;

namespace Airen.Contracts.DTOs;

public record ReadParentescoDto {

    public int Id {get;set;}
    public int IdPersonagem {get;set;}
    public int IdParente {get;set;}
    public virtual string? NomePersonagem {get;set;}
    public virtual string? NomeParente {get;set;}
    public string? TipoParentesco {get;set;}
}