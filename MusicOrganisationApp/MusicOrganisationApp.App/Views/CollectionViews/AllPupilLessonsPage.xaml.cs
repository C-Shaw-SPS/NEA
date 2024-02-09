using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisationApp.App.Views.CollectionViews
{
    public partial class AllPupilLessonsPage : ContentPage
    {
        public AllPupilLessonsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            AllPupilLessonsViewModel viewModel = (AllPupilLessonsViewModel)BindingContext;
            await viewModel.RefreshAsync();
        }
    }
}