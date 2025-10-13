using ToDo.ViewModels;

namespace ToDo.Views
{
    public partial class AllTasksPage : ContentPage
    {
        //private readonly AllTasksViewModel _vm;

        public AllTasksPage()
        {
            InitializeComponent(); // This MUST be called for XAML to load
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Button Clicked", "You pressed the button!", "OK");
        }
    }
}
