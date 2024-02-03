using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Models;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class WorkViewModel : ModelViewModelBase<Work>, IQueryAttributable
    {
        public const string ROUTE = nameof(WorkViewModel);

        private readonly WorkService _service;
        private readonly AsyncRelayCommand _goToComposerCommand;

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

        public WorkViewModel() : base(EditWorkViewModel.ROUTE)
        {
            _service = new(_database);
            _goToComposerCommand = new(GoToComposerAsync);
        }

        public AsyncRelayCommand GoToComposerCommand => _goToComposerCommand;

        protected override IService<Work> Service => _service;

        private async Task GoToComposerAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [ID_PARAMETER] = _value.ComposerId
            };
            await GoToAsync(parameters, ComposerViewModel.ROUTE);
        }

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