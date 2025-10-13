using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.ViewModels;

public class AllTasksViewModel : BindableObject
{
    private readonly ITaskRepository _repository;
    private string _newTaskTitle = string.Empty;

    public ObservableCollection<TaskItem> Tasks { get; } = new();

    public string NewTaskTitle
    {
        get => _newTaskTitle;
        set
        {
            if (_newTaskTitle != value)
            {
                _newTaskTitle = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand ToggleCompleteCommand { get; }

    public AllTasksViewModel(ITaskRepository repository)
    {
        _repository = repository;

        LoadCommand = new Command(async () => await LoadTasks());
        AddCommand = new Command(async () => await AddTask());
        DeleteCommand = new Command<TaskItem>(async (item) => await DeleteTask(item));
        ToggleCompleteCommand = new Command<TaskItem>(async (item) => await ToggleComplete(item));

        Task.Run(LoadTasks);
    }

    private async Task LoadTasks()
    {
        var list = await _repository.GetAllAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Tasks.Clear();
            foreach (var item in list)
                Tasks.Add(item);
        });
    }

    private async Task AddTask()
    {
        if (string.IsNullOrWhiteSpace(NewTaskTitle))
            return;

        var newItem = new TaskItem
        {
            Title = NewTaskTitle,
            DueDate = DateTime.Now.AddDays(1)
        };

        await _repository.AddOrUpdateAsync(newItem);
        NewTaskTitle = string.Empty;
        await LoadTasks();
    }

    private async Task DeleteTask(TaskItem item)
    {
        await _repository.DeleteAsync(item.Id);
        await LoadTasks();
    }

    private async Task ToggleComplete(TaskItem item)
    {
        item.IsCompleted = !item.IsCompleted;
        await _repository.AddOrUpdateAsync(item);
        await LoadTasks();
    }
}
