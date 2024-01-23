using MusicOrganisationApp.App.Views;
using MusicOrganisationApp.Lib.ViewModels.IndividualViewModels;

namespace MusicOrganisationApp.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute(ComposerViewModel.Route, typeof(ComposerPage));
        }
    }
}
