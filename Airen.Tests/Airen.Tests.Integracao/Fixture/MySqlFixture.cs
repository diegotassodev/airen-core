using Airen.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Testcontainers.MySql;

namespace Airen.Tests.Integracao.Fixture;

public class MySqlFixture : IAsyncLifetime
{
    private readonly MySqlContainer _mySqlContainer = new MySqlBuilder("mysql:8.0")
        .WithDatabase("teste")
        .WithUsername("root")
        .WithPassword("root")
        .Build();

    public async Task InitializeAsync()
    {
        await _mySqlContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _mySqlContainer.DisposeAsync();
    }

    public AirenContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AirenContext>()
            .UseMySql(_mySqlContainer.GetConnectionString(), ServerVersion.AutoDetect(_mySqlContainer.GetConnectionString()))
            .Options;
        return new AirenContext(options, Mock.Of<IConfiguration>());
    }

}
