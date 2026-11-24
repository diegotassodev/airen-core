using Airen.Infrastructure.Models;
using Airen.Tests.Integracao.Fixture;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Airen.Tests.Integracao;

[Collection("CollectionFixture")]
public class RegistraCapituloNoBanco
{
    private readonly MySqlFixture fixture;

    public RegistraCapituloNoBanco(MySqlFixture mySqlFixture)
    {
        fixture = mySqlFixture;
    }

    [Fact]
    public async Task GeraCapituloNoBancoEObtemCapituloEspecifico()
    {
        // Arrange
        using var context = fixture.CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var volume = new Volume
        {
            Id = 1,
            Numero = 1,
            Nome = "Volume principal"
        };
        await context.Volumes!.AddAsync(volume);

        var faker = new Faker<Capitulo>()
            .RuleFor(c => c.Id, f => f.IndexFaker + 1)
            .RuleFor(c => c.Numero, f => f.IndexFaker + 1)
            .RuleFor(c => c.Titulo, f => f.Lorem.Sentence(3))
            .RuleFor(c => c.Resumo, f => f.Lorem.Paragraph())
            .RuleFor(c => c.NumeroPaginas, f => f.Random.Int(20, 100))
            .RuleFor(c => c.IdVolume, _ => volume.Id);

        var lista = faker.Generate(50);
        var capituloEspecifico = new Capitulo
        {
            Id = 51,
            Numero = 51,
            Titulo = "Capitulo especifico",
            Resumo = "Este capitulo deve ser encontrado",
            NumeroPaginas = 42,
            IdVolume = volume.Id
        };

        await context.Capitulos!.AddRangeAsync(lista);
        await context.Capitulos.AddAsync(capituloEspecifico);
        await context.SaveChangesAsync();

        // Act
        var encontrado = await context.Capitulos
            .Include(c => c.Volume)
            .SingleAsync(c => c.Id == capituloEspecifico.Id);

        // Assert   
        Assert.Equal("Capitulo especifico", encontrado.Titulo);
        Assert.Equal(volume.Id, encontrado.IdVolume);
        Assert.Equal("Volume principal", encontrado.Volume!.Nome);
    }
}
