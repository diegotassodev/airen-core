using System.ComponentModel.DataAnnotations;
using Airen.Api.Models;

namespace Airen.Api.Data.DTOs;

public class ReadAfiliacaoDto {

    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? Descricao {get;set;}

}