using System.ComponentModel.DataAnnotations;
using Airen.Api.Models;

namespace Airen.Api.Data.DTOs;

public class ReadTecnicaDto {

    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? Descricao {get;set;}
    public string? Categoria {get;set;}
    public string? NomePersonagem {get;set;}
}