using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;

namespace MusicOrganisationApp.Lib.ViewModels.IndividualViewModels
{
    public partial class ComposerViewModel : ViewModelBase, IQueryAttributable
    {
        private const string _ROUTE = nameof(ComposerViewModel);
        private const string _ID_PARAMETER = nameof(_ID_PARAMETER);

        private readonly ComposerService _service;
        private ComposerData _value;

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
        }

        public static string Route => _ROUTE;

        public AsyncRelayCommand EditCommand => throw new NotImplementedException();

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(_ID_PARAMETER, out object? value) && value is int id)
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
