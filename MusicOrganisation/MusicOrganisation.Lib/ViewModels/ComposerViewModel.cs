using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;

namespace MusicOrganisation.Lib.ViewModels
{
    public partial class ComposerViewModel : ViewModelBase, IQueryAttributable
    {
        public const string ROUTE = nameof(ComposerViewModel);

        public const string QUERY_PARAMETER = nameof(ComposerData);

        [ObservableProperty]
        private ComposerData? _composer;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _completeName;

        [ObservableProperty]
        private string _birthDate;

        [ObservableProperty]
        private string _deathDate;

        [ObservableProperty]
        private string _era;

        private readonly AsyncRelayCommand _editCommand;

        public ComposerViewModel()
        {
            _name = string.Empty;
            _completeName = string.Empty;
            _birthDate = string.Empty;
            _deathDate = string.Empty;
            _era = string.Empty;
            _editCommand = new(EditAsync);
        }

        public AsyncRelayCommand EditCommand => _editCommand;

        private async Task EditAsync()
        {
            if (Composer is not null)
            {
                Dictionary<string, object> routeParameters = new()
                {
                    [EditComposerViewModel.COMPOSER_PARAMETER] = Composer,
                    [EditComposerViewModel.IS_NEW_PARAMETER] = false
                };
                await Shell.Current.GoToAsync(EditComposerViewModel.ROUTE, routeParameters);
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue(QUERY_PARAMETER, out object? value) && value is ComposerData composer)
            {
                SetComposer(composer);
            }
        }

        private void SetComposer(ComposerData composer)
        {
            Composer = composer;
            Name = Composer.Name;
            CompleteName = Composer.Name;
            Era = Composer.Era;
            if (Composer.BirthYear is int birthYear)
            {
                BirthDate = birthYear.ToString();
            }
            if (Composer.DeathYear is int deathYear)
            {
                DeathDate = deathYear.ToString();
            }
        }
    }
}