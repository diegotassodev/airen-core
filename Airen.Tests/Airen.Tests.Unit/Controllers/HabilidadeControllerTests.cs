using Airen.Api.Controllers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Airen.Tests.Unit.Controllers;

public class HabilidadeControllerTests
{
    private readonly Mock<IRepository<Habilidade, ReadHabilidadeDto, UpdateHabilidadeDto>> mock = new();
    private readonly HabilidadeController controller;

    public HabilidadeControllerTests()
    {
        controller = new HabilidadeController(mock.Object);
    }

    [Fact]
    public void GetHabilidadesDeveRetornarListaDeHabilidades()
    {
        // Arrange
        var habilidade = new ReadHabilidadeDto 
        { 
            Id = 1, 
            Nome = "Foco",
            Descricao = "Concentração", 
            NomePotentia = "Luz" 
        };

        var resultadoEsperado = new List<ReadHabilidadeDto> { habilidade };
        mock.Setup(repository => repository.ReadAll()).Returns(resultadoEsperado);

        // Act
        var resultado = controller.VerHabilidades();

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var habilidades = Assert.IsAssignableFrom<IEnumerable<ReadHabilidadeDto>>(resultadoOk.Value);
        Assert.Equal(resultadoEsperado, habilidades);
        mock.Verify(repository => repository.ReadAll(), Times.Once);
    }

    [Fact]
    public void GetHabilidadePorIdDeveRetornarHabilidadeCorreta()
    {
        // Arrange
        var resultadoEsperado = new ReadHabilidadeDto 
        { 
            Id = 1,
            Nome = "Foco", 
            Descricao = "Concentração", 
            NomePotentia = "Luz" 
        };

        mock.Setup(repository => repository.ReadById(1)).Returns(resultadoEsperado);

        // Act
        var resultado = controller.BuscaHabilidadePorId(1);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(resultadoEsperado, Assert.IsType<ReadHabilidadeDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadById(1), Times.Once);
    }

    [Fact]
    public void PostHabilidadeDeveCriarObjetoCorretamente()
    {
        // Arrange
        var createDto = new CreateHabilidadeDto { 
            Nome = "Foco",
            Descricao = "Concentração",
            IdPotentia = 2 
        };
        var potentia = new Potentia
        {
            Id = createDto.IdPotentia,
            Nome = "Luz",
            Habilidades = new List<Habilidade>()
        };
        mock.Setup(repository => repository.Create(It.IsAny<Habilidade>()))
            .Callback<Habilidade>(habilidade =>
            {
                habilidade.Potentia = potentia;
                potentia.Habilidades.Add(habilidade);
            });

        // Act
        var resultado = controller.AdicionarHabilidade(createDto);

        // Assert
        var resultadoCreated = Assert.IsType<CreatedAtActionResult>(resultado);
        var habilidade = Assert.IsType<ReadHabilidadeDto>(resultadoCreated.Value);
        Assert.Equal(0, habilidade.Id);
        Assert.Equal(createDto.Nome, habilidade.Nome);
        Assert.Equal(createDto.Descricao, habilidade.Descricao);
        Assert.Same(potentia, Assert.IsType<Habilidade>(potentia.Habilidades.Single()).Potentia);
        Assert.Equal(potentia.Nome, habilidade.NomePotentia);
        mock.Verify(repository => repository.Create(It.IsAny<Habilidade>()), Times.Once);
    }

    [Fact]
    public void PutHabilidadeDeveEditarObjetoCorretamente()
    {
        // Arrange
        var habilidade = new Habilidade { 
            Id = 1,
            Nome = "Foco", 
            Descricao = "Antiga", 
            IdPotentia = 2 
        };
        var updateDto = new UpdateHabilidadeDto { 
            Nome = "Precisão",
            Descricao = "Nova descrição", 
            IdPotentia = 3 
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(habilidade);

        // Act
        var resultado = controller.AtualizaHabilidade(1, updateDto);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(updateDto, Assert.IsType<UpdateHabilidadeDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Update(updateDto, habilidade), Times.Once);
    }

    [Fact]
    public void DeleteHabilidadeDeveExcluirObjetoCorretamente()
    {
        // Arrange
        var habilidade = new Habilidade { 
            Id = 1, 
            Nome = "Foco" 
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(habilidade);

        // Act
        var resultado = controller.DeletaHabilidade(1);

        // Assert
        Assert.IsType<NoContentResult>(resultado);
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Delete(habilidade), Times.Once);
    }
}
