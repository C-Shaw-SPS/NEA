using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisationApp.App.Views.CollectionViews
{
    public partial class AllPupilsPage : ContentPage
    {
    	public AllPupilsPage()
    	{
    		InitializeComponent();
    	}

        protected override async void OnNavigatedFrom(NavigatedFromEventArgs args)
        {
            AllPupilsViewModel viewModel = (AllPupilsViewModel)BindingContext;
            await viewModel.RefreshAsync();
        }
    }
}