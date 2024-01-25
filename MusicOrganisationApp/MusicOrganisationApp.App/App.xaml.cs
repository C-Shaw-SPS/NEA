using MusicOrganisationApp.App.Views.CollectionViews;
using MusicOrganisationApp.App.Views.EditViews;
using MusicOrganisationApp.App.Views.ModelViews;
using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute(AllComposersViewModel.Route, typeof(AllComposersPage));
            Routing.RegisterRoute(ComposerViewModel.Route, typeof(ComposerPage));
            Routing.RegisterRoute(EditComposerViewModel.Route, typeof(EditComposerPage));
        }
    }
}
