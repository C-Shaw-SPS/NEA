using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public partial class AllLessonsViewModel : SelectableCollectionViewModel<Lesson, LessonViewModel, EditLessonViewModel>, IViewModel
    {
        private const string _ROUTE = nameof(AllLessonsViewModel);

        private readonly LessonService _service;

        [ObservableProperty]
        private DateTime _selectedDate = DateTime.Today;

        public AllLessonsViewModel() : base(GetDefaultPath(), false)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override IService<Lesson> Service => _service;

        async partial void OnSelectedDateChanged(DateTime value)
        {
            _service.Date = value;
            await RefreshAsync();
        }
    }
}