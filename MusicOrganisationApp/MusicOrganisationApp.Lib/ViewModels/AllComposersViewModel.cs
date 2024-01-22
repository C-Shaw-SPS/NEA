using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisationApp.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicOrganisationApp.Lib.ViewModels
{
    public partial class AllComposersViewModel : ViewModelBase
    {
        private readonly ComposerService _service;

        [ObservableProperty]
        private string _searchText = string.Empty;

        public AllComposersViewModel()
        {
            _service = new(_database);
        }
    }
}