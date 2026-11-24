using System.Net.Http.Json;
using Airen.Contracts.DTOs;

namespace Airen.Web.Services;

public class TecnicasAPI {

    private readonly HttpClient? _httpClient;

    public TecnicasAPI(IHttpClientFactory factory) 
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<IEnumerable<ReadTecnicaDto>> GetTecnicaAsync() 
    {
        var listaTecnicas = await _httpClient!.GetFromJsonAsync<IEnumerable<ReadTecnicaDto>>("Tecnica");
        return listaTecnicas!;
    }

    public async Task PostTecnicasAsync (CreateTecnicaDto dto)
    {
        using var response = await _httpClient!.PostAsJsonAsync("tecnica", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task PutTecnicasAsync (ReadTecnicaDto readDto, UpdateTecnicaDto updateDto)
    {
        using var response = await _httpClient!.PutAsJsonAsync($"tecnica/{readDto.Id}", updateDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTecnicasAsync (ReadTecnicaDto readDto)
    {
        using var response = await _httpClient!.DeleteAsync($"tecnica/{readDto.Id}");
        response.EnsureSuccessStatusCode();
    }
}