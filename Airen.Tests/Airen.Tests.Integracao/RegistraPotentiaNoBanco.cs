using Airen.Infrastructure.Models;
using Airen.Tests.Integracao.Fixture;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Airen.Tests.Integracao;

// REVISAR REGRA DE NEGÓCIO sobre personagem poder ou não ter mais de uma potentia.

[Collection("CollectionFixture")]
public class RegistraPotentiaNoBanco
{
    private readonly MySqlFixture fixture;

    public RegistraPotentiaNoBanco(MySqlFixture mySqlFixture)
    {
        fixture = mySqlFixture;
    }

    [Fact]
    public async Task GeraPotentiaNoBancoEObtemPotentiaEspecifica()
    {
        // Arrange
        using var context = fixture.CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var personagem = new Personagem
        {
            Id = 1,
            Nome = "Personagem da potentia",
            EstaVivo = true
        };
        await context.Personagens!.AddAsync(personagem);

        var faker = new Faker<Potentia>()
            .RuleFor(p => p.Id, f => f.IndexFaker + 1)
            .RuleFor(p => p.Nome, f => f.Lorem.Word())
            .RuleFor(p => p.Descricao, f => f.Lorem.Sentence())
            .RuleFor(p => p.LinkMusica, f => f.Internet.Url())
            .RuleFor(p => p.PersonagemId, _ => personagem.Id);

        var lista = faker.Generate(50);
        var potentiaEspecifica = new Potentia
        {
            Id = 51,
            Nome = "Potentia especifica",
            Descricao = "Esta potentia deve ser encontrada",
            LinkMusica = "https://musica.exemplo/potentia",
            PersonagemId = personagem.Id
        };

        await context.Potentias!.AddRangeAsync(lista);
        await context.Potentias.AddAsync(potentiaEspecifica);
        await context.SaveChangesAsync();

        // Act
        var encontrada = await context.Potentias
            .Include(p => p.Personagem)
            .SingleAsync(p => p.Id == potentiaEspecifica.Id);

        // Assert
        Assert.Equal("Potentia especifica", encontrada.Nome);
        Assert.Equal(personagem.Id, encontrada.PersonagemId);
        Assert.Equal("Personagem da potentia", encontrada.Personagem!.Nome);
    }
}
