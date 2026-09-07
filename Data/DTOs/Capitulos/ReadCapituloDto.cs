using System.ComponentModel.DataAnnotations;
using Airen.Api.Models;

namespace Airen.Api.Data.DTOs;

public class ReadCapituloDto {

    public int Id {get;set;}
    public int Numero {get;set;}
    public string? Titulo {get;set;}
    public string? Resumo {get;set;}
    public int NumeroPaginas {get;set;}
    public int NumeroVolume {get;set;}
}