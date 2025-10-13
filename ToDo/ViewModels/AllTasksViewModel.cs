using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDo.Models;
using ToDo.Services;

namespace ToDo.ViewModels;

public partial class AllTasksViewModel : ObservableObject
{
    private readonly ITaskRepository _repo;

    [ObservableProperty] private bool isBusy;
    public ObservableCollection<TaskItem> Items { get; } = new();

    public AllTasksViewModel(ITaskRepository repo)
    {
        _repo = repo;
    }

    // Kutsub code-behind (OnAppearing)
    public async Task RefreshAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            Items.Clear();
            var all = await _repo.GetAllAsync();
            foreach (var t in all) Items.Add(t);
        }
        finally { IsBusy = false; }
    }

    // XAML-is <RefreshView Command="{Binding RefreshCommand}">
    [RelayCommand]
    private Task Refresh() => RefreshAsync();

    // XAML-is CheckBox.EventToCommandBehavior → ToggleCompleteCommand
    [RelayCommand]
    private async Task ToggleComplete(TaskItem item)
    {
        item.IsCompleted = !item.IsCompleted;
        await _repo.AddOrUpdateAsync(item);
        await RefreshAsync();
    }
}
