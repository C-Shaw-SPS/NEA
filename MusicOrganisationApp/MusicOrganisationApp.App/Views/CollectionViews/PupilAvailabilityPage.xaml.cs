using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisationApp.App.Views.CollectionViews
{
    public partial class PupilAvailabilityPage : ContentPage
    {
        public PupilAvailabilityPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            PupilAvailabilityViewModel viewModel = (PupilAvailabilityViewModel)BindingContext;
            await viewModel.RefreshAsync();
        }
    }
}