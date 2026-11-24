using System.Net.Http.Json;
using Airen.Contracts.DTOs;

namespace Airen.Web.Services;

public class PotentiasAPI {

    private readonly HttpClient? _httpClient;

    public PotentiasAPI(IHttpClientFactory factory) 
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<IEnumerable<ReadPotentiaDto>> GetPotentiaAsync() 
    {
        var listaPotentias = await _httpClient!.GetFromJsonAsync<IEnumerable<ReadPotentiaDto>>("Potentia");
        return listaPotentias!;
    }

    public async Task PostPotentiasAsync (CreatePotentiaDto dto)
    {
        using var response = await _httpClient!.PostAsJsonAsync("potentia", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task PutPotentiasAsync (ReadPotentiaDto readDto, UpdatePotentiaDto updateDto)
    {
        using var response = await _httpClient!.PutAsJsonAsync($"potentia/{readDto.Id}", updateDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeletePotentiasAsync (ReadPotentiaDto readDto)
    {
        using var response = await _httpClient!.DeleteAsync($"potentia/{readDto.Id}");
        response.EnsureSuccessStatusCode();
    }
}