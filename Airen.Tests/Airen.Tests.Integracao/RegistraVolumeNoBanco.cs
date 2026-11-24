using Airen.Infrastructure.Models;
using Airen.Tests.Integracao.Fixture;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Airen.Tests.Integracao;

[Collection("CollectionFixture")]
public class RegistraVolumeNoBanco
{
    private readonly MySqlFixture fixture;

    public RegistraVolumeNoBanco(MySqlFixture mySqlFixture)
    {
        fixture = mySqlFixture;
    }

    [Fact]
    public async Task GeraVolumeNoBancoEObtemVolumeEspecifico()
    {
        // Arrange
        using var context = fixture.CreateContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var faker = new Faker<Volume>()
            .RuleFor(v => v.Id, f => f.IndexFaker + 1)
            .RuleFor(v => v.Numero, f => f.IndexFaker + 1)
            .RuleFor(v => v.Nome, f => f.Commerce.ProductName())
            .RuleFor(v => v.DataLancamento, f => f.Date.Past())
            .RuleFor(v => v.MensagemVolume, f => f.Lorem.Sentence())
            .RuleFor(v => v.UrlCapa, f => f.Internet.Url());

        var lista = faker.Generate(50);
        var volumeEspecifico = new Volume
        {
            Id = 51,
            Numero = 51,
            Nome = "Volume especifico",
            DataLancamento = new DateTime(2026, 11, 23),
            MensagemVolume = "Este volume deve ser encontrado",
            UrlCapa = "https://imagens.exemplo/volume-51"
        };

        await context.Volumes!.AddRangeAsync(lista);
        await context.Volumes.AddAsync(volumeEspecifico);
        await context.SaveChangesAsync();

        // Act
        var encontrado = await context.Volumes
            .SingleAsync(v => v.Id == volumeEspecifico.Id);

        // Assert
        Assert.Equal("Volume especifico", encontrado.Nome);
        Assert.Equal("Este volume deve ser encontrado", encontrado.MensagemVolume);
        Assert.Equal(51, encontrado.Numero);
    }
}
