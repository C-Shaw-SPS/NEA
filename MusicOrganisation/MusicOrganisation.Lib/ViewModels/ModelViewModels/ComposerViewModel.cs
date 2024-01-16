using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicOrganisation.Lib.Services;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.ViewModels.EditViewModels;

namespace MusicOrganisation.Lib.ViewModels.ModelViewModels
{
    public partial class ComposerViewModel : ModelViewModelBase<ComposerData>, IQueryAttributable
    {
        public const string ROUTE = nameof(ComposerViewModel);

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
            _name = string.Empty;
            _birthYear = string.Empty;
            _deathYear = string.Empty;
            _era = string.Empty;
        }

        protected override void SetDisplayValues()
        {
            Name = _value.Name;
            Era = _value.Era;

            if (_value.BirthYear is int birthYear)
            {
                BirthYear = birthYear.ToString();
            }
            else
            {
                BirthYear = string.Empty;
            }

            if (_value.DeathYear is int deathYear)
            {
                DeathYear = deathYear.ToString();
            }
            else
            {
                DeathYear = string.Empty;
            }
        }
    }
}