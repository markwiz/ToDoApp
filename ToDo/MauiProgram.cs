<<<<<<< Updated upstream
﻿using Microsoft.Extensions.Logging;
<<<<<<< Updated upstream
using ToDoApp.Services;
=======
﻿using ToDo.Services;
using ToDo.Views;
using ToDo.ViewModels;
>>>>>>> Stashed changes
=======
using ToDo.Services;     // ← õige services namespace
using ToDo.ViewModels;   // ← sinu VM-id
using ToDo.Views;        // ← sinu Views
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<ITaskRepository, TaskRepository>();

=======
            // DI
            builder.Services.AddSingleton<ITaskRepository, TaskRepository>(); // ToDo.Services
            builder.Services.AddSingleton<AppShell>();                        // Shell
            builder.Services.AddTransient<AllTasksViewModel>();               // VM
            builder.Services.AddTransient<AllTasksPage>();                    // Page
>>>>>>> Stashed changes

            return builder.Build();
        }
    }
}
