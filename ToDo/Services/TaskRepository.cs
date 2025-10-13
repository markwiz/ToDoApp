using SQLite;
using ToDo.Models;

namespace ToDo.Services;

public sealed class TaskRepository : ITaskRepository
{
    private SQLiteAsyncConnection? _db;
    private readonly string _dbPath;

    public TaskRepository()
    {
        var fileName = "todo.db3";
        _dbPath = Path.Combine(FileSystem.AppDataDirectory, fileName);
    }

    public async Task InitializeAsync()
    {
        if (_db != null) return;
        _db = new SQLiteAsyncConnection(_dbPath);
        await _db.CreateTableAsync<TaskItem>();
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        await InitializeAsync();
        return await _db!.Table<TaskItem>().OrderBy(t => t.IsCompleted).ThenBy(t => t.DueDate).ToListAsync();
    }

    public async Task<List<TaskItem>> GetUncompletedAsync()
    {
        await InitializeAsync();
        return await _db!.Table<TaskItem>().Where(t => !t.IsCompleted).OrderBy(t => t.DueDate).ToListAsync();
    }

    public async Task<List<TaskItem>> GetCompletedByDateRangeAsync(DateTime startLocal, DateTime endLocal)
    {
        await InitializeAsync();
        var s = startLocal.Date; var e = endLocal.Date;
        return await _db!.Table<TaskItem>()
            .Where(t => t.IsCompleted && t.DueDate != null && t.DueDate.Value.Date >= s && t.DueDate.Value.Date <= e)
            .OrderBy(t => t.DueDate).ToListAsync();
    }

    public async Task<List<TaskItem>> GetTodayAsync(DateTime todayLocal)
    {
        await InitializeAsync();
        var d = todayLocal.Date;
        return await _db!.Table<TaskItem>()
            .Where(t => t.DueDate != null && t.DueDate.Value.Date == d)
            .OrderBy(t => t.IsCompleted).ToListAsync();
    }

    public async Task<int> AddOrUpdateAsync(TaskItem item)
    {
        await InitializeAsync();
        return item.Id == 0 ? await _db!.InsertAsync(item) : await _db!.UpdateAsync(item);
    }

    public async Task<int> DeleteAsync(int id)
    {
        await InitializeAsync();
        return await _db!.DeleteAsync<TaskItem>(id);
    }
}
