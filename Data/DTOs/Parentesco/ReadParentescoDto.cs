using System.ComponentModel.DataAnnotations;
using Airen.Api.Models;

namespace Airen.Api.Data.DTOs;

public class ReadParentescoDto {

    public int Id {get;set;}
    public virtual string? NomePersonagem {get;set;}
    public virtual string? NomeParente {get;set;}
    public string? TipoParentesco {get;set;}
}