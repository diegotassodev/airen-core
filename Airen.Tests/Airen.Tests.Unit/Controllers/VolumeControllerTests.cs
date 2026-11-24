using Airen.Api.Controllers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Airen.Tests.Unit.Controllers;

public class VolumeControllerTests
{
    private readonly Mock<IRepository<Volume, ReadVolumeDto, UpdateVolumeDto>> mock = new();
    private readonly VolumeController controller;

    public VolumeControllerTests()
    {
        controller = new VolumeController(mock.Object);
    }

    [Fact]
    public void GetVolumesDeveRetornarListaDeVolumes()
    {
        // Arrange
        var volume = new ReadVolumeDto { 
            Id = 1, 
            Numero = 2, 
            Nome = "Volume 2", 
            MensagemVolume = "Mensagem", 
            UrlCapa = "capa",
            Capitulos =
            [
                new ReadCapituloDto
                {
                    Id = 10,
                    Numero = 1,
                    Titulo = "Capítulo de teste",
                    NumeroVolume = 2
                }
            ]
        };
        var resultadoEsperado = new List<ReadVolumeDto> { volume };
        mock.Setup(repository => repository.ReadAll()).Returns(resultadoEsperado);

        // Act
        var resultado = controller.VerVolumes();

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var volumes = Assert.IsAssignableFrom<IEnumerable<ReadVolumeDto>>(resultadoOk.Value);
        Assert.Equal(resultadoEsperado, volumes);
        Assert.Equal(2, Assert.Single(Assert.Single(volumes).Capitulos!).NumeroVolume);
        mock.Verify(repository => repository.ReadAll(), Times.Once);
    }

    [Fact]
    public void GetVolumePorIdDeveRetornarVolumeCorreto()
    {
        // Arrange
        var resultadoEsperado = new ReadVolumeDto { 
            Id = 1, 
            Numero = 2, 
            Nome = "Volume 2", 
            MensagemVolume = "Mensagem",
            UrlCapa = "capa",
            Capitulos =
            [
                new ReadCapituloDto
                {
                    Id = 10,
                    Numero = 1,
                    Titulo = "Capítulo de teste",
                    NumeroVolume = 2
                }
            ]
        };
        mock.Setup(repository => repository.ReadById(1)).Returns(resultadoEsperado);

        // Act
        var resultado = controller.BuscaVolumePorId(1);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var volumeRetornado = Assert.IsType<ReadVolumeDto>(resultadoOk.Value);
        Assert.Equal(resultadoEsperado, volumeRetornado);
        Assert.Equal(2, Assert.Single(volumeRetornado.Capitulos!).NumeroVolume);
        mock.Verify(repository => repository.ReadById(1), Times.Once);
    }

    [Fact]
    public void PostVolumeDeveCriarObjetoCorretamente()
    {
        // Arrange
        var dataLancamento = new DateTime(2020, 3, 4);
        var createDto = new CreateVolumeDto
        {
            Numero = 2,
            Nome = "Volume 2",
            DataLancamento = dataLancamento,
            MensagemVolume = "Mensagem",
            UrlCapa = "capa"
        };
        mock.Setup(repository => repository.Create(It.IsAny<Volume>()));

        // Act
        var resultado = controller.AdicionaVolume(createDto);

        // Assert
        var resultadoCreated = Assert.IsType<CreatedAtActionResult>(resultado);
        var volume = Assert.IsType<ReadVolumeDto>(resultadoCreated.Value);
        Assert.Equal(0, volume.Id);
        Assert.Equal(createDto.Numero, volume.Numero);
        Assert.Equal(createDto.Nome, volume.Nome);
        Assert.Equal(createDto.DataLancamento, volume.DataLancamento);
        Assert.Equal(createDto.MensagemVolume, volume.MensagemVolume);
        Assert.Equal(createDto.UrlCapa, volume.UrlCapa);
        mock.Verify(repository => repository.Create(It.IsAny<Volume>()), Times.Once);
    }

    [Fact]
    public void PutVolumeDeveEditarObjetoCorretamente()
    {
        // Arrange
        var volume = new Volume { 
            Id = 1, 
            Numero = 2, 
            Nome = "Volume 2" 
        };
        var updateDto = new UpdateVolumeDto
        {
            Numero = 3,
            Nome = "Volume 3",
            DataLancamento = new DateTime(2021, 4, 5),
            MensagemVolume = "Nova mensagem",
            UrlCapa = "nova-capa"
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(volume);

        // Act
        var resultado = controller.AtualizaVolume(1, updateDto);

        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(updateDto, Assert.IsType<UpdateVolumeDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Update(updateDto, volume), Times.Once);
    }

    [Fact]
    public void DeleteVolumeDeveExcluirObjetoCorretamente()
    {
        // Arrange
        var volume = new Volume 
        { 
            Id = 1, 
            Numero = 2, 
            Nome = "Volume 2" 
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(volume);

        // Act
        var resultado = controller.DeletaVolume(1);

        // Assert
        Assert.IsType<NoContentResult>(resultado);
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Delete(volume), Times.Once);
    }
}
