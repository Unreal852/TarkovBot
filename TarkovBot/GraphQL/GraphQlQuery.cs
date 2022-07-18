using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Serilog;
using TarkovBot.Extensions;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming

namespace TarkovBot.GraphQL;

public class GraphQlQuery
{
    private const string QueryArgsDelimiter = "${ARGS}";
    private const string TarkovDevUrl       = "https://api.tarkov.dev/graphql";

    private static readonly HttpClient HttpClient = new();

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
    };

    public GraphQlQuery(string queryName, string query)
    {
        QueryName = queryName;
        Query = query;
    }

    private string Query     { get; }
    private string QueryName { get; }

    public async Task<string> Execute(string args)
    {
        var data = new Dictionary<string, string> { { "query", Query.ReplaceFirst(QueryArgsDelimiter, args) } };
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(TarkovDevUrl, data);
        string responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }

    public async Task<string> Execute(params object[] args)
    {
        return await Execute(string.Join(',', args)).ConfigureAwait(false);
    }

    public async Task<T?> ExecuteAs<T>(string args)
    {
        string content = await Execute(args).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(content))
            return default;
        JsonDocument document = JsonDocument.Parse(content);
        try
        {
            JsonElement dataProperty = document.RootElement.GetProperty("data");
            var data = dataProperty.GetProperty(QueryName).Deserialize<T>(JsonSerializerOptions);
            return data;
        }
        catch (Exception e)
        {
            Log.Error(e, "Json deserialization failed");
            throw;
        }
    }

    public async Task<T?> ExecuteAs<T>(params object[] args)
    {
        return await ExecuteAs<T>(string.Join(',', args));
    }
}