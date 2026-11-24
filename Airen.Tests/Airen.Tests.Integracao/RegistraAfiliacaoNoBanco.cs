using Airen.Infrastructure.Context;
using Airen.Infrastructure.Models;
using Airen.Tests.Integracao.Fixture;
using Bogus;
using Microsoft.EntityFrameworkCore;
namespace Airen.Tests.Integracao;

[Collection("CollectionFixture")]
public class RegistraAfiliacaoNoBanco
{
    private readonly MySqlFixture fixture;

    public RegistraAfiliacaoNoBanco(MySqlFixture mySqlFixture)
    {
        fixture = mySqlFixture;
    }

    [Fact]
    public async Task GeraAfiliacaoNoBancoEObtemAfiliacaoEspecifica()
    {
        // Arrange
        var fakerAfiliacao = new Faker<Afiliacao>()
            .RuleFor(a => a.Id, f => f.IndexFaker + 1)
            .RuleFor(a => a.Nome, f => f.Company.CompanyName())
            .RuleFor(a => a.Descricao, f => f.Lorem.Sentence());
            
        using var context = fixture.CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var lista = fakerAfiliacao.Generate(50);

        var afiliacaoEspecifica = new Afiliacao
        {
            Id = 51,
            Nome = "Afiliacao específica",
            Descricao = "Esta afiliacao deve ser encontrada"
        };

        await context.Afiliacaos!.AddRangeAsync(lista);
        await context.Afiliacaos.AddAsync(afiliacaoEspecifica);
        await context.SaveChangesAsync();

        // Act
        var encontrada = await context.Afiliacaos.SingleAsync(a => a.Id == afiliacaoEspecifica.Id);

        // Assert
        Assert.Equal("Afiliacao específica", encontrada.Nome);
        Assert.Equal("Esta afiliacao deve ser encontrada", encontrada.Descricao);
    }

    [Fact]
    public async Task BancoEstaLimpoAntesDeOutroTeste()
    {
        // Arrange
        using var context = fixture.CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        
        // Act
        var afiliacoes = await context.Afiliacaos!.ToListAsync();
        
        // Assert
        Assert.Empty(afiliacoes);
    }
}
