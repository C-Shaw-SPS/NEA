using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisationApp.App.Views.CollectionViews
{
    public partial class AddPupilAvailabilityPage : ContentPage
    {
        public AddPupilAvailabilityPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            AddPupilAvailabilityViewModel viewModel = (AddPupilAvailabilityViewModel)BindingContext;
            await viewModel.RefreshAsync();
        }
    }
}