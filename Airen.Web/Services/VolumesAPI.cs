using System.Net.Http.Json;
using Airen.Contracts.DTOs;

namespace Airen.Web.Services;

public class VolumesAPI {

    private readonly HttpClient? _httpClient;

    public VolumesAPI(IHttpClientFactory factory) {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<IEnumerable<ReadVolumeDto>> GetVolumeAsync() {
        var listaVolumes = await _httpClient!.GetFromJsonAsync<IEnumerable<ReadVolumeDto>>("Volume");
        return listaVolumes!;
    }
}