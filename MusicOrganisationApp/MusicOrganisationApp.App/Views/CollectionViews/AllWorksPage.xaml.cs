using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisationApp.App.Views.CollectionViews
{
    public partial class AllWorksPage : ContentPage
    {
        public AllWorksPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            AllWorksViewModel viewModel = (AllWorksViewModel)BindingContext;
            await viewModel.RefreshAsync();
        }
    }
}