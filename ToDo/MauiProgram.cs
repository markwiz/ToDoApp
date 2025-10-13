using Microsoft.Extensions.Logging;
using ToDoApp.Services;
using ToDoApp.ViewModels;
using ToDoApp.Views;

namespace ToDo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<ITaskRepository, TaskRepository>();

            builder.Services.AddTransient<AllTasksViewModel>();
            builder.Services.AddTransient<AllTasksPage>();


            return builder.Build();
        }
    }
}
