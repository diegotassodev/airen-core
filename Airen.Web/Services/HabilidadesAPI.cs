using System.Net.Http.Json;
using Airen.Contracts.DTOs;

namespace Airen.Web.Services;

public class HabilidadesAPI {

    private readonly HttpClient? _httpClient;

    public HabilidadesAPI(IHttpClientFactory factory) 
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<IEnumerable<ReadHabilidadeDto>> GetHabilidadeAsync() 
    {
        var listaHabilidades = await _httpClient!.GetFromJsonAsync<IEnumerable<ReadHabilidadeDto>>("Habilidade");
        return listaHabilidades!;
    }

    public async Task PostHabilidadesAsync (CreateHabilidadeDto dto)
    {
        using var response = await _httpClient!.PostAsJsonAsync("habilidade", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task PutHabilidadesAsync (ReadHabilidadeDto readDto, UpdateHabilidadeDto updateDto)
    {
        using var response = await _httpClient!.PutAsJsonAsync($"habilidade/{readDto.Id}", updateDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteHabilidadesAsync (ReadHabilidadeDto readDto)
    {
        using var response = await _httpClient!.DeleteAsync($"habilidade/{readDto.Id}");
        response.EnsureSuccessStatusCode();
    }
}