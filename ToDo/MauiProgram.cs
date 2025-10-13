<<<<<<< Updated upstream
﻿using Microsoft.Extensions.Logging;
using ToDoApp.Services;
=======
﻿using ToDo.Services;
using ToDo.Views;
using ToDo.ViewModels;
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
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<ITaskRepository, TaskRepository>();


            return builder.Build();
        }
    }
}
