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

    public async Task<ReadCapituloDto> PostCapitulosAsync(CreateCapituloDto dto)
    {
        using var response = await _httpClient!.PostAsJsonAsync("capitulo", dto);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ReadCapituloDto>()
            ?? throw new InvalidOperationException("A API não retornou o capítulo criado.");
    }

    public async Task PutCapitulosAsync(ReadCapituloDto readDto, UpdateCapituloDto updateDto)
    {
        using var response = await _httpClient!.PutAsJsonAsync($"capitulo/{readDto.Id}", updateDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCapitulosAsync(ReadCapituloDto readDto)
    {
        using var response = await _httpClient!.DeleteAsync($"capitulo/{readDto.Id}");
        response.EnsureSuccessStatusCode();
    }
}