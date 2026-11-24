using System.Net.Http.Json;
using Airen.Contracts.DTOs;

namespace Airen.Web.Services;

public class ParentescoAPI {

    private readonly HttpClient? _httpClient;

    public ParentescoAPI(IHttpClientFactory factory) 
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<IEnumerable<ReadParentescoDto>> GetParentescoAsync() 
    {
        var listaParentescos = await _httpClient!.GetFromJsonAsync<IEnumerable<ReadParentescoDto>>("Parentesco");
        return listaParentescos!;
    }

    public async Task PostParentescosAsync (CreateParentescoDto dto)
    {
        using var response = await _httpClient!.PostAsJsonAsync("parentesco", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task PutParentescosAsync (ReadParentescoDto readDto, UpdateParentescoDto updateDto)
    {
        using var response = await _httpClient!.PutAsJsonAsync($"parentesco/{readDto.Id}", updateDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteParentescosAsync (ReadParentescoDto readDto)
    {
        using var response = await _httpClient!.DeleteAsync($"parentesco/{readDto.Id}");
        response.EnsureSuccessStatusCode();
    }
}