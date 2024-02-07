using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;
using MusicOrganisationApp.Lib.ViewModels.ModelViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.ViewModels.CollectionViewModels
{
    public class AllPupilLessonsViewModel : CollectionViewModelBase<LessonData>, IQueryAttributable
    {
        public const string ROUTE = nameof(AllPupilLessonsViewModel);
        public const string PUPIL_ID_PARAMETER = nameof(PUPIL_ID_PARAMETER);

        private readonly PupilLessonService _service;
        private int _pupilId;

        public AllPupilLessonsViewModel() : base(LessonViewModel.ROUTE, EditLessonViewModel.ROUTE)
        {
            _service = new(_database);
        }

        private int PupilId
        {
            set
            {
                _pupilId = value;
                _service.PupilId = value;
            }
        }

        protected override IService<LessonData> Service => _service;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(PUPIL_ID_PARAMETER, out object? value) && value is int pupilId)
            {
                PupilId = pupilId;
            }
        }

        protected override void AddAddNewParameters(Dictionary<string, object> parameters)
        {
            parameters[EditLessonViewModel.PUPIL_ID_PARAMETER] = _pupilId;
        }
    }
}