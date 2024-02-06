using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisationApp.App.Views.CollectionViews
{
    public partial class AllComposersPage : ContentPage
    {
    	public AllComposersPage()
    	{
    		InitializeComponent();
    	}

        protected override async void OnAppearing()
        {
            AllComposersViewModel viewModel = (AllComposersViewModel)BindingContext;
            await viewModel.RefreshAsync();
        }
    }
}