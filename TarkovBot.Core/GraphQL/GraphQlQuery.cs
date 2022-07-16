using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TarkovBot.Core.Extensions;

namespace TarkovBot.Core.GraphQL;

public class GraphQlQuery
{
    public const            string     QueryArgsDelimiter = "${ARGS}";
    private static readonly HttpClient HttpClient         = new();

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
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(Constants.TarkovDevUrl, data);
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
            TarkovCore.WriteLine("RESPONSE ----------");
            TarkovCore.WriteLine(content);
            TarkovCore.WriteLine("ERROR ----------");
            TarkovCore.WriteLine(e.Message    + "", ConsoleColor.Red);
            TarkovCore.WriteLine(e.StackTrace + "", ConsoleColor.Red);
            throw;
        }
    }

    public async Task<T?> ExecuteAs<T>(params object[] args)
    {
        return await ExecuteAs<T>(string.Join(',', args));
    }
}