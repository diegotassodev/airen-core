using Airen.Web.Components;
using Airen.Web.Services;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MudBlazor.Services;

namespace Airen.Tests.Unit.Components;

public class TextCorrectionButtonTests
{
    [Fact]
    public async Task CliqueNoBotaoChamaServicoEEnviaTextoCorrigidoAoFormulario()
    {
        const string textoOriginal = "A ortografia presisa de correção.";
        const string textoCorrigido = "A ortografia precisa de correção.";
        var resposta = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
        var geminiApi = new Mock<IGeminiAPI>();
        geminiApi
            .Setup(api => api.CorrigirOrtografiaAsync(textoOriginal))
            .Returns(resposta.Task);
        string? textoRecebidoPeloFormulario = null;

        using var contexto = new BunitContext();
        contexto.JSInterop.Mode = JSRuntimeMode.Loose;
        contexto.Services.AddSingleton(geminiApi.Object);
        contexto.Services.AddMudServices();

        var componente = contexto.Render<TextCorrectionButton>(parameters => parameters
            .Add(button => button.Text, textoOriginal)
            .Add(button => button.TextChanged,
                EventCallback.Factory.Create<string?>(contexto, texto => textoRecebidoPeloFormulario = texto)));

        var clique = componente.Find("button").ClickAsync();

        Assert.Contains("Pensando...", componente.Markup);
        geminiApi.Verify(api => api.CorrigirOrtografiaAsync(textoOriginal), Times.Once);

        resposta.SetResult(textoCorrigido);
        await clique;

        Assert.Equal(textoCorrigido, textoRecebidoPeloFormulario);
    }

    [Fact]
    public void TextoVazioNaoMostraBotaoDeCorrecao()
    {
        var geminiApi = new Mock<IGeminiAPI>();

        using var contexto = new BunitContext();
        contexto.JSInterop.Mode = JSRuntimeMode.Loose;
        contexto.Services.AddSingleton(geminiApi.Object);
        contexto.Services.AddMudServices();

        var componente = contexto.Render<TextCorrectionButton>(parameters =>
            parameters.Add(button => button.Text, string.Empty));

        Assert.Empty(componente.FindAll("button"));
        geminiApi.VerifyNoOtherCalls();
    }
}
