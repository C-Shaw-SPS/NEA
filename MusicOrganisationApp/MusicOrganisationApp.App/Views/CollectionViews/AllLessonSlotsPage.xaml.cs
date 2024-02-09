using MusicOrganisationApp.Lib.ViewModels.CollectionViewModels;

namespace MusicOrganisationApp.App.Views.CollectionViews
{
    public partial class AllLessonSlotsPage : ContentPage
    {
        public AllLessonSlotsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            AllLessonSlotsViewModel viewModel = (AllLessonSlotsViewModel)BindingContext;
            await viewModel.RefreshAsync();
        }
    }
}