using Airen.Api.Controllers;
using Airen.Api.Repositories;
using Airen.Contracts.DTOs;
using Airen.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Airen.Tests.Unit.Controllers;

public class HobbyControllerTests
{
    private readonly Mock<IRepository<Hobby, ReadHobbyDto, UpdateHobbyDto>> mock = new();
    private readonly HobbyController controller;

    public HobbyControllerTests()
    {
        controller = new HobbyController(mock.Object);
    }

    [Fact]
    public void GetHobbiesDeveRetornarListaDeHobbies()
    {
        // Arrange
        var hobby = new ReadHobbyDto {
            Id = 1, 
            Nome = "Leitura", 
            Descricao = "Biologia",
            NomePersonagem = "Hakira"
        };

        var resultadoEsperado = new List<ReadHobbyDto> { hobby };
        mock.Setup(repository => repository.ReadAll()).Returns(resultadoEsperado);

        // Act
        var resultado = controller.VerHobbies();

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        var hobbies = Assert.IsAssignableFrom<IEnumerable<ReadHobbyDto>>(resultadoOk.Value);
        Assert.Equal(resultadoEsperado, hobbies);
        mock.Verify(repository => repository.ReadAll(), Times.Once);
    }

    [Fact]
    public void GetHobbyPorIdDeveRetornarHobbyCorreto()
    {
        // Arrange
        var resultadoEsperado = new ReadHobbyDto { 
            Id = 1,
            Nome = "Leitura",
            Descricao = "Livros",
            NomePersonagem = "Alex"
        };
        mock.Setup(repository => repository.ReadById(1)).Returns(resultadoEsperado);

        // Act
        var resultado = controller.BuscaHobbyPorId(1);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(resultadoEsperado, Assert.IsType<ReadHobbyDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadById(1), Times.Once);
    }

    [Fact]
    public void PostHobbyDeveCriarObjetoCorretamente()
    {
        // Arrange
        var createDto = new CreateHobbyDto 
        { 
            Nome = "Leitura",
            Descricao = "Livros",
            IdVersao = 2 
        };
        var personagem = new Personagem 
        { 
            Id = 1, 
            Nome = "Alex" 
        };
        var versao = new Versao
        {
            Id = createDto.IdVersao,
            Personagem = personagem,
            Hobbies = new List<Hobby>()
        };
        mock.Setup(repository => repository.Create(It.IsAny<Hobby>()))
            .Callback<Hobby>(hobby =>
            {
                hobby.Versao = versao;
                versao.Hobbies.Add(hobby);
            });

        // Act
        var resultado = controller.AdicionarHobby(createDto);

        // Assert
        var resultadoCreated = Assert.IsType<CreatedAtActionResult>(resultado);
        var hobby = Assert.IsType<ReadHobbyDto>(resultadoCreated.Value);
        Assert.Equal(0, hobby.Id);
        Assert.Equal(createDto.Nome, hobby.Nome);
        Assert.Equal(createDto.Descricao, hobby.Descricao);
        Assert.Same(versao, versao.Hobbies.Single().Versao);
        Assert.Equal(personagem.Nome, hobby.NomePersonagem);
        mock.Verify(repository => repository.Create(It.IsAny<Hobby>()), Times.Once);
    }

    [Fact]
    public void PutHobbyDeveEditarObjetoCorretamente()
    {
        // Arrange
        var hobby = new Hobby { 
            Id = 1, 
            Nome = "Leitura", 
            Descricao = "Antiga", 
            IdVersao = 2 
        };
        var updateDto = new UpdateHobbyDto 
        { 
            Nome = "Corrida", 
            Descricao = "Nova descrição",
            IdVersao = 3 
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(hobby);

        // Act
        var resultado = controller.AtualizaHobby(1, updateDto);

        // Assert
        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(updateDto, Assert.IsType<UpdateHobbyDto>(resultadoOk.Value));
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Update(updateDto, hobby), Times.Once);
    }

    [Fact]
    public void DeleteHobbyDeveExcluirObjetoCorretamente()
    {   
        // Arrange
        var hobby = new Hobby { 
            Id = 1,
            Nome = "Leitura" 
        };
        mock.Setup(repository => repository.ReadEntityById(1)).Returns(hobby);

        // Act
        var resultado = controller.DeletaHobby(1);

        // Assert
        Assert.IsType<NoContentResult>(resultado);
        mock.Verify(repository => repository.ReadEntityById(1), Times.Once);
        mock.Verify(repository => repository.Delete(hobby), Times.Once);
    }
}
