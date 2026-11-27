using System.ComponentModel.DataAnnotations;
using Airen.Api.Mappers;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;

namespace Airen.Tests.Unit.Mappers;

public class PersonagemExtensionsTests
{
    [Fact]
    public void UpdatePersonagemDtoAceitaNomeOriginalOpcional()
    {
        var dto = new UpdatePersonagemDto
        {
            Nome = "Nome",
            Biografia = "Biografia",
            DataNascimento = new DateTime(2000, 1, 2),
            EstaVivo = true
        };
        var resultados = new List<ValidationResult>();

        var valido = Validator.TryValidateObject(
            dto,
            new ValidationContext(dto),
            resultados,
            validateAllProperties: true);

        Assert.True(valido);
    }

    [Fact]
    public void MapToPersonagemAtualizaTodosOsCampos()
    {
        var dataNascimento = new DateTime(2001, 2, 3);
        var personagem = new Personagem
        {
            Id = 1,
            Nome = "Nome antigo",
            NomeOriginal = "Nome original antigo",
            Biografia = "Biografia antiga",
            DataNascimento = new DateTime(1990, 1, 1),
            EstaVivo = false
        };
        var dto = new UpdatePersonagemDto
        {
            Nome = "Nome atualizado",
            NomeOriginal = null,
            Biografia = "Biografia atualizada",
            DataNascimento = dataNascimento,
            EstaVivo = true
        };

        var resultado = dto.MapToPersonagem(personagem);

        Assert.Same(personagem, resultado);
        Assert.Equal("Nome atualizado", personagem.Nome);
        Assert.Null(personagem.NomeOriginal);
        Assert.Equal("Biografia atualizada", personagem.Biografia);
        Assert.Equal(dataNascimento, personagem.DataNascimento);
        Assert.True(personagem.EstaVivo);
    }
}
