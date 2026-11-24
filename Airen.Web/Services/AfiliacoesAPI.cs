using System.Net.Http.Json;
using Airen.Contracts.DTOs;

namespace Airen.Web.Services;

public class AfiliacoesAPI {

    private readonly HttpClient? _httpClient;

    public AfiliacoesAPI(IHttpClientFactory factory) 
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<IEnumerable<ReadAfiliacaoDto>> GetAfiliacaoAsync() 
    {
        var listaAfiliacoes = await _httpClient!.GetFromJsonAsync<IEnumerable<ReadAfiliacaoDto>>("Afiliacao");
        return listaAfiliacoes!;
    }

    public async Task PostAfiliacoesAsync (CreateAfiliacaoDto dto)
    {
        await _httpClient!.PostAsJsonAsync("afiliacao", dto);
    }

    public async Task PutAfiliacoesAsync (ReadAfiliacaoDto readDto, UpdateAfiliacaoDto updateDto)
    {
        await _httpClient!.PutAsJsonAsync($"afiliacao/{readDto.Id}", updateDto);
    }

    public async Task DeleteAfiliacoesAsync (ReadAfiliacaoDto readDto)
    {
        await _httpClient!.DeleteAsync($"afiliacao/{readDto.Id}");
    }
}