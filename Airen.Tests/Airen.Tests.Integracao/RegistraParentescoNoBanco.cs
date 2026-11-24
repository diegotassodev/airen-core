using Airen.Infrastructure.Models;
using Airen.Tests.Integracao.Fixture;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Airen.Tests.Integracao;

[Collection("CollectionFixture")]
public class RegistraParentescoNoBanco
{
    private readonly MySqlFixture fixture;

    public RegistraParentescoNoBanco(MySqlFixture mySqlFixture)
    {
        fixture = mySqlFixture;
    }

    [Fact]
    public async Task GeraParentescoNoBancoEObtemParentescoEspecifico()
    {
        // Arrange
        using var context = fixture.CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var personagens = Enumerable.Range(1, 52)
            .Select(id => new Personagem
            {
                Id = id,
                Nome = $"Personagem {id}",
                EstaVivo = true
            })
            .ToList();
        await context.Personagens!.AddRangeAsync(personagens);

        var faker = new Faker<Parentesco>()
            .RuleFor(p => p.Id, f => f.IndexFaker + 1)
            .RuleFor(p => p.TipoParentesco, f => f.PickRandom("Pai", "Mae", "Irmao", "Irma", "Primo"))
            .RuleFor(p => p.IdPersonagem, f => f.IndexFaker + 1)
            .RuleFor(p => p.IdParente, f => f.IndexFaker + 2);

        var lista = faker.Generate(50);
        var parentescoEspecifico = new Parentesco
        {
            Id = 51,
            TipoParentesco = "Pai",
            IdPersonagem = 51,
            IdParente = 52
        };
        var parentescoReciproco = new Parentesco
        {
            Id = 52,
            TipoParentesco = "Filho",
            IdPersonagem = 52,
            IdParente = 51
        };

        await context.Parentescos!.AddRangeAsync(lista);
        await context.Parentescos.AddRangeAsync(parentescoEspecifico, parentescoReciproco);
        await context.SaveChangesAsync();

        // Act
        var encontrado = await context.Parentescos
            .Include(p => p.Personagem)
            .Include(p => p.Parente)
            .SingleAsync(p => p.Id == parentescoEspecifico.Id);
        var reciprocoEncontrado = await context.Parentescos
            .SingleAsync(p => p.Id == parentescoReciproco.Id);

        // Assert
        Assert.Equal("Pai", encontrado.TipoParentesco);
        Assert.Equal("Personagem 51", encontrado.Personagem!.Nome);
        Assert.Equal("Personagem 52", encontrado.Parente!.Nome);
        Assert.Equal("Filho", reciprocoEncontrado.TipoParentesco);
    }

    [Fact]
    public async Task ImpedeMaisDeUmTipoDeParentescoParaOMesmoParOrdenado()
    {
        // Arrange
        using var context = fixture.CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var personagem = new Personagem
        {
            Id = 1,
            Nome = "Personagem A",
            EstaVivo = true
        };
        var parente = new Personagem
        {
            Id = 2,
            Nome = "Personagem B",
            EstaVivo = true
        };
        await context.Personagens!.AddRangeAsync(personagem, parente);
        
        await context.Parentescos!.AddAsync(new Parentesco
        {
            Id = 1,
            TipoParentesco = "Pai",
            IdPersonagem = personagem.Id,
            IdParente = parente.Id
        });
        await context.SaveChangesAsync();

        // Act
        await context.Parentescos.AddAsync(new Parentesco
        {
            Id = 2,
            TipoParentesco = "Avo",
            IdPersonagem = personagem.Id,
            IdParente = parente.Id
        });

        // Assert
        await Assert.ThrowsAsync<DbUpdateException>(() => context.SaveChangesAsync());
    }
}
