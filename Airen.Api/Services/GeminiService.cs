
using Google.GenAI;

namespace Airen.Api.Services;

public class GeminiService
{
    private readonly string _geminiKey;
    private readonly HttpClient _httpClient;

    public GeminiService(IConfiguration configuration, HttpClient httpClient)
    {
        _geminiKey = configuration["Gemini:ApiKey"] ?? throw new InvalidOperationException("A configuração Gemini:ApiKey não foi encontrada.");
        _httpClient = httpClient;
    }

    public async Task<string> RetornarResposta(string pergunta)
    {
        var client = new Client(apiKey: _geminiKey);
        var response = await client.Models.GenerateContentAsync(
            model: "gemini-2.5-flash",
            contents: pergunta + "\nSeja simples e direto, respondendo apenas o que a pergunta solicita, evite gastar tokens."
        );
        return response.Text!;
    }
}