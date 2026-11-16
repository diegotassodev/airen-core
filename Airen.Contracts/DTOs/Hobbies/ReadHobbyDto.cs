namespace Airen.Contracts.DTOs;

public record ReadHobbyDto {

    public int Id {get;set;}
    public string? Nome {get;set;}
    public string? Descricao {get;set;}
    public string? NomePersonagem {get;set;}
}