namespace TarkovBot.Services.Abstractions;

public interface IGraphQlClientService
{
    Task<string> RequestAsString(string query);
    Task<T?> RequestAs<T>(string query) where T : class;
}