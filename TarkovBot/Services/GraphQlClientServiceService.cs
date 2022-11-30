using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Serilog;
using TarkovBot.Services.Abstractions;

namespace TarkovBot.Services;

public class GraphQlClientServiceService : IGraphQlClientService
{
    private readonly ILogger _logger;
    private const    string  TarkovApiUrl = "https://api.tarkov.dev/graphql";

    private readonly HttpClient _httpClient = new(new SocketsHttpHandler()
    {
            PooledConnectionLifetime = TimeSpan.FromMinutes(1)
    });

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
    };

    public GraphQlClientServiceService(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<string> RequestAsString(string query)
    {
        var data = new Dictionary<string, string>
        {
                { "query", $"{{{query}}}" }
        };

        _logger.Information("Fetching data with query '{Query}'", query);

        var httpResponse = await _httpClient.PostAsJsonAsync(TarkovApiUrl, data);
        var responseContentString = await httpResponse.Content.ReadAsStringAsync();
        return responseContentString;
    }

    public async Task<T?> RequestAs<T>(string query) where T : class
    {
        var response = await RequestAsString(query);
        var jsonDoc = JsonDocument.Parse(response);
        var dataRoot = jsonDoc.RootElement.GetProperty("data");
        var responseData = dataRoot.Deserialize<T>(_jsonSerializerOptions);
        return responseData;
    }
}