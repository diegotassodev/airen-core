using Airen.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Testcontainers.MySql;
namespace Airen.Tests.Integracao.Fixture;

[CollectionDefinition("CollectionFixture")]
public class CollectionFixture : ICollectionFixture<MySqlFixture>{}