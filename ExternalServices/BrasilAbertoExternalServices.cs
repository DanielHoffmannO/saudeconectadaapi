using System.Text.Json;

namespace TelemedicinaApi.ExternalServices;

public class DistanceService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://api.brasilaberto.com/v1/distance";

    public DistanceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<double?> ObterDistanciaAsync(string cepA, string cepB)
    {
        var url = $"{BaseUrl}?pointA={cepA}&pointB={cepB}&mode=STRAIGHT_LINE";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<DistanceResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result?.Result?.Distance;
    }
}

public class DistanceResponse
{
    public DistanceResult Result { get; set; }
}

public class DistanceResult
{
    public double Distance { get; set; }
}