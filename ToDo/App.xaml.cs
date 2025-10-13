#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

using ToDo.Views; // peab sisaldama AllTasksPage'i
// NB! AppShell peab olema ToDo nimeruumis (vt järgmine samm)

namespace ToDo
{
    public partial class App : Application
    {
        const int WindowWidth = 1080;
        const int WindowHeight = 1920;

        public App(IServiceProvider sp)
        {
            InitializeComponent();

            // sinu WINDOWS akna suuruse kood jääb siia muutmata...

            MainPage = sp.GetRequiredService<AppShell>(); // <— oluliselt
        }

    }
}
