using Airen.Infrastructure.Models;
using Airen.Tests.Integracao.Fixture;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Airen.Tests.Integracao;

[Collection("CollectionFixture")]
public class RegistraHobbyNoBanco
{
    private readonly MySqlFixture fixture;

    public RegistraHobbyNoBanco(MySqlFixture mySqlFixture)
    {
        fixture = mySqlFixture;
    }

    [Fact]
    public async Task GeraHobbyNoBancoEObtemHobbyEspecifico()
    {   
        // Arrange
        using var context = fixture.CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var personagem = new Personagem
        {
            Id = 1,
            Nome = "Personagem da versao",
            EstaVivo = true
        };
        await context.Personagens!.AddAsync(personagem);

        var versao = new Versao
        {
            Id = 1,
            Nome = "Versao principal",
            IdPersonagem = personagem.Id,
            IdadeInicial = 18,
            IdadeFinal = 25
        };
        await context.Versoes!.AddAsync(versao);

        var faker = new Faker<Hobby>()
            .RuleFor(h => h.Id, f => f.IndexFaker + 1)
            .RuleFor(h => h.Nome, f => f.Commerce.Department())
            .RuleFor(h => h.Descricao, f => f.Lorem.Sentence())
            .RuleFor(h => h.IdVersao, _ => versao.Id);

        var lista = faker.Generate(50);
        var hobbyEspecifico = new Hobby
        {
            Id = 51,
            Nome = "Hobby especifico",
            Descricao = "Este hobby deve ser encontrado",
            IdVersao = versao.Id
        };

        await context.Hobbies!.AddRangeAsync(lista);
        await context.Hobbies.AddAsync(hobbyEspecifico);
        await context.SaveChangesAsync();

        // Act
        var encontrado = await context.Hobbies
            .Include(h => h.Versao)
            .SingleAsync(h => h.Id == hobbyEspecifico.Id);

        // Assert
        Assert.Equal("Hobby especifico", encontrado.Nome);
        Assert.Equal(versao.Id, encontrado.IdVersao);
        Assert.Equal("Versao principal", encontrado.Versao!.Nome);
    }
}
