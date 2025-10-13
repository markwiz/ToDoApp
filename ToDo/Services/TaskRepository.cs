using SQLite;
using ToDo.Models;

namespace ToDo.Services;

public class TaskRepository : ITaskRepository
{
    private SQLiteAsyncConnection? _db;
    private readonly string _dbPath;

    public TaskRepository()
    {
        var fileName = "todo.db3";
        _dbPath = Path.Combine(FileSystem.AppDataDirectory, fileName);
    }

    private async Task Init()
    {
        if (_db != null) return;
        _db = new SQLiteAsyncConnection(_dbPath);
        await _db.CreateTableAsync<TaskItem>();
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        await Init();
        return await _db!.Table<TaskItem>().ToListAsync();
    }

    public async Task<List<TaskItem>> GetUncompletedAsync()
    {
        await Init();
        return await _db!.Table<TaskItem>().Where(t => !t.IsCompleted).ToListAsync();
    }

    public async Task<List<TaskItem>> GetCompletedAsync()
    {
        await Init();
        return await _db!.Table<TaskItem>().Where(t => t.IsCompleted).ToListAsync();
    }

    public async Task<List<TaskItem>> GetTodayAsync(DateTime todayLocal)
    {
        await Init();
        return await _db!.Table<TaskItem>()
            .Where(t => t.DueDate != null && t.DueDate.Value.Date == todayLocal.Date)
            .ToListAsync();
    }

    public async Task<int> AddOrUpdateAsync(TaskItem item)
    {
        await Init();
        return item.Id == 0
            ? await _db!.InsertAsync(item)
            : await _db!.UpdateAsync(item);
    }

    public async Task<int> DeleteAsync(int id)
    {
        await Init();
        return await _db!.DeleteAsync<TaskItem>(id);
    }
}
