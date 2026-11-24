using System.Net.Http.Json;
using Airen.Contracts.DTOs;

namespace Airen.Web.Services;

public class CapitulosAPI {

    private readonly HttpClient? _httpClient;

    public CapitulosAPI(IHttpClientFactory factory) 
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<IEnumerable<ReadCapituloDto>> GetCapituloAsync() 
    {
        var listaCapitulos = await _httpClient!.GetFromJsonAsync<IEnumerable<ReadCapituloDto>>("Capitulo");
        return listaCapitulos!;
    }

    public async Task PostCapitulosAsync (CreateCapituloDto dto)
    {
        await _httpClient!.PostAsJsonAsync("capitulo", dto);
    }

    public async Task PutCapitulosAsync (ReadCapituloDto readDto, UpdateCapituloDto updateDto)
    {
        await _httpClient!.PutAsJsonAsync($"capitulo/{readDto.Id}", updateDto);
    }

    public async Task DeleteCapitulosAsync (ReadCapituloDto readDto)
    {
        await _httpClient!.DeleteAsync($"capitulo/{readDto.Id}");
    }
}