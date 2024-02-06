using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisationApp.App.Views.CollectionViews
{
    public partial class AllCaregiversPage : ContentPage
    {
    	public AllCaregiversPage()
    	{
    		InitializeComponent();
    	}

        protected override async void OnAppearing()
        {
            AllCaregiversViewModel viewModel = (AllCaregiversViewModel)BindingContext;
            await viewModel.RefreshAsync();
        }
    }
}