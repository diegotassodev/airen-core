using System.Net.Http.Json;
using Airen.Contracts.DTOs;

namespace Airen.Web.Services;

public class VolumesAPI {

    private readonly HttpClient? _httpClient;

    public VolumesAPI(IHttpClientFactory factory) 
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<IEnumerable<ReadVolumeDto>> GetVolumeAsync() 
    {
        var listaVolumes = await _httpClient!.GetFromJsonAsync<IEnumerable<ReadVolumeDto>>("Volume");
        return listaVolumes!;
    }

    public async Task PostVolumesAsync (CreateVolumeDto dto)
    {
        await _httpClient!.PostAsJsonAsync("volume", dto);
    }

    public async Task PutVolumesAsync (ReadVolumeDto readDto, UpdateVolumeDto updateDto)
    {
        await _httpClient!.PutAsJsonAsync($"volume/{readDto.Id}", updateDto);
    }

    public async Task DeleteVolumesAsync (ReadVolumeDto readDto)
    {
        await _httpClient!.DeleteAsync($"volume/{readDto.Id}");
    }
}