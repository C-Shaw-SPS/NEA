using CommunityToolkit.Mvvm.ComponentModel;
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

        public ComposerViewModel()
        {
            _name = string.Empty;
            _completeName = string.Empty;
            _birthDate = string.Empty;
            _deathDate = string.Empty;
            _era = string.Empty;
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
            CompleteName = Composer.CompleteName;
            Era = Composer.Era;
            if (Composer.BirthDate is DateTime birthDate)
            {
                BirthDate = birthDate.Year.ToString();
            }
            if (Composer.DeathDate is DateTime deathDate)
            {
                DeathDate = deathDate.Year.ToString();
            }
        }
    }
}