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
        await _httpClient!.PostAsJsonAsync("versao", dto);
    }

    public async Task PutVersoesAsync (ReadVersaoDto readDto, UpdateVersaoDto updateDto)
    {
        await _httpClient!.PutAsJsonAsync($"versao/{readDto.Id}", updateDto);
    }

    public async Task DeleteVersoesAsync (ReadVersaoDto readDto)
    {
        await _httpClient!.DeleteAsync($"versao/{readDto.Id}");
    }
}