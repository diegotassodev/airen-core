using Airen.Api.Controllers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Airen.Tests.Controllers;

public class VersaoControllerTests
{
    private readonly Mock<IRepository<Versao, ReadVersaoDto, UpdateVersaoDto>> mock = new();
    private readonly VersaoController controller;

    public VersaoControllerTests()
    {
        controller = new VersaoController(mock.Object);
    }

    [Fact]
    public void GetVersoesDeveRetornarListaDeVersoes()
    {
        // Arrange
        var versao = new ReadVersaoDto { 
            Id = 1, 
            Nome = "Atual", 
            NomePersonagem = "Alex", 
            Genero = "Fantasia" 
        };
        var resultadoEsperado = new List<ReadVersaoDto> { versao };
        mock.Setup(repository => repository.ReadAll()).Returns(resultadoEsperado);

        // Act
        var resultado = controller.VerVersoes();

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var versoes = Assert.IsAssignableFrom<IEnumerable<ReadVersaoDto>>(resultadoOk.Value);
        Assert.Equal(resultadoEsperado, versoes);
        mock.Verify(repository => repository.ReadAll(), Times.Once);
    }

    [Fact]
    public void GetVersaoPorIdDeveRetornarVersaoCorreta()
    {
        // Arrange
        var resultadoEsperado = new ReadVersaoDto { 
            Id = 1, 
            Nome = "Atual", 
            NomePersonagem = "Alex", 
            Genero = "Fantasia" 
        };
        mock.Setup(repository => repository.ReadById(1)).Returns(resultadoEsperado);

        // Act
        var resultado = controller.BuscaVersaoPorId(1);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(resultadoEsperado, Assert.IsType<ReadVersaoDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadById(1), Times.Once);
    }

    [Fact]
    public void PostVersaoDeveCriarObjetoCorretamente()
    {
        // Arrange
        var personagem = new Personagem { 
            Id = 2, 
            Nome = "Alex", 
            Versoes = new List<Versao>() 
        };
        var createDto = new CreateVersaoDto
        {
            Nome = "Atual",
            Genero = "Fantasia",
            UrlImagem = "imagem",
            IdadeInicial = 10,
            IdadeFinal = 20,
            Descricao = "Descrição",
            Personalidade = "Calma",
            IdPersonagem = personagem.Id
        };
        mock.Setup(repository => repository.Create(It.IsAny<Versao>()))
            .Callback<Versao>(versao =>
            {
                versao.Personagem = personagem;
                personagem.Versoes.Add(versao);
            });

        // Act
        var resultado = controller.AdicionaVersao(createDto);

        // Assert
        var resultadoCreated = Assert.IsType<CreatedAtActionResult>(resultado);
        var versao = Assert.IsType<ReadVersaoDto>(resultadoCreated.Value);
        Assert.Single(personagem.Versoes);
        Assert.Equal(personagem.Nome, versao.NomePersonagem);
        Assert.Equal(0, versao.Id);
        Assert.Equal(createDto.Nome, versao.Nome);
        Assert.Equal(createDto.Genero, versao.Genero);
        Assert.Equal(createDto.UrlImagem, versao.UrlImagem);
        Assert.Equal(createDto.IdadeInicial, versao.IdadeInicial);
        Assert.Equal(createDto.IdadeFinal, versao.IdadeFinal);
        Assert.Equal(createDto.Descricao, versao.Descricao);
        Assert.Equal(createDto.Personalidade, versao.Personalidade);
        mock.Verify(repository => repository.Create(It.IsAny<Versao>()), Times.Once);
    }

    [Fact]
    public void PutVersaoDeveEditarObjetoCorretamente()
    {
        var versao = new Versao { 
            Id = 1, 
            Nome = "Atual",
            IdPersonagem = 2 
        };
        var updateDto = new UpdateVersaoDto
        {
            Nome = "Futura",
            Genero = "Drama",
            UrlImagem = "nova-imagem",
            IdadeInicial = 15,
            IdadeFinal = 25,
            Descricao = "Nova descrição",
            Personalidade = "Forte",
            IdPersonagem = 3
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(versao);

        var resultado = controller.AtualizaVersao(1, updateDto);

        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(updateDto, Assert.IsType<UpdateVersaoDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Update(updateDto, versao), Times.Once);
    }

    [Fact]
    public void DeleteVersaoDeveExcluirObjetoCorretamente()
    {
        var versao = new Versao { 
            Id = 1,
            Nome = "Atual" 
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(versao);

        var resultado = controller.DeletaVersao(1);

        Assert.IsType<NoContentResult>(resultado);
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Delete(versao), Times.Once);
    }
}
