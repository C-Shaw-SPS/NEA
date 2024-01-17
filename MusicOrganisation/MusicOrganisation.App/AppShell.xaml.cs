using MusicOrganisation.App.Views;
using MusicOrganisation.Lib.ViewModels;
using MusicOrganisation.Lib.ViewModels.CollectionViewModels;
using MusicOrganisation.Lib.ViewModels.ModelViewModels;
using MusicOrganisation.Lib.ViewModels.EditViewModels;
using System.Diagnostics;

namespace MusicOrganisation.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(AllComposersViewModel.Route, typeof(AllComposersPage));
            Routing.RegisterRoute(ComposerViewModel.Route, typeof(ComposerPage));
            Routing.RegisterRoute(DeveloperToolsViewModel.Route, typeof(DeveloperToolsPage));
            Routing.RegisterRoute(EditComposerViewModel.Route, typeof(EditComposerPage));

            Debug.WriteLine($"App data directory: {FileSystem.AppDataDirectory}");
        }
    }
}