namespace Airen.Api.Services;

public interface IGeminiService
{
    Task<string> CorrigirOrtografiaAsync(string texto, CancellationToken cancellationToken);
}
