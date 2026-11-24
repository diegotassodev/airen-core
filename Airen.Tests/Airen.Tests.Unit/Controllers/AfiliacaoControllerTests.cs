using Airen.Api.Controllers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Airen.Tests.Unit.Controllers;

public class AfiliacaoControllerTests
{
    private readonly Mock<IRepository<Afiliacao, ReadAfiliacaoDto, UpdateAfiliacaoDto>>? mock;
    private readonly AfiliacaoController? controller;

    public AfiliacaoControllerTests()
    {
        mock = new Mock<IRepository<Afiliacao, ReadAfiliacaoDto, UpdateAfiliacaoDto>>();
        controller = new AfiliacaoController(mock.Object);
    }

    [Fact]
    public void GetAfiliacaoDeveRetornarListaAfiliacoes()
    {
        // Arrange
        var afiliacaoDto = new ReadAfiliacaoDto()
        {
            Id = 1,
            Nome = "Teste",
            Descricao = "Teste-Descrição"
        };

        var resultadoEsperado = new List<ReadAfiliacaoDto> { afiliacaoDto };
        mock!.Setup(repository => repository.ReadAll()).Returns(resultadoEsperado);

        // Act
        var resultado = controller!.VerAfiliacoes();
        
        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var resultadoEncontrado = Assert.IsAssignableFrom<IEnumerable<ReadAfiliacaoDto>>(resultadoOk.Value);

        Assert.Equal(resultadoEsperado, resultadoEncontrado);
        mock.Verify(repository => repository.ReadAll(), Times.Once());

    }

    [Fact]
    public void GetAfiliacaoPorIdDeveRetornarAAfiliacaoCorretaPorId()
    {

        // Arrange
        var resultadoEsperado = new ReadAfiliacaoDto()
        {
            Id = 1,
            Nome = "Teste",
            Descricao = "Teste-Descrição"
        };

        mock!.Setup(repository => repository.ReadById(1)).Returns(resultadoEsperado);

        // Act
        var resultado = controller!.BuscaAfiliacaoPorId(1);
        
        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var resultadoEncontrado = Assert.IsAssignableFrom<ReadAfiliacaoDto>(resultadoOk.Value);

        Assert.Equal(resultadoEncontrado, resultadoEsperado);
        mock.Verify(repository => repository.ReadById(1), Times.Once);
        
    }

    [Fact]
    public void PostAfiliacaoDeveCriarUmObjetoCorretamente()
    {
        // Arrange
        var createDto = new CreateAfiliacaoDto()
        {
            Nome = "Teste",
            Descricao = "Teste-Descrição"
        };

        var resultadoEsperado = new ReadAfiliacaoDto()
        {
            Id = 0,
            Nome = "Teste",
            Descricao = "Teste-Descrição"
        };

        mock!.Setup(repository => repository.Create(It.IsAny<Afiliacao>()));

        // Act
        var resultado = controller!.AdicionaAfiliacao(createDto);

        // Assert
        var resultadoCreated = Assert.IsType<CreatedAtActionResult>(resultado);
        var resultadoEncontrado = Assert.IsAssignableFrom<ReadAfiliacaoDto>(resultadoCreated.Value);

        Assert.Equal(resultadoEncontrado, resultadoEsperado);

        mock.Verify (repository => repository.Create(It.IsAny<Afiliacao>()), Times.Once);
        
    }

    [Fact]
    public void PutAfiliacaoDeveEditarUmObjetoCorretamente()
    {
        // Arrange
        var updateDto = new UpdateAfiliacaoDto()
        {
            Nome = "Teste3",
            Descricao = "Teste-Descrição"
        };
        var resultadoEsperado = new ReadAfiliacaoDto()
        {
            Id = 0,
            Nome = "Teste3",
            Descricao = "Teste-Descrição"
        };

        var afiliacao = new Afiliacao()
        {
            Id = 0,
            Nome = "Teste",
            Descricao = "Teste-Descrição"
        };

        mock!.Setup(repository => repository.ReadEntityById(0)).Returns(afiliacao);
        mock!.Setup(repository => repository.Update(It.IsAny<UpdateAfiliacaoDto>(), It.IsAny<Afiliacao>()));       

        // Act
        var resultado = controller!.AtualizaAfiliacao(0, updateDto);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var resultadoEncontrado = Assert.IsAssignableFrom<ReadAfiliacaoDto?>(resultadoOk.Value);

        Assert.Equal(resultadoEncontrado, resultadoEsperado);
        mock.Verify(repository => repository.ReadEntityById(0), Times.Once);
        mock.Verify(repository => repository.Update(It.IsAny<UpdateAfiliacaoDto>(), It.IsAny<Afiliacao>()), Times.Once);
         
    }

    [Fact]
    public void DeleteDeveDeletarUmObjetoCorretamente ()
    {

        // Arrange
        var afiliacao = new Afiliacao()
        {
            Id = 0,
            Nome = "Teste",
            Descricao = "Teste-Descrição"
        };

        mock!.Setup(repository => repository.ReadEntityById(0)).Returns(afiliacao);
        mock!.Setup(repository => repository.Delete(afiliacao));

        // Act
        var resultado = controller!.DeletaAfiliacao(afiliacao.Id);   

        // Assert
        var resultadoNoContent = Assert.IsType<NoContentResult>(resultado);
        mock.Verify(repository => repository.ReadEntityById(0), Times.Once);
        mock.Verify(repository => repository.Delete(afiliacao), Times.Once);
        
    }
}