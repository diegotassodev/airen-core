using Airen.Api.Controllers.External;
using Airen.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Airen.Tests.Unit.Controllers;

public class GeminiControllerTests
{
    private readonly Mock<IGeminiService> geminiService = new();
    private readonly GeminiController controller;

    public GeminiControllerTests()
    {
        controller = new GeminiController(geminiService.Object);
    }

    [Fact]
    public async Task CorrigirOrtografiaDeveRetornarTextoCorrigidoPeloServico()
    {
        const string textoOriginal = "A ortografia presisa de correção.";
        const string textoCorrigido = "A ortografia precisa de correção.";
        using var cancellationSource = new CancellationTokenSource();
        var cancellationToken = cancellationSource.Token;

        geminiService
            .Setup(service => service.CorrigirOrtografiaAsync(textoOriginal, cancellationToken))
            .ReturnsAsync(textoCorrigido);

        var resultado = await controller.CorrigirOrtografia(textoOriginal, cancellationToken);

        var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal(textoCorrigido, resultadoOk.Value);
        geminiService.Verify(
            service => service.CorrigirOrtografiaAsync(textoOriginal, cancellationToken),
            Times.Once);
    }
}
