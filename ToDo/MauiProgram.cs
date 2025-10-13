using Microsoft.Extensions.Logging;
using ToDo.ViewModels;
using ToDo.Views;
using ToDoApp.Services;

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
            builder.Services.AddTransient<UncompletedTasksViewModel>();
            builder.Services.AddTransient<UncompletedTasksPage>();


            return builder.Build();
        }
    }
}
