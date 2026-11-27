namespace Airen.Web.Services;

public interface IGeminiAPI
{
    Task<string> CorrigirOrtografiaAsync(string texto);
}
