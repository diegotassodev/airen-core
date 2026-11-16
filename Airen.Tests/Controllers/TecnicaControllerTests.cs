using Airen.Api.Controllers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Airen.Tests.Controllers;

public class TecnicaControllerTests
{
    private readonly Mock<IRepository<Tecnica, ReadTecnicaDto, UpdateTecnicaDto>> mock = new();
    private readonly TecnicaController controller;

    public TecnicaControllerTests()
    {
        controller = new TecnicaController(mock.Object);
    }

    [Fact]
    public void GetTecnicasDeveRetornarListaDeTecnicas()
    {
        // Arrange
        var tecnica = new ReadTecnicaDto 
        { 
            Id = 1,
            Nome = "Corte",
            Descricao = "Ataque", 
            Categoria = "Ofensiva", 
            NomePersonagem = "Alex" 
        };
        var resultadoEsperado = new List<ReadTecnicaDto> { tecnica };
        mock.Setup(repository => repository.ReadAll()).Returns(resultadoEsperado);

        // Act
        var resultado = controller.VerTecnicas();

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var tecnicas = Assert.IsAssignableFrom<IEnumerable<ReadTecnicaDto>>(resultadoOk.Value);
        Assert.Equal(resultadoEsperado, tecnicas);
        mock.Verify(repository => repository.ReadAll(), Times.Once);
    }

    [Fact]
    public void GetTecnicaPorIdDeveRetornarTecnicaCorreta()
    {
        // Arrange
        var resultadoEsperado = new ReadTecnicaDto { 
            Id = 1,
            Nome = "Corte", 
            Descricao = "Ataque", 
            Categoria = "Ofensiva", 
            NomePersonagem = "Alex" 
        };
        mock.Setup(repository => repository.ReadById(1)).Returns(resultadoEsperado);

        // Act
        var resultado = controller.BuscaTecnicaPorId(1);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(resultadoEsperado, Assert.IsType<ReadTecnicaDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadById(1), Times.Once);
    }

    [Fact]
    public void PostTecnicaDeveCriarObjetoCorretamente()
    {
        // Arrange
        var createDto = new CreateTecnicaDto { 
            Nome = "Corte", 
            Descricao = "Ataque", 
            Categoria = "Ofensiva", 
            IdVersao = 2 
        };
        var personagem = new Personagem { 
            Id = 1, 
            Nome = "Alex" 
        };
        var versao = new Versao
        {
            Id = createDto.IdVersao,
            Personagem = personagem,
            Tecnicas = new List<Tecnica>()
        };
        mock.Setup(repository => repository.Create(It.IsAny<Tecnica>()))
            .Callback<Tecnica>(tecnica =>
            {
                tecnica.Versao = versao;
                versao.Tecnicas.Add(tecnica);
            });

        // Act
        var resultado = controller.AdicionaTecnica(createDto);

        // Assert
        var resultadoCreated = Assert.IsType<CreatedAtActionResult>(resultado);
        var tecnica = Assert.IsType<ReadTecnicaDto>(resultadoCreated.Value);
        Assert.Equal(0, tecnica.Id);
        Assert.Equal(createDto.Nome, tecnica.Nome);
        Assert.Equal(createDto.Descricao, tecnica.Descricao);
        Assert.Equal(createDto.Categoria, tecnica.Categoria);
        Assert.Same(versao, versao.Tecnicas.Single().Versao);
        Assert.Equal(personagem.Nome, tecnica.NomePersonagem);
        mock.Verify(repository => repository.Create(It.IsAny<Tecnica>()), Times.Once);
    }

    [Fact]
    public void PutTecnicaDeveEditarObjetoCorretamente()
    {   
        // Arrange
        var tecnica = new Tecnica { 
            Id = 1, 
            Nome = "Corte", 
            IdVersao = 2 
        };
        var updateDto = new UpdateTecnicaDto { 
            Nome = "Defesa", 
            Descricao = "Bloqueio", 
            Categoria = "Defensiva", 
            IdVersao = 3 
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(tecnica);

        var resultado = controller.AtualizaTecnica(1, updateDto);

        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(updateDto, Assert.IsType<UpdateTecnicaDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Update(updateDto, tecnica), Times.Once);
    }

    [Fact]
    public void DeleteTecnicaDeveExcluirObjetoCorretamente()
    {
        // Arrange
        var tecnica = new Tecnica { 
            Id = 1, 
            Nome = "Corte" 
        };
        
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(tecnica);

        // Act
        var resultado = controller.DeletaTecnica(1);

        // Assert
        Assert.IsType<NoContentResult>(resultado);
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Delete(tecnica), Times.Once);
    }
}
