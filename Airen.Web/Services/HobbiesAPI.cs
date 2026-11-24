using System.Net.Http.Json;
using Airen.Contracts.DTOs;

namespace Airen.Web.Services;

public class HobbiesAPI
{
    private readonly HttpClient? _httpClient;

    public HobbiesAPI(IHttpClientFactory factory) 
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<IEnumerable<ReadHobbyDto>> GetHobbyAsync() 
    {
        var listaHobbies = await _httpClient!.GetFromJsonAsync<IEnumerable<ReadHobbyDto>>("Hobby");
        return listaHobbies!;
    }

    public async Task PostHobbiesAsync (CreateHobbyDto dto)
    {
        using var response = await _httpClient!.PostAsJsonAsync("hobby", dto);
        response.EnsureSuccessStatusCode();
    }

    public async Task PutHobbiesAsync (ReadHobbyDto readDto, UpdateHobbyDto updateDto)
    {
        using var response = await _httpClient!.PutAsJsonAsync($"hobby/{readDto.Id}", updateDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteHobbiesAsync (ReadHobbyDto readDto)
    {
        using var response = await _httpClient!.DeleteAsync($"hobby/{readDto.Id}");
        response.EnsureSuccessStatusCode();
    }
}