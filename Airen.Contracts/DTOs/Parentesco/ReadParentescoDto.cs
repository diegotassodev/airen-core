using System.ComponentModel.DataAnnotations;
using Airen.Infrastructure.Models;

namespace Airen.Contracts.DTOs;

public class ReadParentescoDto {

    public int Id {get;set;}
    public virtual string? NomePersonagem {get;set;}
    public virtual string? NomeParente {get;set;}
    public string? TipoParentesco {get;set;}
}