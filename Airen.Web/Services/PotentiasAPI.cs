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
        await _httpClient!.PostAsJsonAsync("potentia", dto);
    }

    public async Task PutPotentiasAsync (ReadPotentiaDto readDto, UpdatePotentiaDto updateDto)
    {
        await _httpClient!.PutAsJsonAsync($"potentia/{readDto.Id}", updateDto);
    }

    public async Task DeletePotentiasAsync (ReadPotentiaDto readDto)
    {
        await _httpClient!.DeleteAsync($"potentia/{readDto.Id}");
    }
}