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

            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            RegisterComposerRoutes();
            RegisterWorkRoutes();
            RegisterPupilRoutes();
            RegisterRepertoireRoutes();
            RegisterCaregiverRoutes();
            RegisterPupilCaregiverRoutes();
            RegisterLessonSlotRoutes();
            RegisterLessonRoues();
        }

        private void RegisterComposerRoutes()
        {
            RegisterRoute<AllComposersPage>(AllComposersViewModel.ROUTE);
            RegisterRoute<ComposerPage>(ComposerViewModel.ROUTE);
            RegisterRoute<EditComposerPage>(EditComposerViewModel.ROUTE);
        }

        private void RegisterWorkRoutes()
        {
            RegisterRoute<AllWorksPage>(AllWorksViewModel.ROUTE);
            RegisterRoute<WorkPage>(WorkViewModel.ROUTE);
            RegisterRoute<EditWorkPage>(EditWorkViewModel.ROUTE);
        }

        private void RegisterPupilRoutes()
        {
            RegisterRoute<AllPupilsPage>(AllPupilsViewModel.ROUTE);
            RegisterRoute<PupilPage>(PupilViewModel.ROUTE);
            RegisterRoute<EditPupilPage>(EditPupilViewModel.ROUTE);
        }

        private void RegisterRepertoireRoutes()
        {
            RegisterRoute<AllRepertoirePage>(AllRepertoireViewModel.ROUTE);
            RegisterRoute<RepertoirePage>(RepertoireViewModel.ROUTE);
            RegisterRoute<EditRepertoirePage>(EditRepertoireViewModel.ROUTE);
        }

        private void RegisterCaregiverRoutes()
        {
            RegisterRoute<AllCaregiversPage>(AllCaregiversViewModel.ROUTE);
            RegisterRoute<CaregiverPage>(CaregiverViewModel.ROUTE);
            RegisterRoute<EditCaregiverPage>(EditCaregiverViewModel.ROUTE);
        }

        private void RegisterPupilCaregiverRoutes()
        {
            RegisterRoute<AllPupilCaregiversPage>(AllPupilCaregiversViewModel.ROUTE);
            RegisterRoute<PupilCaregiverPage>(PupilCaregiverViewModel.ROUTE);
            RegisterRoute<EditPupilCaregiverPage>(EditPupilCaregiverViewModel.ROUTE);
        }

        private void RegisterLessonSlotRoutes()
        {
            RegisterRoute<AllLessonSlotsPage>(AllLessonSlotsViewModel.ROUTE);
            RegisterRoute<LessonSlotPage>(LessonSlotViewModel.ROUTE);
            RegisterRoute<EditLessonSlotPage>(EditLessonSlotViewModel.ROUTE);
        }

        private void RegisterLessonRoues()
        {
            RegisterRoute<AllLessonsPage>(AllLessonsViewModel.ROUTE);
            RegisterRoute<LessonPage>(LessonViewModel.ROUTE);
            RegisterRoute<EditLessonPage>(EditLessonViewModel.ROUTE);
        }

        private void RegisterRoute<T>(string route) where T : ContentPage
        {
            Routing.RegisterRoute(route, typeof(T));
        }
    }
}