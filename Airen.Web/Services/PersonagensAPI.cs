using System.Net.Http.Json;
using Airen.Contracts.DTOs;

namespace Airen.Web.Services;

public class PersonagensAPI {

    private readonly HttpClient? _httpClient;

    public PersonagensAPI(IHttpClientFactory factory) 
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<IEnumerable<ReadPersonagemDto>> GetPersonagensAsync() 
    {
        var listaPersonagens = await _httpClient!.GetFromJsonAsync<IEnumerable<ReadPersonagemDto>>("Personagem");
        return listaPersonagens!;
    }

    public async Task PostPersonagensAsync(CreatePersonagemDto dto)
    {
        using var response = await _httpClient!.PostAsJsonAsync("Personagem", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task PutPersonagensAsync(ReadPersonagemDto readDto, UpdatePersonagemDto updateDto)
    {
        using var response = await _httpClient!.PutAsJsonAsync($"Personagem/{readDto.Id}", updateDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeletePersonagensAsync(ReadPersonagemDto readDto)
    {
        using var response = await _httpClient!.DeleteAsync($"Personagem/{readDto.Id}");
        response.EnsureSuccessStatusCode();
    }
    
}