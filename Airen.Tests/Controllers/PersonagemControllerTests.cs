using Airen.Api.Controllers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Airen.Tests.Controllers;

public class PersonagemControllerTests
{
    private readonly Mock<IRepository<Personagem, ReadPersonagemDto, UpdatePersonagemDto>> mock = new();
    private readonly PersonagemController controller;

    public PersonagemControllerTests()
    {
        controller = new PersonagemController(mock.Object);
    }

    [Fact]
    public void GetPersonagensDeveRetornarListaDePersonagens()
    {
        // Arrange
        var personagem = new ReadPersonagemDto { 
            Id = 1, 
            Nome = "Alex", 
            NomeOriginal = "Alexander", 
            Biografia = "Biografia", 
            EstaVivo = true 
        };
        var resultadoEsperado = new List<ReadPersonagemDto> { personagem };
        mock.Setup(repository => repository.ReadAll()).Returns(resultadoEsperado);

        // Act
        var resultado = controller.VerPersonagens();

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var personagens = Assert.IsAssignableFrom<IEnumerable<ReadPersonagemDto>>(resultadoOk.Value);
        Assert.Equal(resultadoEsperado, personagens);
        mock.Verify(repository => repository.ReadAll(), Times.Once);
    }

    [Fact]
    public void GetPersonagemPorIdDeveRetornarPersonagemCorreto()
    {
        // Arrange
        var resultadoEsperado = new ReadPersonagemDto 
        { 
            Id = 1, 
            Nome = "Alex", 
            NomeOriginal = "Alexander", 
            Biografia = "Biografia", 
            EstaVivo = true 
        };
        mock.Setup(repository => repository.ReadById(1)).Returns(resultadoEsperado);

        // Act
        var resultado = controller.BuscaPersonagemPorId(1);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(resultadoEsperado, Assert.IsType<ReadPersonagemDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadById(1), Times.Once);
    }

    [Fact]
    public void PostPersonagemDeveCriarObjetoCorretamente()
    {
        // Arrange
        var dataNascimento = new DateTime(2000, 1, 2);
        var createDto = new CreatePersonagemDto
        {
            Nome = "Alex",
            NomeOriginal = "Alexander",
            Biografia = "Biografia",
            DataNascimento = dataNascimento,
            EstaVivo = true
        };
        mock.Setup(repository => repository.Create(It.IsAny<Personagem>()));

        // Act
        var resultado = controller.AdicionaPersonagem(createDto);

        // Assert
        var resultadoCreated = Assert.IsType<CreatedAtActionResult>(resultado);
        var personagem = Assert.IsType<ReadPersonagemDto>(resultadoCreated.Value);
        Assert.Equal(0, personagem.Id);
        Assert.Equal(createDto.Nome, personagem.Nome);
        Assert.Equal(createDto.NomeOriginal, personagem.NomeOriginal);
        Assert.Equal(createDto.Biografia, personagem.Biografia);
        Assert.Equal(createDto.DataNascimento, personagem.DataNascimento);
        Assert.Equal(createDto.EstaVivo, personagem.EstaVivo);
        mock.Verify(repository => repository.Create(It.IsAny<Personagem>()), Times.Once);
    }

    [Fact]
    public void PutPersonagemDeveEditarObjetoCorretamente()
    {
        // Arrange
        var personagem = new Personagem 
        { 
            Id = 1, 
            Nome = "Alex" 
        };
        var updateDto = new UpdatePersonagemDto
        {
            Nome = "Sam",
            NomeOriginal = "Samuel",
            Biografia = "Nova biografia",
            DataNascimento = new DateTime(2001, 2, 3),
            EstaVivo = true
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(personagem);

        // Act
        var resultado = controller.AtualizaPersonagem(1, updateDto);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(updateDto, Assert.IsType<UpdatePersonagemDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Update(updateDto, personagem), Times.Once);
    }

    [Fact]
    public void DeletePersonagemDeveExcluirObjetoCorretamente()
    {
        // Arrange
        var personagem = new Personagem { 
            Id = 1, 
            Nome = "Alex" 
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(personagem);

        // Act
        var resultado = controller.DeletaPersonagem(1);

        // Assert
        Assert.IsType<NoContentResult>(resultado);
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Delete(personagem), Times.Once);
    }
}
