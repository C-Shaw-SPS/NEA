using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisation.Lib.ViewModels
{
    public partial class ComposerViewModel : ViewModelBase
    {
        public const string ROUTE = nameof(ComposerViewModel);

        public const string QUERY_PARAMETER = nameof(ComposerData);

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
            _composer = composer;
            Name = _composer.Name;
            CompleteName = _composer.CompleteName;
            if (_composer.BirthDate is DateTime birthDate)
            {
                BirthDate = birthDate.ToShortDateString();
            }
            if (_composer.DeathDate is DateTime deathDate)
            {
                DeathDate = deathDate.ToShortDateString();
            }
        }
    }
}