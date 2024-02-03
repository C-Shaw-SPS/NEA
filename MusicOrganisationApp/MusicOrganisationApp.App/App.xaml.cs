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

            Routing.RegisterRoute(AllComposersViewModel.ROUTE, typeof(AllComposersPage));
            Routing.RegisterRoute(ComposerViewModel.ROUTE, typeof(ComposerPage));
            Routing.RegisterRoute(EditComposerViewModel.ROUTE, typeof(EditComposerPage));

            Routing.RegisterRoute(AllWorksViewModel.ROUTE, typeof(AllWorksPage));
            Routing.RegisterRoute(WorkViewModel.ROUTE, typeof(WorkPage));
            Routing.RegisterRoute(EditWorkViewModel.ROUTE, typeof(EditWorkPage));

            Routing.RegisterRoute(AllPupilsViewModel.ROUTE, typeof(AllPupilsPage));
            Routing.RegisterRoute(PupilViewModel.ROUTE, typeof(PupilPage));
            Routing.RegisterRoute(EditPupilViewModel.ROUTE, typeof(EditPupilPage));

            Routing.RegisterRoute(AllRepertoireViewModel.ROUTE, typeof(AllRepertoirePage));
            Routing.RegisterRoute(RepertoireViewModel.ROUTE, typeof(RepertoirePage));
            Routing.RegisterRoute(EditRepertoireViewModel.ROUTE, typeof(EditRepertoirePage));

            Routing.RegisterRoute(AllCaregiversViewModel.ROUTE, typeof(AllCaregiversPage));
            Routing.RegisterRoute(CaregiverViewModel.ROUTE, typeof(CaregiverPage));
            Routing.RegisterRoute(EditCaregiverViewModel.ROUTE, typeof(EditCaregiverPage));
        }
    }
}