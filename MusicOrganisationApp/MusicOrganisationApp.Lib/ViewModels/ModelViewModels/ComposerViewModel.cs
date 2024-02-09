using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class ComposerViewModel : ModelViewModelBase<Composer, EditComposerViewModel>, IQueryAttributable, IViewModel
    {
        private const string _ROUTE = nameof(ComposerViewModel);

        private readonly ComposerService _service;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _birthYear;

        [ObservableProperty]
        private string _deathYear;

        [ObservableProperty]
        private string _era;

        public ComposerViewModel() : base()
        {
            _service = new(_database);

            _name = string.Empty;
            _birthYear = string.Empty;
            _deathYear = string.Empty;
            _era = string.Empty;
        }

        public static string Route => _ROUTE;

        protected override IService<Composer> Service => _service;

        protected override void SetDisplayValues()
        {
            Name = _value.Name;
            BirthYear = _value.BirthYear.ToString() ?? string.Empty;
            DeathYear = _value.DeathYear.ToString() ?? string.Empty;
            Era = _value.Era;
        }
    }
}
