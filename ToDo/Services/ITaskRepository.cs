using ToDo.Models;

namespace ToDo.Services;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync();
    Task<List<TaskItem>> GetUncompletedAsync();
    Task<List<TaskItem>> GetCompletedAsync();
    Task<List<TaskItem>> GetTodayAsync(DateTime todayLocal);

    Task<int> AddOrUpdateAsync(TaskItem item);
    Task<int> DeleteAsync(int id);
}
