using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.IndividualViewModels
{
    public partial class ComposerViewModel : ViewModelBase, IQueryAttributable
    {
        public const string ID_PARAMETER = nameof(ID_PARAMETER);

        private const string _ROUTE = nameof(ComposerViewModel);

        private readonly ComposerService _service;
        private ComposerData _value;

        private readonly AsyncRelayCommand _editCommand;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _birthYear;

        [ObservableProperty]
        private string _deathYear;

        [ObservableProperty]
        private string _era;

        public ComposerViewModel()
        {
            _service = new(_database);
            _value = new();
            _name = string.Empty;
            _birthYear = string.Empty;
            _deathYear = string.Empty;
            _era = string.Empty;

            _editCommand = new(EditAsync);
        }

        public static string Route => _ROUTE;

        public AsyncRelayCommand EditCommand => _editCommand;

        private async Task EditAsync()
        {
            Dictionary<string, object> parameters = new()
            {
                [EditComposerViewModel.ID_PARAMETER] = _value.Id,
                [EditComposerViewModel.IS_NEW_PARAMETER] = false
            };
            await GoToAsync(parameters, EditComposerViewModel.Route);
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(ID_PARAMETER, out object? value) && value is int id)
            {
                await SetValue(id);
            }
        }

        private async Task SetValue(int id)
        {
            (bool suceeded, ComposerData composer) = await _service.GetAsync(id);
            if (suceeded)
            {
                _value = composer;
                SetDisplayValues();
            }
            else
            {
                await ReturnAsync();
            }
        }

        private void SetDisplayValues()
        {
            Name = _value.Name;
            BirthYear = _value.BirthYear.ToString() ?? string.Empty;
            DeathYear = _value.DeathYear.ToString() ?? string.Empty;
            Era = _value.Era;
        }
    }
}
