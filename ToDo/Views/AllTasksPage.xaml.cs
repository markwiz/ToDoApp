using ToDo.ViewModels;

namespace ToDo.Views
{
    public partial class AllTasksPage : ContentPage
    {
        private readonly AllTasksViewModel _vm;

        public AllTasksPage(AllTasksViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = _vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.RefreshAsync(); // Lae andmed kui leht ilmub
        }
    }
}
