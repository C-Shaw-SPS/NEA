using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;

namespace MusicOrganisationApp.Lib.ViewModels.EditViewModels
{
    public partial class EditRepertoireViewModel : EditViewModelBase<Repertoire>
    {
        public const string PUPIL_ID_PARAMETER = nameof(PUPIL_ID_PARAMETER);

        private RepertoireService _service;

        [ObservableProperty]
        private DateTime _dateStarted;

        [ObservableProperty]
        private string _syllabus = string.Empty;

        public EditRepertoireViewModel(string editPageTitle, string newPageTitle) : base(editPageTitle, newPageTitle)
        {
            _service = new(_database);
        }

        protected override IService<Repertoire> Service => _service;

        protected override void SetDisplayValues()
        {
            throw new NotImplementedException();
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