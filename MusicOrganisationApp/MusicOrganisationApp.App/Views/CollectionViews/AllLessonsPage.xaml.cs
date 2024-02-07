using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisationApp.App.Views.CollectionViews;

public partial class AllLessonsPage : ContentPage
{
	public AllLessonsPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        AllLessonsViewModel viewModel = (AllLessonsViewModel)BindingContext;
        await viewModel.RefreshAsync();
    }
}