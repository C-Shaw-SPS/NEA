using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisationApp.App.Views.CollectionViews
{
    public partial class AllPupilCaregiversPage : ContentPage
    {
        public AllPupilCaregiversPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            AllPupilCaregiversViewModel viewModel = (AllPupilCaregiversViewModel)BindingContext;
            await viewModel.RefreshAsync();
        }
    }
}