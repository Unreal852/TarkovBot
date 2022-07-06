using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using TarkovBot.Core.Extensions;
using TarkovBot.Core.TarkovData;

namespace TarkovBot.Core.GraphQL;

public class GraphQlQuery
{
    public const            string     QueryArgsDelimiter = "${ARGS}";
    private static readonly HttpClient HttpClient         = new();

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
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
        var data = new Dictionary<string, string> { { "query", Query.Replace(QueryArgsDelimiter, args) } };
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(Constants.TarkovDevUrl, data);
        string responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }

    public async Task<string> Execute(params object[] args)
    {
        return await Execute(string.Join(',', args));
    }

    public async Task<T?> ExecuteAs<T>(string args)
    {
        string content = await Execute(args);
        if (string.IsNullOrWhiteSpace(content))
            return default;
        JsonDocument document = JsonDocument.Parse(content);
        JsonElement dataProperty = document.RootElement.GetProperty("data");
        return dataProperty.GetProperty(QueryName).Deserialize<T>(JsonSerializerOptions);
    }

    public async Task<T?> ExecuteAs<T>(params object[] args)
    {
        return await ExecuteAs<T>(string.Join(',', args));
    }
}