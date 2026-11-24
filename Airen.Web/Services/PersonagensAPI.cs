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
        await _httpClient!.PostAsJsonAsync("Personagem", dto);
    }

    public async Task PutPersonagensAsync(ReadPersonagemDto readDto, UpdatePersonagemDto updateDto)
    {
        await _httpClient!.PutAsJsonAsync($"Personagem/{readDto.Id}", updateDto);
    }

    public async Task DeletePersonagensAsync(ReadPersonagemDto readDto)
    {
        await _httpClient!.DeleteAsync($"Personagem/{readDto.Id}");
    }
    
}