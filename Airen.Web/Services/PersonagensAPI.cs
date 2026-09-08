using System.Net.Http.Json;
using Microsoft.Extensions.Http;

namespace Airen.Web.Services;

public class PersonagensApi {

    private readonly HttpClient? _httpClient;

    public PersonagensApi(IHttpClientFactory factory) {
        _httpClient = factory.CreateClient("API");
    }

    public void GetPersonagensAsync() {
        return _httpClient.GetFromJsonAsync<>();
    }
}