using System.Net.Http.Json;

namespace Airen.Web.Services;

public class GeminiAPI
{
    private readonly HttpClient _httpClient;
   
    public GeminiAPI(IHttpClientFactory factory) 
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<string> CorrigirOrtografiaAsync(string texto)
    {
        var response = await _httpClient.PostAsJsonAsync("Gemini", texto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<string>()
            ?? throw new InvalidOperationException("A API não retornou o texto corrigido.");
    }
}