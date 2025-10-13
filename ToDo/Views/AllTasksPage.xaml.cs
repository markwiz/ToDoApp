using ToDo.Models;
using ToDo.ViewModels;

namespace ToDo.Views
{
    public partial class AllTasksPage : ContentPage
    {
        private readonly AllTasksViewModel _vm;

        public AllTasksPage(AllTasksViewModel vm)
        {
            InitializeComponent(); // This MUST be called for XAML to load
            BindingContext = _vm = vm;
        }

        // Called when CheckBox is toggled in the item template
        // Called when CheckBox is toggled in the item template
        private void OnCheckBoxCheckedChanged(object? sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox cb && cb.BindingContext is TaskItem item)
            {
                var cmd = _vm.ToggleCompleteCommand;
                if (cmd.CanExecute(item))
                    cmd.Execute(item);
            }
        }

        // Called when Delete button is clicked
        //private void OnDeleteClicked(object sender, EventArgs e)
        //{
        //    if (sender is Button btn && btn.BindingContext is TaskItem item)
        //    {
        //        var cmd = _vm.DeleteCommand;
        //        if (cmd.CanExecute(item))
        //            cmd.Execute(item);
        //    }
        //}
    }
}
