using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class WorkViewModel : ModelViewModelBase<Work>, IQueryAttributable
    {
        private const string _ROUTE = nameof(WorkViewModel);

        private readonly WorkService _service;

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _subtitle = string.Empty;

        [ObservableProperty]
        private string _genre = string.Empty;

        [ObservableProperty]
        private string _composerName = string.Empty;

        [ObservableProperty]
        private string _notes = string.Empty;

        public WorkViewModel() : base(EditWorkViewModel.Route)
        {
            _service = new(_database);
        }

        public static string Route => _ROUTE;

        protected override IService<Work> Service => _service;

        protected override void SetDisplayValues()
        {
            Title = _value.Title;
            Subtitle = _value.Subtitle;
            Genre = _value.Genre;
            ComposerName = _value.ComposerName;
            Notes = _value.Notes;
        }
    }
}