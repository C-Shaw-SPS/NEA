using MusicOrganisationApp.App.Views;
using MusicOrganisationApp.App.Views.CollectionViews;
using MusicOrganisationApp.App.Views.EditViews;
using MusicOrganisationApp.App.Views.ModelViews;
using MusicOrganisationApp.Lib.ViewModels;
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
            RegisterLessonRoutes();
            RegisterAvaliabilityRoutes();
            RegisterTimetableRoutes();
        }

        private void RegisterComposerRoutes()
        {
            RegisterRoute<AllComposersViewModel, AllComposersPage>();
            RegisterRoute<ComposerViewModel, ComposerPage>();
            RegisterRoute<EditComposerViewModel, EditComposerPage>();
        }

        private void RegisterWorkRoutes()
        {
            RegisterRoute<AllWorksViewModel, AllWorksPage>();
            RegisterRoute<WorkViewModel, WorkPage>();
            RegisterRoute<EditWorkViewModel, EditWorkPage>();
        }

        private void RegisterPupilRoutes()
        {
            RegisterRoute<AllPupilsViewModel, AllPupilsPage>();
            RegisterRoute<PupilViewModel, PupilPage>();
            RegisterRoute<EditPupilViewModel, EditPupilPage>();
        }

        private void RegisterRepertoireRoutes()
        {
            RegisterRoute<AllRepertoireViewModel, AllRepertoirePage>();
            RegisterRoute<RepertoireViewModel, RepertoirePage>();
            RegisterRoute<EditRepertoireViewModel, EditRepertoirePage>();
        }

        private void RegisterCaregiverRoutes()
        {
            RegisterRoute<AllCaregiversViewModel, AllCaregiversPage>();
            RegisterRoute<CaregiverViewModel, CaregiverPage>();
            RegisterRoute<EditCaregiverViewModel, EditCaregiverPage>();
        }

        private void RegisterPupilCaregiverRoutes()
        {
            RegisterRoute<AllPupilCaregiversViewModel, AllPupilCaregiversPage>();
            RegisterRoute<PupilCaregiverViewModel, PupilCaregiverPage>();
            RegisterRoute<EditPupilCaregiverViewModel, EditPupilCaregiverPage>();
        }

        private void RegisterLessonSlotRoutes()
        {
            RegisterRoute<AllLessonSlotsViewModel, AllLessonSlotsPage>();
            RegisterRoute<LessonSlotViewModel, LessonSlotPage>();
            RegisterRoute<EditLessonSlotViewModel, EditLessonSlotPage>();
        }

        private void RegisterLessonRoutes()
        {
            RegisterRoute<AllLessonsViewModel, AllLessonsPage>();
            RegisterRoute<LessonViewModel, LessonPage>();
            RegisterRoute<EditLessonViewModel, EditLessonPage>();
            RegisterRoute<AllPupilLessonsViewModel, AllPupilLessonsPage>();
        }

        private void RegisterAvaliabilityRoutes()
        {
            RegisterRoute<PupilAvailabilityViewModel, PupilAvailabilityPage>();
            RegisterRoute<AddPupilAvailabilityViewModel, AddPupilAvailabilityPage>();
        }

        private void RegisterTimetableRoutes()
        {
            RegisterRoute<TimetableGeneratorViewModel, TimetableGeneratorPage>();
        }

        private void RegisterRoute<TViewModel, TContentPage>()
            where TContentPage : ContentPage
            where TViewModel : IViewModel
        {
            Routing.RegisterRoute(TViewModel.Route, typeof(TContentPage));
        }
    }
}