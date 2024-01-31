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

            Routing.RegisterRoute(AllWorksViewModel.Route, typeof(AllWorksPage));
            Routing.RegisterRoute(WorkViewModel.Route, typeof(WorkPage));
            Routing.RegisterRoute(EditWorkViewModel.Route, typeof(EditWorkPage));

            Routing.RegisterRoute(AllPupilsViewModel.Route, typeof(AllPupilsPage));
            Routing.RegisterRoute(PupilViewModel.Route, typeof(PupilPage));
            Routing.RegisterRoute(EditPupilViewModel.Route, typeof(EditPupilPage));

            Routing.RegisterRoute(AllRepertoireViewModel.Route, typeof(AllRepertoirePage));
            Routing.RegisterRoute(RepertoireViewModel.Route, typeof(RepertoirePage));
            Routing.RegisterRoute(EditRepertoireViewModel.Route, typeof(EditRepertoirePage));
        }
    }
}