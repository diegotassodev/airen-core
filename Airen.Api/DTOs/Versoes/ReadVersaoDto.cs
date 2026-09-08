using System.ComponentModel.DataAnnotations;
using Airen.Infrastructure.Models;

namespace Airen.Api.DTOs;

public class ReadVersaoDto {

    public int Id {get;set;}
    public string? NomePersonagem {get;set;}
    public string? Nome {get;set;}
    public string? Personalidade {get;set;}
    public string? Descricao {get;set;}
    public string? Genero {get;set;}
    public string? UrlImagem {get;set;}
    public int IdadeInicial {get;set;}
    public int IdadeFinal {get;set;}
    public ICollection<ReadTecnicaDto>? Tecnicas {get;set;}
    public ICollection<ReadHobbyDto>? Hobbies {get;set;}
    public ICollection<ReadVolumeDto>? Volumes {get;set;}

}