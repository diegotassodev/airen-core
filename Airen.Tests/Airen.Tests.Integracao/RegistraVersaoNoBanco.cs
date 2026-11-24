using Airen.Infrastructure.Models;
using Airen.Tests.Integracao.Fixture;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Airen.Tests.Integracao;

[Collection("CollectionFixture")]
public class RegistraVersaoNoBanco
{
    private readonly MySqlFixture fixture;

    public RegistraVersaoNoBanco(MySqlFixture mySqlFixture)
    {
        fixture = mySqlFixture;
    }

    [Fact]
    public async Task GeraVersaoNoBancoEObtemVersaoEspecifica()
    {
        // Arrange
        using var context = fixture.CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var personagem = new Personagem
        {
            Id = 1,
            Nome = "Personagem com versoes",
            EstaVivo = true
        };
        await context.Personagens!.AddAsync(personagem);

        var faker = new Faker<Versao>()
            .RuleFor(v => v.Id, f => f.IndexFaker + 1)
            .RuleFor(v => v.Nome, f => f.Name.FirstName())
            .RuleFor(v => v.Genero, f => f.PickRandom("Masculino", "Feminino"))
            .RuleFor(v => v.IdadeInicial, f => f.Random.Int(10, 30))
            .RuleFor(v => v.IdadeFinal, f => f.Random.Int(31, 60))
            .RuleFor(v => v.Descricao, f => f.Lorem.Sentence())
            .RuleFor(v => v.Personalidade, f => f.Lorem.Word())
            .RuleFor(v => v.IdPersonagem, _ => personagem.Id);

        var lista = faker.Generate(50);
        var versaoEspecifica = new Versao
        {
            Id = 51,
            Nome = "Versao especifica",
            Genero = "Masculino",
            IdadeInicial = 18,
            IdadeFinal = 30,
            Descricao = "Esta versao deve ser encontrada",
            Personalidade = "Determinado",
            IdPersonagem = personagem.Id
        };

        await context.Versoes!.AddRangeAsync(lista);
        await context.Versoes.AddAsync(versaoEspecifica);
        await context.SaveChangesAsync();
    
        // Act
        var encontrada = await context.Versoes
            .Include(v => v.Personagem)
            .SingleAsync(v => v.Id == versaoEspecifica.Id);

        // Assert   
        Assert.Equal("Versao especifica", encontrada.Nome);
        Assert.Equal("Determinado", encontrada.Personalidade);
        Assert.Equal("Personagem com versoes", encontrada.Personagem!.Nome);
    }
}
