using MusicOrganisation.Lib.ViewModels;

namespace MusicOrganisation.App.Views;

public partial class AllComposersPage : ContentPage
{
	public AllComposersPage()
	{
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        AllComposersViewModel viewModel = (AllComposersViewModel)BindingContext;
        await viewModel.RefreshAsync();
        viewModel.SelectedComposer = null;
    }
}