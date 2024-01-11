using MusicOrganisation.App.Views;
using MusicOrganisation.Lib.ViewModels;
using System.Diagnostics;

namespace MusicOrganisation.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(AllComposersViewModel.ROUTE, typeof(AllComposersPage));
            Routing.RegisterRoute(ComposerViewModel.ROUTE, typeof(ComposerPage));

            Debug.WriteLine($"App data directory: {FileSystem.AppDataDirectory}");
        }
    }
}