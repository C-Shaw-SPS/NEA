using CommunityToolkit.Mvvm.ComponentModel;
using MusicOrganisation.Lib.Tables;
using MusicOrganisation.Lib.Viewmodels;

namespace MusicOrganisation.Lib.ViewModels
{
    public partial class AllComposersViewModel : ViewModelBase
    {
        [ObservableProperty]
        private List<ComposerData> _composers;

        public const string ROUTE = nameof(AllComposersViewModel);

        public AllComposersViewModel(string directory, string fileName) : base(directory, fileName)
        {
            _composers = [];
        }


    }
}