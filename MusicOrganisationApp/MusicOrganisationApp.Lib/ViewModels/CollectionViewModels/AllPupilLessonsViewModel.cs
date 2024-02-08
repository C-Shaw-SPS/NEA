using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public class AllPupilLessonsViewModel : CollectionViewModelBase<LessonData, LessonViewModel, EditLessonViewModel>, IQueryAttributable, IPupilDataViewModel
    {
        private const string _ROUTE = nameof(AllPupilLessonsViewModel);

        private readonly PupilLessonService _service;

        public AllPupilLessonsViewModel() : base()
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        public int? PupilId
        {
            get => _service.PupilId;
            set => _service.PupilId = value;
        }

        protected override IService<LessonData> Service => _service;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(IPupilDataViewModel.PUPIL_ID_PARAMETER, out object? value) && value is int pupilId)
            {
                PupilId = pupilId;
            }
        }

        protected override void AddAddNewParameters(Dictionary<string, object> parameters)
        {
            if (PupilId is not null)
            {
                parameters[IPupilDataViewModel.PUPIL_ID_PARAMETER] = PupilId;
            }
        }
    }
}