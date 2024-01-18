using MusicOrganisation.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisation.App.Views;

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
        viewModel.SelectedItem = null;
    }
}