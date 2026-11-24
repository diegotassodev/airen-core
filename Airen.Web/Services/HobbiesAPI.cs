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
        await _httpClient!.PostAsJsonAsync("hobby", dto);
    }

    public async Task PutHobbiesAsync (ReadHobbyDto readDto, UpdateHobbyDto updateDto)
    {
        await _httpClient!.PutAsJsonAsync($"hobby/{readDto.Id}", updateDto);
    }

    public async Task DeleteHobbiesAsync (ReadHobbyDto readDto)
    {
        await _httpClient!.DeleteAsync($"hobby/{readDto.Id}");
    }
}