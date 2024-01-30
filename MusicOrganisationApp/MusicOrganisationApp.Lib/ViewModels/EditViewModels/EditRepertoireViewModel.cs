using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditRepertoireViewModel : EditViewModelBase<Repertoire>
    {
        public const string PUPIL_ID_PARAMETER = nameof(PUPIL_ID_PARAMETER);

        private readonly RepertoireService _service;

        [ObservableProperty]
        private DateTime _dateStarted;

        [ObservableProperty]
        private string _syllabus = string.Empty;

        [ObservableProperty]
        private bool _isFinishedLearning;

        [ObservableProperty]
        private string _notes = string.Empty;

        [ObservableProperty]
        private int _workId;

        public EditRepertoireViewModel(string editPageTitle, string newPageTitle) : base(editPageTitle, newPageTitle)
        {
            _service = new(_database);
        }

        protected override IService<Repertoire> Service => _service;

        protected override void SetDisplayValues()
        {
            DateStarted = _value.DateStarted;
        }

        protected override bool TrySetValuesToSave()
        {
            throw new NotImplementedException();
        }

        public override void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(PUPIL_ID_PARAMETER, out object? value) && value is int id)
            {
                _service.PupilId = id;
            }
            base.ApplyQueryAttributes(query);
        }
    }
}