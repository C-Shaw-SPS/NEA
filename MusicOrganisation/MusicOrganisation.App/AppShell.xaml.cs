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
            Routing.RegisterRoute(DeveloperToolsViewModel.ROUTE, typeof(DeveloperToolsPage));
            Routing.RegisterRoute(EditComposerViewModel.ROUTE, typeof(EditComposerPage));

            Debug.WriteLine($"App data directory: {FileSystem.AppDataDirectory}");
        }
    }
}