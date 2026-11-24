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
        await _httpClient!.PostAsJsonAsync("parentesco", dto);
    }

    public async Task PutParentescosAsync (ReadParentescoDto readDto, UpdateParentescoDto updateDto)
    {
        await _httpClient!.PutAsJsonAsync($"parentesco/{readDto.Id}", updateDto);
    }

    public async Task DeleteParentescosAsync (ReadParentescoDto readDto)
    {
        await _httpClient!.DeleteAsync($"parentesco/{readDto.Id}");
    }
}