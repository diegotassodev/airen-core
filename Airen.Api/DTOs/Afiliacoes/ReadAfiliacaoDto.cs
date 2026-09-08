using System.ComponentModel.DataAnnotations;
using Airen.Infrastructure.Models;

namespace Airen.Api.DTOs;

public class ReadAfiliacaoDto {

    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? Descricao {get;set;}

}