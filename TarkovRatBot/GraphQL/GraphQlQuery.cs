using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using TarkovRatBot.Extensions;

namespace TarkovRatBot.GraphQL;

public class GraphQlQuery
{
    public GraphQlQuery(string graphQlFileName)
    {
        HttpClient = new();
        if (!graphQlFileName.EndsWith(".graphql"))
            graphQlFileName += ".graphql";
        Query = Assembly.GetExecutingAssembly().ReadFileToEnd(graphQlFileName);
        if (string.IsNullOrWhiteSpace(Query))
        {
            IsValid = false;
            Console.WriteLine($"Failed to load file : {graphQlFileName}");
        }
        else
            IsValid = true;
    }

    private HttpClient HttpClient { get; }
    private string     Query      { get; }
    public  bool       IsValid    { get; }

    public async Task<string> Execute(string param = null)
    {
        if (!IsValid)
            return string.Empty;
        var data = new Dictionary<string, string>
        {
                { "query", string.IsNullOrWhiteSpace(param) ? Query : Query.Replace("@", param) }
        };
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(Consts.TarkovDevUrl, data);
        string responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }

    public async Task<T> ExecuteAs<T>(string param = null)
    {
        string content = await Execute(param);
        return string.IsNullOrWhiteSpace(content) ? default : JsonSerializer.Deserialize<T>(content);
    }
}