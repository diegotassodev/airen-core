using AirenApi.Controllers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Airen.Tests.Unit.Controllers;

public class PotentiaControllerTests
{
    private readonly Mock<IRepository<Potentia, ReadPotentiaDto, UpdatePotentiaDto>> mock = new();
    private readonly PotentiaController controller;

    public PotentiaControllerTests()
    {
        controller = new PotentiaController(mock.Object);
    }

    [Fact]
    public void GetPotentiasDeveRetornarListaDePotentias()
    {
        // Arrange
        var potentia = new ReadPotentiaDto 
        { 
            Id = 1, 
            Nome = "Luz", 
            Descricao = "Energia", 
            LinkMusica = "musica", 
            NomePersonagem = "Alex" 
        };
        var resultadoEsperado = new List<ReadPotentiaDto> { potentia };
        mock.Setup(repository => repository.ReadAll()).Returns(resultadoEsperado);

        // Act
        var resultado = controller.VerPotentias();

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var potentias = Assert.IsAssignableFrom<IEnumerable<ReadPotentiaDto>>(resultadoOk.Value);
        Assert.Equal(resultadoEsperado, potentias);
        mock.Verify(repository => repository.ReadAll(), Times.Once);
    }

    [Fact]
    public void GetPotentiaPorIdDeveRetornarPotentiaCorreta()
    {
        // Arrange
        var resultadoEsperado = new ReadPotentiaDto 
        { 
            Id = 1, 
            Nome = "Luz", 
            Descricao = "Energia", 
            LinkMusica = "musica", 
            NomePersonagem = "Alex" 
        };
        mock.Setup(repository => repository.ReadById(1)).Returns(resultadoEsperado);

        // Act
        var resultado = controller.BuscaPotentiaPorId(1);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(resultadoEsperado, Assert.IsType<ReadPotentiaDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadById(1), Times.Once);
    }

    [Fact]
    public void PostPotentiaDeveCriarObjetoCorretamente()
    {
        // Arrange
        var personagem = new Personagem 
        { 
            Id = 2, 
            Nome = "Alex" 
        };
        var createDto = new CreatePotentiaDto
        {
            Nome = "Luz",
            Descricao = "Energia",
            LinkMusica = "musica",
            PersonagemId = personagem.Id
        };
        mock.Setup(repository => repository.Create(It.IsAny<Potentia>()))
            .Callback<Potentia>(potentia =>
            {
                potentia.Personagem = personagem;
                personagem.Potentia = potentia;
            });

        // Act
        var resultado = controller.AdicionaPotentia(createDto);

        // Assert
        var resultadoCreated = Assert.IsType<CreatedAtActionResult>(resultado);
        var potentiaDto = Assert.IsType<ReadPotentiaDto>(resultadoCreated.Value);
        var potentia = Assert.IsType<Potentia>(personagem.Potentia);
        Assert.Same(personagem, potentia.Personagem);
        Assert.Equal(0, potentiaDto.Id);
        Assert.Equal(createDto.Nome, potentiaDto.Nome);
        Assert.Equal(createDto.Descricao, potentiaDto.Descricao);
        Assert.Equal(createDto.LinkMusica, potentiaDto.LinkMusica);
        Assert.Equal(personagem.Nome, potentiaDto.NomePersonagem);
        mock.Verify(repository => repository.Create(It.IsAny<Potentia>()), Times.Once);
    }

    [Fact]
    public void PutPotentiaDeveEditarObjetoCorretamente()
    {
        // Arrange
        var potentia = new Potentia 
        { 
            Id = 1, 
            Nome = "Luz", 
            PersonagemId = 2 
        };
        var updateDto = new UpdatePotentiaDto { 
            Nome = "Sombra",
            Descricao = "Nova energia",
            LinkMusica = "nova",
            PersonagemId = 3 
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(potentia);

        // Act
        var resultado = controller.AtualizaPotentia(1, updateDto);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(updateDto, Assert.IsType<UpdatePotentiaDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Update(updateDto, potentia), Times.Once);
    }

    [Fact]
    public void DeletePotentiaDeveExcluirObjetoCorretamente()
    {
        
        // Arrange
        var potentia = new Potentia { 
            Id = 1,
            Nome = "Luz"
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(potentia);

        // Act
        var resultado = controller.DeletaPotentia(1);

        // Assert
        Assert.IsType<NoContentResult>(resultado);
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Delete(potentia), Times.Once);
    }
}
