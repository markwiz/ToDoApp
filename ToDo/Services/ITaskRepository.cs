using ToDoApp.Models;

namespace ToDoApp.Services;

public interface ITaskRepository
{
    Task InitializeAsync();
    Task<List<TaskItem>> GetAllAsync();
    Task<List<TaskItem>> GetUncompletedAsync();
    Task<List<TaskItem>> GetCompletedByDateRangeAsync(DateTime startLocal, DateTime endLocal);
    Task<List<TaskItem>> GetTodayAsync(DateTime todayLocal);
    Task<int> AddOrUpdateAsync(TaskItem item);
    Task<int> DeleteAsync(int id);
}
