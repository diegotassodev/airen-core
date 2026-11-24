using Airen.Infrastructure.Models;
using Airen.Tests.Integracao.Fixture;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Airen.Tests.Integracao;

[Collection("CollectionFixture")]
public class RegistraTecnicaNoBanco
{
    private readonly MySqlFixture fixture;

    public RegistraTecnicaNoBanco(MySqlFixture mySqlFixture)
    {
        fixture = mySqlFixture;
    }

    [Fact]
    public async Task GeraTecnicaNoBancoEObtemTecnicaEspecifica()
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

        var faker = new Faker<Tecnica>()
            .RuleFor(t => t.Id, f => f.IndexFaker + 1)
            .RuleFor(t => t.Nome, f => f.Lorem.Word())
            .RuleFor(t => t.Descricao, f => f.Lorem.Sentence())
            .RuleFor(t => t.Categoria, f => f.PickRandom("Ataque", "Defesa", "Suporte"))
            .RuleFor(t => t.IdVersao, _ => versao.Id);

        var lista = faker.Generate(50);
        var tecnicaEspecifica = new Tecnica
        {
            Id = 51,
            Nome = "Tecnica especifica",
            Descricao = "Esta tecnica deve ser encontrada",
            Categoria = "Ataque",
            IdVersao = versao.Id
        };

        await context.Tecnicas!.AddRangeAsync(lista);
        await context.Tecnicas.AddAsync(tecnicaEspecifica);
        await context.SaveChangesAsync();

        // Act
        var encontrada = await context.Tecnicas
            .Include(t => t.Versao)
            .SingleAsync(t => t.Id == tecnicaEspecifica.Id);

        // Assert
        Assert.Equal("Tecnica especifica", encontrada.Nome);
        Assert.Equal("Ataque", encontrada.Categoria);
        Assert.Equal("Versao principal", encontrada.Versao!.Nome);
    }
}
