using Airen.Api.Controllers;
using Airen.Api.Mappers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Airen.Tests.Unit.Controllers;

public class CapituloControllerTests
{
    private readonly Mock<IRepository<Capitulo, ReadCapituloDto, UpdateCapituloDto>>? mock;
    private readonly Mock<IRepository<Volume, ReadVolumeDto, UpdateVolumeDto>>? mockVolume;
    private readonly CapituloController? controller;

    public CapituloControllerTests()
    {
        mock = new Mock<IRepository<Capitulo, ReadCapituloDto, UpdateCapituloDto>>();
        mockVolume = new Mock<IRepository<Volume, ReadVolumeDto, UpdateVolumeDto>>();
        controller = new CapituloController(mock.Object);
    }

    [Fact]
    public void GetCapituloDeveRetornarListaCapitulo()
    {
        // Arrange
        var capituloDto = new ReadCapituloDto()
        {
            Id = 1,
            Numero = 2,
            Titulo = "Exemplo-Titulo",
            Resumo = "Exemplo-Resumo",
            NumeroPaginas = 10,
            NumeroVolume = 1
        };

        var resultadoEsperado = new List<ReadCapituloDto> { capituloDto };
        mock!.Setup(repository => repository.ReadAll()).Returns(resultadoEsperado);

        // Act
        var resultado = controller!.VerCapitulos();
        
        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var resultadoEncontrado = Assert.IsAssignableFrom<IEnumerable<ReadCapituloDto>>(resultadoOk.Value);

        Assert.Equal(resultadoEsperado, resultadoEncontrado);
        mock.Verify(repository => repository.ReadAll(), Times.Once());

    }

     [Fact]
    public void GetCapituloPorIdDeveRetornarOCapituloCorretoPorId()
    {
        // Arrange
        var resultadoEsperado = new ReadCapituloDto()
        {
            Id = 1,
            Numero = 2,
            Titulo = "Exemplo-Titulo",
            Resumo = "Exemplo-Resumo",
            NumeroPaginas = 10,
            NumeroVolume = 1
        };

        mock!.Setup(repository => repository.ReadById(1)).Returns(resultadoEsperado);

        // Act
        var resultado = controller!.BuscaCapituloPorId(1);
        
        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var resultadoEncontrado = Assert.IsAssignableFrom<ReadCapituloDto>(resultadoOk.Value);

        Assert.Equal(resultadoEncontrado, resultadoEsperado);
        mock.Verify(repository => repository.ReadById(1), Times.Once);
        
    }

    [Fact]
    public void PostCapituloDeveCriarUmObjetoCorretamente()
    {
        // Arrange

        var volume = new Volume()
        {
            Id = 1,
            Numero = 1,
            Nome = "Exemplo-Nome-Volume",
            DataLancamento = new DateTime(2005,10,10),
            MensagemVolume = "Exemplo-Mensagem",
            UrlCapa = "Exemplo-Capa",
        };

        mockVolume!.Setup(repository => repository.ReadEntityById(volume.Id)).Returns(volume);

        var createDto = new CreateCapituloDto()
        {
            Numero = 2,
            Titulo = "Exemplo-Titulo",
            Resumo = "Exemplo-Resumo",
            NumeroPaginas = 10,
            IdVolume = volume.Id,
        };

        var resultadoEsperado = new ReadCapituloDto()
        {
            Id = 0,
            Numero = 2,
            Titulo = "Exemplo-Titulo",
            Resumo = "Exemplo-Resumo",
            NumeroPaginas = 10,
            NumeroVolume = volume.Numero,
        };

        volume.Capitulos = new List<Capitulo>();
        mock!.Setup(repository => repository.Create(It.IsAny<Capitulo>()))
            .Callback<Capitulo>(capitulo =>
            {
                capitulo.Volume = volume;
                volume.Capitulos.Add(capitulo);
            });

        // Act
        var resultado = controller!.AdicionaCapitulo(createDto);

        // Assert
        var resultadoCreated = Assert.IsType<CreatedAtActionResult>(resultado);
        var resultadoEncontrado = Assert.IsAssignableFrom<ReadCapituloDto>(resultadoCreated.Value);

        Assert.Equal(resultadoEncontrado, resultadoEsperado);
        Assert.Same(volume, Assert.IsType<Capitulo>(volume.Capitulos.Single()).Volume);

        mock.Verify (repository => repository.Create(It.IsAny<Capitulo>()), Times.Once);
        
    }

    [Fact]
    public void PutCapituloDeveEditarUmObjetoCorretamente()
    {
        // Arrange
        var volume = new Volume()
        {
            Id = 1,
            Numero = 1,
            Nome = "Exemplo-Nome-Volume",
            DataLancamento = new DateTime(2005,10,10),
            MensagemVolume = "Exemplo-Mensagem",
            UrlCapa = "Exemplo-Capa",
        };

        var updateDto = new UpdateCapituloDto()
        {
            Numero = 2,
            Titulo = "Exemplo-Titulo",
            Resumo = "Exemplo-Resumo",
            NumeroPaginas = 10,
            IdVolume = volume.Id,
        };
        var resultadoEsperado = new ReadCapituloDto()
        {
            Id = 1,
            Numero = 2,
            Titulo = "Exemplo-Titulo",
            Resumo = "Exemplo-Resumo",
            NumeroPaginas = 10,
            NumeroVolume = volume.Numero,
        };

        var capitulo = new Capitulo()
        {
            Id = 1,
            Numero = 2,
            Titulo = "Exemplo-Titulo",
            Resumo = "Exemplo-Resumo",
            NumeroPaginas = 10,
            IdVolume = volume.Id,
            Volume = volume,
        };

        mock!.Setup(repository => repository.ReadEntityById(1)).Returns(capitulo);
        mock!.Setup(repository => repository.Update(It.IsAny<UpdateCapituloDto>(), It.IsAny<Capitulo>()))
            .Returns(resultadoEsperado);

        // Act
        var resultado = controller!.AtualizaCapitulo(1, updateDto);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var resultadoEncontrado = Assert.IsAssignableFrom<ReadCapituloDto?>(resultadoOk.Value);

        Assert.Equal(resultadoEncontrado, resultadoEsperado);
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Update(It.IsAny<UpdateCapituloDto>(), It.IsAny<Capitulo>()), Times.Once);
         
    }

    [Fact]
    public void DeleteDeveDeletarUmObjetoCorretamente ()
    {
        // Arrange
        var volume = new Volume()
        {
            Id = 1,
            Numero = 1,
            Nome = "Exemplo-Nome-Volume",
            DataLancamento = new DateTime(2005,10,10),
            MensagemVolume = "Exemplo-Mensagem",
            UrlCapa = "Exemplo-Capa",
        };

        var capitulo = new Capitulo()
        {
            Id = 1,
            Numero = 2,
            Titulo = "Exemplo-Titulo",
            Resumo = "Exemplo-Resumo",
            NumeroPaginas = 10,
            IdVolume = volume.Id,
            Volume = volume,
        };

        mock!.Setup(repository => repository.ReadEntityById(1)).Returns(capitulo);
        mock!.Setup(repository => repository.Delete(capitulo));

        // Act
        var resultado = controller!.DeletaCapitulo(capitulo.Id);   

        // Assert
        var resultadoNoContent = Assert.IsType<NoContentResult>(resultado);
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Delete(capitulo), Times.Once);
        
    }

}