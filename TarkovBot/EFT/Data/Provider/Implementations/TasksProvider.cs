using TarkovBot.GraphQL;
using Task = TarkovBot.EFT.Data.Raw.Task;

namespace TarkovBot.EFT.Data.Provider.Implementations;

public class TasksProvider : LocalizedDataProvider<Task>
{
    public TasksProvider() : base(GraphQlQueryBuilder.FromResource("tasks")!)
    {
    }
}