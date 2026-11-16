using System.ComponentModel.DataAnnotations;
using Airen.Infrastructure.Models;

namespace Airen.Contracts.DTOs;

public record ReadPersonagemDto {
    
    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? NomeOriginal {get;set;}
    public string? Biografia {get;set;}
    public DateTime? DataNascimento {get;set;}
    public bool EstaVivo {get;set;}

    public virtual ReadPotentiaDto? Potentia {get;set;}
    public virtual ICollection<ReadVersaoDto>? Versoes {get;set;}
    public virtual ICollection<ReadAfiliacaoDto>? Afiliacoes {get;set;}
    public virtual ICollection<ReadParentescoDto>? Parentes {get;set;}
}