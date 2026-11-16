using Airen.Api.Controllers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Airen.Tests.Controllers;

public class ParentescoControllerTests
{
    private readonly Mock<IRepository<Parentesco, ReadParentescoDto, UpdateParentescoDto>> mock = new();
    private readonly ParentescoController controller;

    public ParentescoControllerTests()
    {
        controller = new ParentescoController(mock.Object);
    }

    [Fact]
    public void GetParentescosDeveRetornarListaDeParentescos()
    {
        // Arrange
        var parentesco = new ReadParentescoDto { 
            Id = 1,
            NomePersonagem = "Alex",
            NomeParente = "Sam",
            TipoParentesco = "Irmão"
        };
        var resultadoEsperado = new List<ReadParentescoDto> { parentesco };
        mock.Setup(repository => repository.ReadAll()).Returns(resultadoEsperado);

        // Act
        var resultado = controller.VerParentescos();

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var parentescos = Assert.IsAssignableFrom<IEnumerable<ReadParentescoDto>>(resultadoOk.Value);
        Assert.Equal(resultadoEsperado, parentescos);
        mock.Verify(repository => repository.ReadAll(), Times.Once);
    }

    [Fact]
    public void GetParentescoPorIdDeveRetornarParentescoCorreto()
    {
        // Arrange
        var resultadoEsperado = new ReadParentescoDto { 
            Id = 1, 
            NomePersonagem = "Alex", 
            NomeParente = "Sam", 
            TipoParentesco = "Irmão" 
        };
        mock.Setup(repository => repository.ReadById(1)).Returns(resultadoEsperado);

        // Act
        var resultado = controller.BuscaParentescoPorId(1);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(resultadoEsperado, Assert.IsType<ReadParentescoDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadById(1), Times.Once);
    }

    [Fact]
    public void PostParentescoDeveCriarObjetoCorretamente()
    {
        // Arrange
        var personagem = new Personagem { 
            Id = 1, 
            Nome = "Alex" 
        };
        var parente = new Personagem { 
            Id = 2, 
            Nome = "Sam" 
        };

        var createDto = new CreateParentescoDto
        {
            IdPersonagem = personagem.Id,
            IdParente = parente.Id,
            TipoParentesco = "Irmão"
        };
        mock.Setup(repository => repository.Create(It.IsAny<Parentesco>()))
            .Callback<Parentesco>(parentesco =>
            {
                parentesco.Personagem = personagem;
                parentesco.Parente = parente;
                personagem.Parentes = new List<Parentesco> { parentesco };
            });

        // Act
        var resultado = controller.AdicionarParentesco(createDto);

        // Assert
        var resultadoCreated = Assert.IsType<CreatedAtActionResult>(resultado);
        var parentesco = Assert.IsType<ReadParentescoDto>(resultadoCreated.Value);
        Assert.Equal(0, parentesco.Id);
        Assert.Equal(personagem.Nome, parentesco.NomePersonagem);
        Assert.Equal(parente.Nome, parentesco.NomeParente);
        Assert.Equal(createDto.TipoParentesco, parentesco.TipoParentesco);
        Assert.Same(personagem, (personagem.Parentes ?? Array.Empty<Parentesco>()).Single().Personagem);
        mock.Verify(repository => repository.Create(It.IsAny<Parentesco>()), Times.Once);
    }

    [Fact]
    public void PutParentescoDeveEditarObjetoCorretamente()
    {
        // Arrange
        var parentesco = new Parentesco 
        { 
            Id = 1, 
            IdPersonagem = 1, 
            IdParente = 2, 
            TipoParentesco = "Irmão" 
        };
        var updateDto = new UpdateParentescoDto { 
            IdPersonagem = 2, 
            IdParente = 3, 
            TipoParentesco = "Amigo" 
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(parentesco);

        // Act
        var resultado = controller.AtualizaParentesco(1, updateDto);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(updateDto, Assert.IsType<UpdateParentescoDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Update(updateDto, parentesco), Times.Once);
    }

    [Fact]
    public void DeleteParentescoDeveExcluirObjetoCorretamente()
    {
        // Arrange
        var parentesco = new Parentesco 
        { 
            Id = 1, 
            TipoParentesco = "Irmão" 
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(parentesco);

        // Act
        var resultado = controller.DeletaParentesco(1);

        // Assert
        Assert.IsType<NoContentResult>(resultado);
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Delete(parentesco), Times.Once);
    }
}
