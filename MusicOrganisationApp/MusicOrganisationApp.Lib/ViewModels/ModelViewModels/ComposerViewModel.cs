using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using MusicOrganisationApp.Lib.Tables;
using MusicOrganisationApp.Lib.ViewModels.EditViewModels;

namespace MusicOrganisationApp.Lib.ViewModels.ModelViewModels
{
    public partial class ComposerViewModel : ModelViewModelBase<ComposerData>, IQueryAttributable
    {
        public const string ROUTE = nameof(ComposerViewModel);

        private readonly ComposerService _service;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _birthYear;

        [ObservableProperty]
        private string _deathYear;

        [ObservableProperty]
        private string _era;

        public ComposerViewModel() : base(EditComposerViewModel.ROUTE)
        {
            _service = new(_database);

            _name = string.Empty;
            _birthYear = string.Empty;
            _deathYear = string.Empty;
            _era = string.Empty;
        }

        protected override IService<ComposerData> Service => _service;

        protected override void SetDisplayValues()
        {
            Name = _value.Name;
            BirthYear = _value.BirthYear.ToString() ?? string.Empty;
            DeathYear = _value.DeathYear.ToString() ?? string.Empty;
            Era = _value.Era;
        }
    }
}
