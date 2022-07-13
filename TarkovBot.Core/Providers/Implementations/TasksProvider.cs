using TarkovBot.Core.GraphQL;
using Task = TarkovBot.Core.Data.Task;

namespace TarkovBot.Core.Providers.Implementations;

public class TasksProvider : LocalizedDataProvider<Task>
{
    public TasksProvider() : base(GraphQlQueryBuilder.FromResource("tasks")!)
    {
    }
}