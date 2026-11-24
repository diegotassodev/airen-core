using Airen.Infrastructure.Models;
using Airen.Tests.Integracao.Fixture;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Airen.Tests.Integracao;

[Collection("CollectionFixture")]
public class RegistraPersonagemNoBanco
{
    private readonly MySqlFixture fixture;

    public RegistraPersonagemNoBanco(MySqlFixture mySqlFixture)
    {
        fixture = mySqlFixture;
    }

    [Fact]
    public async Task GeraPersonagemNoBancoEObtemPersonagemEspecifico()
    {
        // Arrange
        using var context = fixture.CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var faker = new Faker<Personagem>()
            .RuleFor(p => p.Id, f => f.IndexFaker + 1)
            .RuleFor(p => p.Nome, f => f.Name.FirstName())
            .RuleFor(p => p.NomeOriginal, f => f.Name.FullName())
            .RuleFor(p => p.Biografia, f => f.Lorem.Paragraph())
            .RuleFor(p => p.DataNascimento, f => f.Date.Past(30))
            .RuleFor(p => p.EstaVivo, f => f.Random.Bool());

        var lista = faker.Generate(50);
        var personagemEspecifico = new Personagem
        {
            Id = 51,
            Nome = "Personagem especifico",
            NomeOriginal = "Personagem Original",
            Biografia = "Este personagem deve ser encontrado",
            EstaVivo = true
        };

        await context.Personagens!.AddRangeAsync(lista);
        await context.Personagens.AddAsync(personagemEspecifico);
        await context.SaveChangesAsync();

        // Act
        var encontrado = await context.Personagens
            .SingleAsync(p => p.Id == personagemEspecifico.Id);

        // Assert
        Assert.Equal("Personagem especifico", encontrado.Nome);
        Assert.Equal("Este personagem deve ser encontrado", encontrado.Biografia);
        Assert.True(encontrado.EstaVivo);
    }
}
