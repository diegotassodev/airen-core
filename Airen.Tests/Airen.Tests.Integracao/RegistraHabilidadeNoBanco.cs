using Airen.Infrastructure.Models;
using Airen.Tests.Integracao.Fixture;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Airen.Tests.Integracao;

[Collection("CollectionFixture")]
public class RegistraHabilidadeNoBanco
{
    private readonly MySqlFixture fixture;

    public RegistraHabilidadeNoBanco(MySqlFixture mySqlFixture)
    {
        fixture = mySqlFixture;
    }

    [Fact]
    public async Task GeraHabilidadeNoBancoEObtemHabilidadeEspecifica()
    {
        // Arrange
        using var context = fixture.CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var personagem = new Personagem
        {
            Id = 1,
            Nome = "Personagem da Potentia",
            NomeOriginal = "Personagem",
            EstaVivo = true
        };
        await context.Personagens!.AddAsync(personagem);

        var potentia = new Potentia
        {
            Id = 1,
            Nome = "Potentia principal",
            Descricao = "Potentia usada pela habilidade",
            PersonagemId = personagem.Id
        };
        await context.Potentias!.AddAsync(potentia);

        var faker = new Faker<Habilidade>()
            .RuleFor(h => h.Id, f => f.IndexFaker + 1)
            .RuleFor(h => h.Nome, f => f.Lorem.Word())
            .RuleFor(h => h.Descricao, f => f.Lorem.Sentence())
            .RuleFor(h => h.IdPotentia, _ => potentia.Id);

        var lista = faker.Generate(50);
        var habilidadeEspecifica = new Habilidade
        {
            Id = 51,
            Nome = "Habilidade especifica",
            Descricao = "Esta habilidade deve ser encontrada",
            IdPotentia = potentia.Id
        };

        await context.Habilidades!.AddRangeAsync(lista);
        await context.Habilidades.AddAsync(habilidadeEspecifica);
        await context.SaveChangesAsync();

        // Act
        var encontrada = await context.Habilidades
            .Include(h => h.Potentia)
            .SingleAsync(h => h.Id == habilidadeEspecifica.Id);

        // Assert
        Assert.Equal("Habilidade especifica", encontrada.Nome);
        Assert.Equal(potentia.Id, encontrada.IdPotentia);
        Assert.Equal("Potentia principal", encontrada.Potentia!.Nome);
    }
}
