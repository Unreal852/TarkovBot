using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using TarkovRatBot.Extensions;
using static Program;

namespace TarkovRatBot.GraphQL;

public class GraphQlQuery
{
    private readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
            Converters = { new JsonStringEnumConverter() }
    };
    
    public GraphQlQuery(string graphQlFileName)
    {
        HttpClient = new();
        if (!graphQlFileName.EndsWith(".graphql"))
            graphQlFileName += ".graphql";
        Query = Assembly.GetExecutingAssembly().ReadFileToEnd(graphQlFileName);
        GraphQlQueryName = graphQlFileName[..graphQlFileName.IndexOf(".", StringComparison.Ordinal)].FirstCharToLowerCase();
        if (string.IsNullOrWhiteSpace(Query))
        {
            IsValid = false;
            Console.WriteLine($"Failed to load file : {graphQlFileName}");
        }
        else
            IsValid = true;
    }

    private HttpClient HttpClient       { get; }
    private string     Query            { get; }
    private string     GraphQlQueryName { get; }
    public  bool       IsValid          { get; }

    public async Task<string> Execute(string param = null)
    {
        if (!IsValid)
            return string.Empty;
        var data = new Dictionary<string, string>
        {
                { "query", string.IsNullOrWhiteSpace(param) ? Query : Query.Replace("@", param.Replace("\"", "")) }
        };
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(Consts.TarkovDevUrl, data);
        string responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }

    public async Task<T> ExecuteAs<T>(string param = null, string propertyName = null)
    {
        string content = await Execute(param);
        JsonDocument document = JsonDocument.Parse(content);
        WriteLine(content);
        return string.IsNullOrWhiteSpace(content)
                ? default
                : document.RootElement.GetProperty("data").GetProperty(string.IsNullOrWhiteSpace(propertyName)
                        ? GraphQlQueryName
                        : propertyName).Deserialize<T>(JsonSerializerOptions);
    }
}