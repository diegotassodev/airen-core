using System.Net.Http.Json;
using Airen.Contracts.DTOs;

namespace Airen.Web.Services;

public class VersoesAPI {

    private readonly HttpClient? _httpClient;

    public VersoesAPI(IHttpClientFactory factory) 
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<IEnumerable<ReadVersaoDto>> GetVersaoAsync() 
    {
        var listaVersoes = await _httpClient!.GetFromJsonAsync<IEnumerable<ReadVersaoDto>>("Versao");
        return listaVersoes!;
    }

    public async Task PostVersoesAsync (CreateVersaoDto dto)
    {
        using var response = await _httpClient!.PostAsJsonAsync("versao", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task PutVersoesAsync (ReadVersaoDto readDto, UpdateVersaoDto updateDto)
    {
        using var response = await _httpClient!.PutAsJsonAsync($"versao/{readDto.Id}", updateDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteVersoesAsync (ReadVersaoDto readDto)
    {
        using var response = await _httpClient!.DeleteAsync($"versao/{readDto.Id}");
        response.EnsureSuccessStatusCode();
    }
}